using Store.Api.Endpoints;
using Store.Application;
using Microsoft.AspNetCore.RateLimiting;
using Store.Api;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Starting App With Environment: {builder.Environment.EnvironmentName}");

builder.Services.AddRateLimiter(limiterOptions =>
{
    limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    limiterOptions.AddFixedWindowLimiter(policyName: RateLimitOptions.MyRateLimit, options =>
    {
        options.PermitLimit = 3;
        options.Window = TimeSpan.FromSeconds(5);
    });
});

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