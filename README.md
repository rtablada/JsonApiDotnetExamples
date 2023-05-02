# JSON API Dotnet Core Error Examples

## Requirements

1. Dotnet 7
2. Postgres running on local host (or configure the `ExampleDb` EF connection string to point to a Postgres server)

> **NOTE** the startup of this example will drop and rebuild the specified database, so connect to a known server with a new DB or you could lose data. I recommend mounting a base docker pg instance or similar to test with.

## Running this project:

1. Clone this repo and install any dotnet core dependencies using your IDE or from the CLI using `dotnet restore`
2. Run `dotnet run` from CLI or similar from your IDE of choice

## 1. OwnsOne and `include`

When using OwnsOne to have component or nested tabular data, endpoints work as expected until includes or sparse fieldsets are used.

Example