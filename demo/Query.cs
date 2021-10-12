using HotChocolate.ApolloFederationExtension.Demo.Model;
using HotChocolate.Types;
using System.Collections.Generic;

namespace HotChocolate.ApolloFederationExtension.Demo
{
    public class Query
    {
        public List<Hotel> SearchHotelsByDestination([GraphQLType(typeof(NonNullType<IdType>))] int destinationId)
        {
            return Hotel.GetByDestinationId(destinationId);
        }
    }
}