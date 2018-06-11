using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddDbContext<FlightKitDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FlightKitConnectionString")));
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(container.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISchema, FlightKitDataAccessSchema>();

            services.AddGraphQLHttp();

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

            // add http for Schema at default url /graphql
            app.UseGraphQLHttp<ISchema>(new GraphQLHttpOptions());

            if (env.IsDevelopment())
            {
                // use graphql-playground at default url /ui/playground
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            }

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
            //AllowResolvingFuncFactories(container.Options);
        }

        private void SetupService(Container container)
        {
            container.RegisterSingleton<IMappingHelperService, MappingHelperService>();
            container.Register<IFlightKitReportDataService, FlightKitReportDataService>(Lifestyle.Scoped);
            RegisterFuncFactory<IFlightKitReportDataService, FlightKitReportDataService>(container, Lifestyle.Scoped);
        }

        private void SetupGraphQL(Container container)
        {
            var graphQLTypes = typeof(RiskAdditionDateType).Assembly;
            var registrations = graphQLTypes.GetExportedTypes()
                .Where(t => !t.IsAbstract && typeof(GenericGraphQLType<>).IsAssignableFrom(t))
                .Where(t => t.Namespace == "FlightKit.DataAccess.Core.GraphQL.Types");

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
            container.RegisterConditional(typeof(IDbRepository<>), typeof(FlightKitReadonlyDbRepository<>),
                Lifestyle.Scoped,
                context => noTrackChange(context) && !context.Handled);

            container.RegisterConditional(typeof(IDbRepository<>), typeof(FlightKitDbRepository<>),
                Lifestyle.Scoped,
                config => !config.Handled);

            container.Collection.Register(typeof(IDbRepository<>), 
                typeof(Risk_Report).Assembly
                .ExportedTypes.Where(typeof(IEntityWithSyncMetadata<Risk_SyncMetadata>).IsAssignableFrom)
                .Select(t => Lifestyle.Scoped
                .CreateRegistration(typeof(IDbRepository<>).MakeGenericType(t), container))
                );
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
    }
}
