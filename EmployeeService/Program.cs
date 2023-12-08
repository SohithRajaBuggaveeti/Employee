using EmployeeService.Data;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddSingleton((provider) =>
{
    var endPointUrl = configuration["CosmosDBSettings:Endpointurl"];
    var primaryKey = configuration["CosmosDBSettings:PrimaryKey"];
    var databaseName = configuration["CosmosDBSettings:DatabaseName"];
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

builder.Services.AddControllers();

builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>()    ;

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
