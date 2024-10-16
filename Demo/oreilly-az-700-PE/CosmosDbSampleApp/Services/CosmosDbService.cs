using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CosmosDbService
{
    private Container _container;

    public CosmosDbService(CosmosClient cosmosClient, string databaseId, string containerId)
    {
        _container = cosmosClient.GetContainer(databaseId, containerId);
    }

    public async Task<List<T>> GetItemsAsync<T>(string query)
    {
        var queryDefinition = new QueryDefinition(query);
        var resultSetIterator = _container.GetItemQueryIterator<T>(queryDefinition);
        List<T> results = new List<T>();
        while (resultSetIterator.HasMoreResults)
        {
            FeedResponse<T> response = await resultSetIterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }
}
