using HotChocolate.ApolloFederationExtension.Directives;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;

namespace HotChocolate.ApolloFederationExtension.Attributes
{
    public class FederationObjectDirective : ObjectTypeDescriptorAttribute
    {
        public string Name { get; set; }

        public string Fields { get; set; }

        public override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        {
            switch (Name)
            {
                case "extends":
                    descriptor.Directive<ExtendsDirectiveType>();
                    break;

                case "key":
                    descriptor.Directive(new KeyDirective() { Fields = Fields });
                    break;

                default:
                    descriptor.Directive<ExtendsDirectiveType>();
                    break;
            }
        }
    }
}