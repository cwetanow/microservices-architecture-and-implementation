using Catalog.WebApi.Data;
using Catalog.WebApi.Routes;
using Catalog.WebApi.Settings;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var settings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

services.AddSingleton(_ => new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName));

services.AddScoped<ICatalog, MongoCatalog>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .AddCatalogRoutes()
    .Run();