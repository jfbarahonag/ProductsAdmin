using System.Text.Json;
using BlazorApp.Models;

namespace BlazorApp.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _client;
    
    private readonly JsonSerializerOptions _options;
    
    public CategoryService(HttpClient client, JsonSerializerOptions options)
    {
        _client = client;
        _options = options;
    }

    public async Task<List<Category>> Get()
  {
    var response = await _client.GetAsync("v1/categories");
    var content = await response.Content.ReadAsStreamAsync();
    return await JsonSerializer.DeserializeAsync<List<Category>>(content, _options) ?? [];
  }

}

public interface ICategoryService 
{
  Task<List<Category>> Get();
}
