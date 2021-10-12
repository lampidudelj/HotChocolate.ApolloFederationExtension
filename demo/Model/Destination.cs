using HotChocolate.Types;

namespace HotChocolate.ApolloFederationExtension.Demo.Model
{
    public record Destination([property: GraphQLType(typeof(NonNullType<IdType>))] int Id, string Name)
    {
        public static Destination GetById(int destinationId)
        {
            return new Destination(destinationId, "Foo");
        }
    }
}
