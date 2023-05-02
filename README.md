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

Example:

`curl http://localhost:5097/countries`

```json
{
  "links": {
    "self": "http://localhost:5097/countries",
    "first": "http://localhost:5097/countries"
  },
  "data": [
    {
      "type": "countries",
      "id": "1",
      "attributes": {
        "name": "United States",
        "statistics": {
          "population": 334702010,
          "gdp": 21427000,
          "perCapita": 15.620572641993746208055257386
        }
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/1/relationships/region",
            "related": "http://localhost:5097/countries/1/region"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/1"
      }
    },
    {
      "type": "countries",
      "id": "2",
      "attributes": {
        "name": "Mexico",
        "statistics": {
          "population": 129830000,
          "gdp": 1272839,
          "perCapita": 102.00033154232389170979204754
        }
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/2/relationships/region",
            "related": "http://localhost:5097/countries/2/region"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/2"
      }
    },
    {
      "type": "countries",
      "id": "3",
      "attributes": {
        "name": "United Kingdon",
        "statistics": {
          "population": 67830000,
          "gdp": 3131000,
          "perCapita": 21.664005110188438198658575535
        }
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/3/relationships/region",
            "related": "http://localhost:5097/countries/3/region"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/3"
      }
    },
    {
      "type": "countries",
      "id": "4",
      "attributes": {
        "name": "Sweeden",
        "statistics": {
          "population": 10830000,
          "gdp": 635000,
          "perCapita": 17.055118110236220472440944882
        }
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/4/relationships/region",
            "related": "http://localhost:5097/countries/4/region"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/4"
      }
    }
  ]
}
```

With Includes: `curl http://localhost:5097/countries?include=region` (statistics is always set to null)

```json
{
  "links": {
    "self": "http://localhost:5097/countries?include=region",
    "first": "http://localhost:5097/countries?include=region"
  },
  "data": [
    {
      "type": "countries",
      "id": "1",
      "attributes": {
        "name": "United States",
        "statistics": null
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/1/relationships/region",
            "related": "http://localhost:5097/countries/1/region"
          },
          "data": {
            "type": "regions",
            "id": "1"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/1"
      }
    },
    {
      "type": "countries",
      "id": "2",
      "attributes": {
        "name": "Mexico",
        "statistics": null
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/2/relationships/region",
            "related": "http://localhost:5097/countries/2/region"
          },
          "data": {
            "type": "regions",
            "id": "1"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/2"
      }
    },
    {
      "type": "countries",
      "id": "3",
      "attributes": {
        "name": "United Kingdon",
        "statistics": null
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/3/relationships/region",
            "related": "http://localhost:5097/countries/3/region"
          },
          "data": {
            "type": "regions",
            "id": "2"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/3"
      }
    },
    {
      "type": "countries",
      "id": "4",
      "attributes": {
        "name": "Sweeden",
        "statistics": null
      },
      "relationships": {
        "region": {
          "links": {
            "self": "http://localhost:5097/countries/4/relationships/region",
            "related": "http://localhost:5097/countries/4/region"
          },
          "data": {
            "type": "regions",
            "id": "2"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/countries/4"
      }
    }
  ],
  "included": [
    {
      "type": "regions",
      "id": "1",
      "attributes": {
        "name": "North America"
      },
      "relationships": {
        "countries": {
          "links": {
            "self": "http://localhost:5097/regions/1/relationships/countries",
            "related": "http://localhost:5097/regions/1/countries"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/regions/1"
      }
    },
    {
      "type": "regions",
      "id": "2",
      "attributes": {
        "name": "Europe"
      },
      "relationships": {
        "countries": {
          "links": {
            "self": "http://localhost:5097/regions/2/relationships/countries",
            "related": "http://localhost:5097/regions/2/countries"
          }
        }
      },
      "links": {
        "self": "http://localhost:5097/regions/2"
      }
    }
  ]
}
```

With `fields`: `curl http://localhost:5097/countries?fields[countries]=statistics`

```json
{
  "links": {
    "self": "http://localhost:5097/countries?fields%5Bcountries%5D=statistics",
    "first": "http://localhost:5097/countries?fields%5Bcountries%5D=statistics"
  },
  "errors": [
    {
      "id": "bb6a352d-dc92-4c06-8800-bebdc33d1152",
      "status": "500",
      "title": "An unhandled error occurred while processing this request.",
      "detail": "A tracking query is attempting to project an owned entity without a corresponding owner in its result, but owned entities cannot be tracked without their owner. Either include the owner entity in the result or make the query non-tracking using 'AsNoTracking'."
    }
  ]
}
```