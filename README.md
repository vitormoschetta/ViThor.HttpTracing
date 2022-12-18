# ViThor HTTP Tracing

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

The above filter will retrieve the TraceID , log the request and the response.

If your API has to pass the TraceID to another service, you also need to add the following line in `Program.cs`:

```csharp
builder.Services.AddHttpContextAccessor();
```

And, your controller needs to extend from `ViThorControllerBase`:
  
```csharp
public class TodoController : ViThorControllerBase
{
    public TodoController(IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : base(httpContextAccessor, httpClient)
    {
    }
}
```

The `ViThorControllerBase` will retrieve the TraceID from the header and pass it to the HttpClient.



## Sample 

### Running the sample

```bash
dotnet run --project Samples/ViThor.HttpTracing.Sample.AppOne/ViThor.HttpTracing.Sample.AppOne.csproj
dotnet run --project Samples/ViThor.HttpTracing.Sample.AppTwo/ViThor.HttpTracing.Sample.AppTwo.csproj
```

### Testing the sample

#### Swagger

The sample project has Swagger configured. To access it, run the API and access the following URL:

```bash
http://localhost:5000/swagger/index.html
```

#### Curl

Request without passing the X-Correlation-ID:

```bash
curl --location --request GET 'http://localhost:5000/Todo'
```

Request passing the X-Correlation-ID:

```bash
curl --location --request GET 'http://localhost:5000/Todo' \
--header 'X-Correlation-ID: ced6398d-36a2-4f0c-80c2-c78e91274d0d'
```

You will be able to view the content of the HTTP request and response in both applications, with a unique TraceID.



## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License

[MIT](https://choosealicense.com/licenses/mit/)