using HotChocolate.Types;
using System.Collections.Generic;

namespace HotChocolate.ApolloFederationExtension.Unions
{
    [UnionType("_Entity")]
    public interface IEntityUnionType
    {
        IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKeys);
    }
}