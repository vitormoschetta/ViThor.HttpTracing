# LoggingFilter

A package that logging all your HTTP requests and responses using dotnet filters.  

It also adds tracing support, linking requests and responses with a unique identifier.

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



### How to use

Add the package to your project:  

```shell
dotnet add package ViThor.LoggingFilter
```


Add in `Program.cs`:  

```csharp
using Vithor.LoggingFilter.Filters;

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LoggingFilter>();
});
```
