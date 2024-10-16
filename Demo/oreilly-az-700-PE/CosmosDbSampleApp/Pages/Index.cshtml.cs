using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly CosmosDbService _cosmosDbService;

    public List<People> People { get; set; }

    public IndexModel(CosmosDbService cosmosDbService)
    {
        _cosmosDbService = cosmosDbService;
    }

    public async Task OnGetAsync()
    {
        People = await _cosmosDbService.GetItemsAsync<People>("SELECT * FROM c");
    }
}
