# Disco

Disco is a .NET 7.0 project that follows the Clean Architecture pattern for each microservice, including Barcodes, Companies, Discounts, Points, and Users. It also includes the Disco.Shared project, which contains shared code for MongoDB, RabbitMQ, Consul, Fabio, and authentication and authorization.

## Microservices

Each microservice in Disco follows the Clean Architecture pattern, with the following structure:

### Barcodes

The Barcodes microservice is structured as follows:

- `Barcodes.API`: contains the API layer for the Barcodes microservice.
- `Barcodes.Application`: contains the application layer for the Barcodes microservice.
- `Barcodes.Core`: contains the core domain logic for the Barcodes microservice.
- `Barcodes.Infrastructure`: contains the infrastructure layer for the Barcodes microservice, including implementations for external dependencies.
- `Barcodes.Domain`: contains tests for domain logic for the Barcodes microservice.
- `Barcodes.IntegrationTests`: contains integration tests for the Barcodes microservice.

### Companies

The Companies microservice is structured as follows:

- `Companies.API`: contains the API layer for the Companies microservice.
- `Companies.Application`: contains the application layer for the Companies microservice.
- `Companies.Core`: contains the core domain logic for the Companies microservice.
- `Companies.Infrastructure`: contains the infrastructure layer for the Companies microservice, including implementations for external dependencies.
- `Companies.Domain`: contains tests for domain logic for the Companies microservice.
- `Companies.IntegrationTests`: contains integration tests for the Companies microservice.

### Discounts

The Discounts microservice is structured as follows:

- `Discounts.API`: contains the API layer for the Discounts microservice.
- `Discounts.Application`: contains the application layer for the Discounts microservice.
- `Discounts.Core`: contains the core domain logic for the Discounts microservice.
- `Discounts.Infrastructure`: contains the infrastructure layer for the Discounts microservice, including implementations for external dependencies.
- `Discounts.Domain`: contains tests for domain logic for the Discounts microservice.
- `Discounts.IntegrationTests`: contains integration tests for the Discounts microservice.

### Points

The Points microservice is structured as follows:

- `Points.API`: contains the API layer for the Points microservice.
- `Points.Application`: contains the application layer for the Points microservice.
- `Points.Core`: contains the core domain logic for the Points microservice.
- `Points.Infrastructure`: contains the infrastructure layer for the Points microservice, including implementations for external dependencies.
- `Points.Domain`: contains tests for domain logic for the Points microservice.
- `Points.IntegrationTests`: contains integration tests for the Points microservice.

### Users

The Users microservice is structured as follows:

- `Users.API`: contains the API layer for the Users microservice.
- `Users.Application`: contains the application layer for the Users microservice.
- `Users.Core`: contains the core domain logic for the Users microservice.
- `Users.Infrastructure`: contains the infrastructure layer for the Users microservice, including implementations for external dependencies.
- `Users.Domain`: contains tests for domain logic for the Users microservice.
- `Users.IntegrationTests`: contains integration tests for the Users microservice.

## Disco.Shared

The Disco.Shared project contains shared code for MongoDB, RabbitMQ, Consul, Fabio, and authentication and authorization, and is structured as follows:

- `Disco.Shared.Mongo`: contains the implementation for MongoDB.
- `Disco.Shared.RabbitMQ`: contains the implementation for RabbitMQ.
- `Disco.Shared.Consul`: contains the implementation for Consul.
- `Disco.Shared.Fabio`: contains the implementation for Fabio.
- `Disco.Shared.Auth`: contains the implementation for authentication and authorization


### Docker

The Disco project also includes a docker-compose.yml file, which can be used to run the required Docker infrastructure, including MongoDB, RabbitMQ, Consul, Fabio. Simply navigate to the root directory of the project and run `docker-compose up` to start the infrastructure.

