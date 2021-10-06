using HotChocolate.ApolloFederationExtension.Scalars;
using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Directives
{
    public class KeyDirective
    {
        [GraphQLType(typeof(FieldSetScalarType))]
        public string Fields { get; set; }
    }

    public class KeyDirectiveType : DirectiveType<KeyDirective>
    {
        protected override void Configure(IDirectiveTypeDescriptor<KeyDirective> descriptor)
        {
            descriptor.Name("key");
            descriptor.Location(DirectiveLocation.Object | DirectiveLocation.Interface);
            descriptor.Repeatable();
        }
    }
}