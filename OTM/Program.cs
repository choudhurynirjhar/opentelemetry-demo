using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OTM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IUserProvider>(new UserProvider("Persist Security Info = False; Integrated Security = true; Initial Catalog = Demo; server = .\\SQLEXPRESS"));

var serviceName = "StreetService";

builder.Services.AddOpenTelemetryTracing(b => {
    b.AddConsoleExporter()
    .AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService(serviceName, serviceVersion: "1.0.0")
    )
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddHttpClientInstrumentation();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
