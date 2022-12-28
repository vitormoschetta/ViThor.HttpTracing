using ViThor.HttpTracing.Filters;
using ViThor.HttpTracing.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add ViThorTraceFilter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ViThorTraceFilter>();
});

// Add Suporte to HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add Suporte to HttpClientFactory
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add ViThorExceptionHandlingMiddleware (optional)
app.UseMiddleware<ViThorExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
