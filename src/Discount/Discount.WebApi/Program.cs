using Discount.Domain;
using Discount.Domain.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDiscounts(builder.Configuration.GetConnectionString("DiscountDb"))
    .AddScoped<DiscountDatabaseMigrator>()
    .AddControllers();

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

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var migrator = scope.ServiceProvider.GetRequiredService<DiscountDatabaseMigrator>();
migrator.MigrateAsync().GetAwaiter().GetResult();

app.Run();
