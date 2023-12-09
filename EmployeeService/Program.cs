using AspNetCoreRateLimit;
using EmployeeService.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using static EmployeeService.DAL.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

// Cosmos DB settings
var cosmosDbSettings = new CosmosDbSettings
{
    EndpointUrl = configuration["CosmosDbSettings:Endpointurl"],
    PrimaryKey = configuration["CosmosDbSettings:PrimaryKey"],
    DatabaseName = configuration["CosmosDbSettings:DatabaseName"]
};

builder.Services.AddSingleton((provider) =>
{
    var endPointUrl = cosmosDbSettings.EndpointUrl;
    var primaryKey = cosmosDbSettings.PrimaryKey;
    var databaseName = cosmosDbSettings.DatabaseName;
    var cosmosClientOption = new CosmosClientOptions
    {
        ApplicationName = databaseName
    };
    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
    var cosmosClient = new CosmosClient(endPointUrl, primaryKey, cosmosClientOption);
    cosmosClient.ClientOptions.ConnectionMode = ConnectionMode.Direct;
    return cosmosClient;
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));


builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>(); ;
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>()    ;

builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();
