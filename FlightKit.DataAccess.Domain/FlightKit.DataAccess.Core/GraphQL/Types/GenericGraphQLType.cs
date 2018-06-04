using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public abstract class GenericGraphQLType<TApplication> : ObjectGraphType<TApplication>
    {
        public GenericGraphQLType()
        {
            Name = typeof(TApplication).Name.ToCamelCase();
            Description = $"Risk {Name}";

            var allPrimitivProperties = typeof(TApplication).GetProperties().Where(p =>
            {
                var t = p.PropertyType;

                if (t == typeof(string))
                {
                    return true;
                }

                if (t.IsConstructedGenericType)
                {
                    return !typeof(ICollection<>).IsAssignableFrom(t.GetGenericTypeDefinition())
                        && !t.GetGenericArguments().Any(g => g == typeof(Guid));
                }

                return t != typeof(Guid) && !t.IsArray && !t.IsClass;
            });

            var idProperties = typeof(TApplication).GetProperties().Where(p => p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?));

            foreach (var primitiveProperty in allPrimitivProperties)
            {
                var (underlineType, isNullable) = IfNullableType(primitiveProperty.PropertyType);
                if (underlineType == typeof(short))
                {
                    underlineType = typeof(int);
                }

                Type graphType = underlineType.GetGraphTypeFromType(isNullable);
                Field(graphType, primitiveProperty.Name.ToCamelCase(),
                    resolve: context => primitiveProperty.GetValue(context.Source));
            }

            foreach (var idProperty in idProperties)
            {
                var (underlineType, isNullable) = IfNullableType(idProperty.PropertyType);
                Field<IdGraphType>(idProperty.Name.ToCamelCase(),
                    resolve: context => idProperty.GetValue(context.Source));
            }
        }

        private (Type, bool) IfNullableType(Type t)
        {
            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return (t.GetGenericArguments()[0], true);
            }

            return (t, false);
        }
    }
}
