# LoggingFilter

A small package for tracing HTTP requests between dotnet microservices.

Sample of the log:

```shell
info: Vithor.Filters.LoggingFilter[0]
      [ced6398d-36a2-4f0c-80c2-c78e91274d0d] Request from [Username/Anonymous] to Controller/Action with query parameters: [] and arguments: {
        "id": 5
      }
info: Vithor.Filters.LoggingFilter[0]
      [ced6398d-36a2-4f0c-80c2-c78e91274d0d] Response from [Username/Anonymous] to Controller/Action with result: {
        "id": 5,
        "name": "Test",
        "isComplete": true
      }
```

`[ced6398d-36a2-4f0c-80c2-c78e91274d0d]` is a tracking code that can be received in the header with the term `X-Correlation-ID`.

If an `X-Correlation-ID` is not passed then this library will generate a random value to track request and response.


## Installation

Install the package from NuGet:

```bash
dotnet add package ViThor.HttpTracing
```


## Usage

Add in `Program.cs`:  

```csharp
using Vithor.HttpTracing.Filters;

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ViThorTraceFilter>();
});
```


## Sample 

### Running the sample

```bash
dotnet run --project ViThor.HttpTracing.Sample/ViThor.HttpTracing.Sample.csproj
```

### Testing the sample

#### Swagger

The sample project has Swagger configured. To access it, run the API and access the following URL:

```bash
http://localhost:5000/swagger/index.html
```

#### Postman

The example project has a Postman collection that can be used to test endpoints. To use it, import the collections from the `Postman` folder into Postman and run the requests.


## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

[MIT](https://choosealicense.com/licenses/mit/)