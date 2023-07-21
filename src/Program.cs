using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// New instance of CosmosClient class
using CosmosClient client = new(
    accountEndpoint: Environment.GetEnvironmentVariable("COSMOS_ENDPOINT")!,
    authKeyOrResourceToken: Environment.GetEnvironmentVariable("COSMOS_KEY")!
);

// Database reference with creation if it does not already exist
Database database = await client.CreateDatabaseIfNotExistsAsync(id: "cosmicworks");

Console.WriteLine($"New database:\t{database.Id}");

// Container reference with creation if it does not already exist
Container container = await database.CreateContainerIfNotExistsAsync(
    id: "products",
    partitionKeyPath: "/categoryId",
    throughput: 400
);

Console.WriteLine($"New container:\t{container.Id}");


Console.WriteLine($"The configuration value is {app.Configuration["test"]}");
app.Urls.Add("http://*:3000");
app.MapGet("/test", () => app.Configuration["test"]);

app.MapGet("/", () => "Hello World!");

app.MapGet("/cpu", () => {
    Stopwatch stopwatch = Stopwatch.StartNew();
    Thread.SpinWait(100*1000000);
    return stopwatch.Elapsed.TotalMilliseconds;
});

app.MapGet("/createItem", async () => {
// Create new object and upsert (create or replace) to container
Product newItem = new(
    id: "70b63682-b93a-4c77-aad2-65501347265f",
    categoryId: "61dba35b-4f02-45c5-b648-c6badc0cbd79",
    categoryName: "gear-surf-surfboards",
    name: "Yamba Surfboard",
    quantity: 12,
    sale: false
);

Product createdItem = await container.CreateItemAsync<Product>(
    item: newItem,
    partitionKey: new PartitionKey("61dba35b-4f02-45c5-b648-c6badc0cbd79")
);

return $"Created item:\t{createdItem.id}\t[{createdItem.categoryName}]";
});

app.Run();

// C# record representing an item in the container
public record Product(
    string id,
    string categoryId,
    string categoryName,
    string name,
    int quantity,
    bool sale
);
