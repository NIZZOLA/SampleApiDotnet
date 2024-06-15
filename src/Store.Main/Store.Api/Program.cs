using Store.Api.Endpoints;
using Store.Infra.Data.Configuration;
using Store.Application;
using Store.Business;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Starting App With Environment: {builder.Environment.EnvironmentName}");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection("MongoDbConfiguration"));

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapStorePostRequestModelEndpoints();

app.Run();

