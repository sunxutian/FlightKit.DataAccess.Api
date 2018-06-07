using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlightKit.DataAccess.Core.GraphQL.Types
{
    public abstract class GenericGraphQLType<TApplication> : ObjectGraphType<TApplication>
    {
        public GenericGraphQLType()
        {
            Name = typeof(TApplication).Name.ToCamelCase();

            var properties = typeof(TApplication)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => (p.PropertyType.GetTypeInfo().IsValueType
                        && p.PropertyType.GetTypeInfo() != typeof(Guid)
                        && p.PropertyType.GetTypeInfo() != typeof(Guid?))
                || p.PropertyType == typeof(string));

            foreach (var propertyInfo in properties)
            {
                var (underlineType, isNullable) = IfNullableType(propertyInfo.PropertyType);
                underlineType = underlineType == typeof(short) ? typeof(int) : underlineType;

                Field(underlineType.GetGraphTypeFromType(isNullable),
                    propertyInfo.Name.ToCamelCase());
            }

            var idProperties = typeof(TApplication).GetProperties().Where(p => p.PropertyType == typeof(Guid) || p.PropertyType == typeof(Guid?));


            foreach (var idProperty in idProperties)
            {
                var (underlineType, isNullable) = IfNullableType(idProperty.PropertyType);
                Field<IdGraphType>(idProperty.Name.ToCamelCase(),
                    resolve: context => idProperty.GetValue(context.Source));
            }
        }

        private (Type, bool) IfNullableType(Type t)
        {
            if (t.IsNullable())
            {
                return (t.GetGenericArguments()[0], true);
            }

            return (t, false);
        }
    }
}
