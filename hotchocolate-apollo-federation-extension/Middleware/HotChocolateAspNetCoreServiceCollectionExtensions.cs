using HotChocolate.ApolloFederationExtension.Directives;
using HotChocolate.ApolloFederationExtension.Queries;
using HotChocolate.Execution.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HotChocolateAspNetCoreServiceCollectionExtensions
    {
        public static IRequestExecutorBuilder AddFederationExtensions(this IRequestExecutorBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.AddDirectiveType<ExtendsDirectiveType>()
                          .AddDirectiveType<KeyDirectiveType>()
                          .AddDirectiveType<ExternalDirectiveType>()
                          .AddDirectiveType<ProvidesDirectiveType>()
                          .AddDirectiveType<RequiresDirectiveType>()
                          .AddTypeExtension<FederationQueryExtensions>();
        }
    }
}