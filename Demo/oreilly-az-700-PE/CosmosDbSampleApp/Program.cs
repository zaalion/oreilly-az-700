using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Cosmos Client
builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string account = configuration["CosmosDb:Account"];
    string key = configuration["CosmosDb:Key"];
    return new CosmosClient(account, key);
});

// Add CosmosDbService
builder.Services.AddSingleton<CosmosDbService>(serviceProvider =>
{
    var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    string databaseId = configuration["CosmosDb:DatabaseId"];
    string containerId = configuration["CosmosDb:ContainerId"];
    return new CosmosDbService(cosmosClient, databaseId, containerId);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
