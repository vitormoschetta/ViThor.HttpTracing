using ViThor.HttpTracing.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Add Suporte to HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add ViThorTraceFilter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ViThorTraceFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();