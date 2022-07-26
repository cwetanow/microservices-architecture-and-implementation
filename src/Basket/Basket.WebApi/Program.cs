using Basket.WebApi;
using Basket.WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddStackExchangeRedisCache(opts => opts.Configuration = builder.Configuration["RedisConnectionString"])
    .AddScoped<IBasketData, RedisBasketData>();

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
    .AddBasketRoutes()
    .Run();
