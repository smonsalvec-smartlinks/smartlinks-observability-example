using APITest;
using CountriesProvider;
using Infrastructure;
using Mapster;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Observability.Application.Commands;
using Observability.Application.Queries;
using Observability.Domain;
using PlaceHolderProvider;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ITelemetryInitializer, CorrelationTelemetryInitializer>();
builder.Services.AddSingleton<ITelemetryInitializer, SessionTelemetryInitializer>();

builder.Services.AddControllers();
builder.Services.AddMapster();
builder.Services.AddTransient<ICountryQueryService, CountryQueryService>();
builder.Services.AddTransient<IPlaceHolderQueryService, PlaceHolderQueryService>();
builder.Services.AddTransient<IPlaceHolderCommandService, PlaceHolderCommandService>();
builder.Services.AddTransient<ICountryProvider, CountryProvider>();
builder.Services.AddTransient<IPlaceHolderProvider, PlaceHoldersProvider>();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ContainerServices.TelemetryClient = app.Services.GetService<TelemetryClient>();

// Configure the HTTP request pipeline.
if( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestTelemetryMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();