using HotChocolate.ApolloFederationExtension.Attributes;
using HotChocolate.ApolloFederationExtension.Unions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;

namespace HotChocolate.ApolloFederationExtension.Demo.Model
{
    [FederationObjectDirective(Name = "key", Fields = "id")]
    [FederationObjectDirective(Name = "extends")]
    public record Hotel([property: FederationFieldDirective(Name = "external")]
                        [property: GraphQLType(typeof(NonNullType<IdType>))]
                        int Id,
                        Destination Destination) : IEntityUnionType
    {
        [GraphQLIgnore]
        public IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKey)
        {
            if (primaryKey.Key == "id")
            {
                return GetById((int)primaryKey.Value);
            }

            return null;
        }

        public static Hotel GetById(int id)
        {
            return new Hotel(id, Destination.GetById(new Random().Next()));
        }

        public static List<Hotel> GetByDestinationId(int destinationId)
        {
            List<Hotel> result = new List<Hotel>();

            for (int i = 0; i <= new Random().Next(); i++)
            {
                result.Add(new Hotel(new Random().Next(), Destination.GetById(destinationId)));
            }

            return result;
        }
    }
}