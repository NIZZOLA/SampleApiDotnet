using Store.Api.Endpoints;
using Store.Application;
using Microsoft.AspNetCore.RateLimiting;
using Store.Api;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Starting App With Environment: {builder.Environment.EnvironmentName}");

var concurrencyPolicy = "Concurrency";
var myOptions = new RateLimitOptions();
builder.Configuration.GetSection(RateLimitOptions.MyRateLimit).Bind(myOptions);

builder.Services.AddRateLimiter(_ => _
    .AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
    {
        options.PermitLimit = myOptions.PermitLimit;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = myOptions.QueueLimit;
    }));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);

//builder.Services.Configure<MongoDbConfiguration>(
//    builder.Configuration.GetSection("MongoDbConfiguration"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();
app.UseHttpsRedirection();

app.MapStorePostRequestModelEndpoints();

app.Run();
