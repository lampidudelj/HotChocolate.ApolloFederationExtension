using HotChocolate.ApolloFederationExtension.Directives;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;

namespace HotChocolate.ApolloFederationExtension.Attributes
{
    public class ExtendAttribute : ObjectTypeDescriptorAttribute
    {
        public override void OnConfigure(IDescriptorContext context, IObjectTypeDescriptor descriptor, Type type)
        {
            descriptor.Directive<ExtendsDirectiveType>();
        }
    }
}