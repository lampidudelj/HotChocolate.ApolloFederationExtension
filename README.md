# HotChocolate Apollo Federation Extension
[![CI](https://github.com/lampidudelj/HotChocolate.ApolloFederationExtension/actions/workflows/ci.yml/badge.svg?branch=master)](https://github.com/lampidudelj/HotChocolate.ApolloFederationExtension/actions/workflows/ci.yml)

An extension adding GraphQL federation support to HotChocolate framework based on [Apollo Federation Spec](https://www.apollographql.com/docs/federation/federation-spec/)

## Getting Started

Add the federated extension to the service configuration by adding the following code to the `ConfigureServices` method in the `Startup.cs`.

 ```c#
    services
        .AddGraphQLServer()
        .AddFederationExtensions()
        .AddQueryType<Query>()
```

This will extend your server and schema with [Fetch Service capability](https://www.apollographql.com/docs/federation/federation-spec/#fetch-service-capabilities) and custom federation scalars, unions and directives

## Using annotation to extend types

The extension provides both Object and Field level directives to generate federation spec compliant schema
For example, following class will generate:

```c#
    [FederationObjectDirective(Name = "key", Fields = "id")]
    [FederationObjectDirective(Name = "extends")]
    public record Hotel : IEntityUnionType
    {
        public Hotel(string Id)
        {
            this.Id = Id;
        }

        [FederationFieldDirective(Name = "external")]
        [GraphQLType(typeof(NonNullType<IdType>))]
        public string Id { get; init; }

        [GraphQLIgnore]
        public IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKey)
        {
            if (primaryKey.Key == "id")
            {
                //fetch a hotel based on the id
                return new Hotel(primaryKey.Value.ToString());
            }

            return null;
        }
    }
```

following schema

```graphql
    type Hotel @key(fields: "id") @extends {
        id: ID! @external
    }
```

### Key points

- any type annotated with the **@key** directive, including both types native to the schema and extended types, must implement `IEntityUnionType` interface as it serves as a representation of the federation [UnionEntity](https://www.apollographql.com/docs/federation/federation-spec/#union-_entity)
- `public IEntityUnionType ResolveReference(KeyValuePair<string, object> primaryKey)` method is a resolver for a query across service boundaries. It should return an entity based on the key specified in the schema and provided as an input parameter. [More on how this works](https://www.apollographql.com/docs/federation/federation-spec/#resolve-requests-for-entities)
- `[GraphQLIgnore]` attribute **must** annotate  `ResolveReference` method. HotChocolate's schema builder engine can not serialize  `KeyValuePair` into a GraphQL scalar and will crash trying.


## TODO

- implement [@provides](https://www.apollographql.com/docs/federation/federation-spec/#provides) and [@requires](https://www.apollographql.com/docs/federation/federation-spec/#requires) directives.
