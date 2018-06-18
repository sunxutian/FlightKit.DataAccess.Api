using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FlightKit.DataAccess.Application.Mapping;
using FlightKit.DataAccess.Core.GraphQL;
using FlightKit.DataAccess.Core.GraphQL.Types;
using FlightKit.DataAccess.Domain;
using FlightKit.DataAccess.Domain.Helpers;
using FlightKit.DataAccess.Domain.Repo;
using FlightKit.DataAccess.Domain.Repo.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using FlightKit.DataAccess.Api.GraphQL;
using FlightKit.DataAccess.Core.Services;
using FlightKit.DataAccess.Core.Services.Impl;
using System.Linq.Expressions;
using FlightKit.DataAccess.Core.Helpers;
using FlightKit.DataAccess.Domain.Data.Entity;
using FlightKit.DataAccess.Domain.Data;
using FlightKit.DataAccess.Core.UnitOfWork.CommandHandlers;
using FlightKit.DataAccess.Api.UnitOfWork.CommandHandlerDecorators;
using FlightKit.DataAccess.Core.UnitOfWork.Commands;
using FlightKit.DataAccess.Application.Models;
using System.Collections.Generic;
using FlightKit.DataAccess.Api.Services;
using GraphQL.Server.Ui.Voyager;
using GraphQL.Server.Transports.WebSockets;

namespace FlightKit.DataAccess.Api
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        private Container container = new Container();

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }
        public static IHostingEnvironment HostingEnvironment { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "FlightKit Address Validation Api",
                    Version = "v1",
                });
                c.DescribeStringEnumsInCamelCase();
                c.AddSecurityDefinition("api-key", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme
                {
                    Description = "AWS Api GateWay Key",
                    Name = "X-API-KEY",
                    Type = "apiKey",
                    In = "header"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"api-key", new []{"readAccess", "writeAccess"} }
                });
            });
            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddDbContext<FlightKitDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FlightKitConnectionString")));
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(container.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISchema, FlightKitDataAccessSchema>();

            services.AddGraphQLHttp();
            services.AddGraphQLWebSocket<ISchema>();

            if (HostingEnvironment.IsDevelopment())
            {
                services.Configure<ExecutionOptions>(options =>
                {
                    options.EnableMetrics = true;
                    options.ExposeExceptions = true;
                });
            }

            // initialize injector
            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeContainer(app);
            loggerFactory.AddLambdaLogger(Configuration.GetLambdaLoggerOptions());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AnyOrigin");
            app.UseWebSockets();

            // add http for Schema at default url /graphql
            app.UseGraphQLHttp<ISchema>(new GraphQLHttpOptions());

            // use websocket middleware for ChatSchema at default url /graphql
            app.UseGraphQLWebSocket<ISchema>(new GraphQLWebSocketsOptions());

            if (env.IsDevelopment())
            {
                // use graphql-playground at default url /ui/playground
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

                // use voyager middleware at default url /ui/voyager
                app.UseGraphQLVoyager(new GraphQLVoyagerOptions());
            }

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, request) =>
                {
                    swaggerDoc.Schemes = new List<string> { request.Scheme };
                    swaggerDoc.Schemes.Add("https");
                    swaggerDoc.Schemes = swaggerDoc.Schemes.Distinct().ToList();
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightKit Address Validation Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);

            // Add application services. For instance:
            Initialize(container);
        }

        private void Initialize(Container container)
        {
            SetupEntityFramework(container);
            SetupAutoMapper(container);
            SetupGraphQL(container);
            SetupService(container);
            SetupUnitOrWork(container);
            //AllowResolvingFuncFactories(container.Options);
        }

        private void SetupUnitOrWork(Container container)
        {
            container.Register(typeof(ICommandHandler<,>), new[] { typeof(ICommandHandler<,>).Assembly },
                            Lifestyle.Scoped);

            SetupSyncMetadataFilterCommandHandler<Risk_Report, RiskReport>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_AdditionDate, RiskAdditionDate>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_Comment, RiskComment>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_CommentSegment, RiskCommentSegment>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_Exposure, RiskExposure>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_FireDivisionRisk, RiskFireDivisionRisk>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_FloorsAndRoof, RiskFloorsAndRoof>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_InternalProtection, RiskInternalProtection>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_Occupant, RiskOccupant>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_OccupantLevel, RiskOccupantLevel>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_OccupantHazard, RiskOccupantHazard>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ProtectionSafeguard, RiskProtectionSafeguard>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportAddress, RiskReportAddress>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportAttachment, RiskReportAttachment>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportBuildingInformation, RiskReportBuildingInformation>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportHazard, RiskReportHazard>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportPhoto, RiskReportPhoto>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_ReportRelatedDate, RiskReportRelatedDate>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_RetiredOccupantNumber, RiskRetiredOccupantNumber>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_SecondaryConstruction, RiskSecondaryConstruction>(container);
            SetupSyncMetadataFilterCommandHandler<Risk_Wall, RiskWall>(container);

            container.RegisterDecorator(typeof(ICommandHandler<,>), context =>
                typeof(DbTransactionCommandHandlerDecorator<,,>).MakeGenericType(typeof(FlightKitDbContext), context.ServiceType.GetGenericArguments()[0], context.ServiceType.GetGenericArguments()[1]),
                Lifestyle.Scoped, context => context.ImplementationType.GetCustomAttributes<TransactionAttribute>(true) != null);
        }

        private void SetupSyncMetadataFilterCommandHandler<TEntity, TDto>(Container container)
            where TEntity : RiskEntityWithSyncMetadata, new()
            where TDto : RiskDtoWithSyncMetadata
        {
            container.Register<
                ICommandHandler<GetRiskSyncMetadataByRiskReportFilterCommand<TEntity, TDto>,
                (ICollection<TDto> data, int totalReportsCount, Guid? endReportIdCursor, bool hasNext)>,
                GetRiskSyncMetadataByRiskReportFilterCommandHandler<TEntity, TDto>>(Lifestyle.Scoped);
        }

        private void SetupService(Container container)
        {
            container.RegisterSingleton<IMappingHelperService, MappingHelperService>();
            container.Register<IFlightKitReportDataService, FlightKitReportDataService>(Lifestyle.Scoped);
            RegisterFuncFactory<IFlightKitReportDataService, FlightKitReportDataService>(container, Lifestyle.Scoped);
            container.RegisterSingleton<ICommandHandlerFactory, CommandHandlerFactory>();
            container.Register(typeof(ITransactionFactory<>), typeof(TransactionFactory<>), Lifestyle.Scoped);
        }

        private void SetupGraphQL(Container container)
        {
            var graphQLTypes = typeof(RiskAdditionDateType).Assembly;
            var registrations = graphQLTypes.GetExportedTypes()
                .Where(t => t.Namespace == "FlightKit.DataAccess.Core.GraphQL.Types")
                .Where(t => !t.IsAbstract);

            foreach (var t in registrations)
            {
                container.RegisterSingleton(t, t);
            }

            container.RegisterSingleton<FlightKitDataAccessQuery>();
        }

        private void SetupAutoMapper(Container container)
        {
            container.RegisterConditional<IMapper>(Lifestyle.Singleton.CreateRegistration(typeof(IMapper), () =>
            {
                var config = new MapperConfiguration(
                       cfg =>
                       {
                           cfg.AddProfile<FlightKitDomainToFlightKitApplicationMappingProfile>();
                       });
                config.AssertConfigurationIsValid();
                IMapper mapper = config.CreateMapper(container.GetInstance);
                return mapper;
            }, container), 
            context => context.Consumer?.Target?.GetCustomAttribute<QueryWithSyncMetadataAttribute>() != null);

            container.RegisterConditional<IMapper>(Lifestyle.Singleton.CreateRegistration(typeof(IMapper), () =>
            {
                var config = new MapperConfiguration(
                       cfg =>
                       {
                           cfg.AddProfile<FlightKitDomainToFlightKitApplicationWithNoSyncMetadataMappingProfile>();
                       });
                config.AssertConfigurationIsValid();
                IMapper mapper = config.CreateMapper(container.GetInstance);
                return mapper;
            }, container),
            context => context.Consumer?.Target?.GetCustomAttribute<QueryWithSyncMetadataAttribute>() == null);
        }

        private void SetupEntityFramework(Container container)
        {
            Func<PredicateContext, bool> noTrackChange = (context) => context.Consumer?.ImplementationType?
                .GetCustomAttribute<NotTrackDbChangeAttribute>(true) != null
                    || context.Consumer?.Target?
                .GetCustomAttribute<NotTrackDbChangeAttribute>(true) != null;

            container.RegisterConditional(typeof(IDbRepository<>), context =>
            {
                var entityType = context.ServiceType.GetGenericArguments()[0];
                var isFkDb = typeof(IFlightKitEntity).IsAssignableFrom(entityType);
                var dbContextType = typeof(FlightKitDbContext);

                return typeof(LocationDbRepository<,>).MakeGenericType(dbContextType, entityType);
            }, Lifestyle.Scoped, c =>
             c.ServiceType.IsGenericType &&
             c.ServiceType.GetGenericArguments()[0].GetCustomAttribute<HasLocationDataAttribute>() != null);

            container.RegisterConditional(typeof(IDbRepository<>), typeof(FlightKitReadonlyDbRepository<>),
                Lifestyle.Scoped,
                context => noTrackChange(context) && !context.Handled);

            container.RegisterConditional(typeof(IDbRepository<>), typeof(FlightKitDbRepository<>),
                Lifestyle.Scoped,
                config => !config.Handled);

            container.Collection.Register<IDbRepository>(
                typeof(Risk_Report).Assembly.ExportedTypes
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .Where(typeof(IFlightKitEntity).IsAssignableFrom)
                    .Select(t => typeof(FlightKitReadonlyDbRepository<>).MakeGenericType(t))
                    .Select(t => Lifestyle.Scoped.CreateRegistration(t, container)));
        }

        private void RegisterFuncFactory<TService, TImpl>(
            Container container, Lifestyle lifestyle = null)
            where TService : class
            where TImpl : class, TService
        {
            lifestyle = lifestyle ?? container.Options.DefaultLifestyle;
            var producer = lifestyle.CreateProducer<TService, TImpl>(container);
            container.RegisterInstance<Func<TService>>(producer.GetInstance);
        }

        private void AllowResolvingFuncFactories(ContainerOptions options)
        {
            options.Container.ResolveUnregisteredType += (s, e) => {
                var type = e.UnregisteredServiceType;

                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Func<>))
                {
                    return;
                }

                Type serviceType = type.GetGenericArguments().First();

                InstanceProducer registration =
                    options.Container.GetRegistration(serviceType, true);

                Type funcType = typeof(Func<>).MakeGenericType(serviceType);

                var factoryDelegate = Expression.Lambda(funcType,
                    registration.BuildExpression()).Compile();

                e.Register(Expression.Constant(factoryDelegate));
            };
        }

        #region root service classes
        private class CommandHandlerFactory : ICommandHandlerFactory
        {
            private readonly Container _container;

            public CommandHandlerFactory(Container container)
            {
                _container = container;
            }
            public ICommandHandler<TCommand, TResult> RequestCommandHandler<TCommand, TResult>() 
                where TCommand : ICommand
            {
                return _container.GetInstance<ICommandHandler<TCommand, TResult>>();
            }
        }
        #endregion
    }
}
