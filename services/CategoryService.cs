using System.Text.Json;
using BlazorApp.models;

namespace BlazorApp.services;

public class CategoryService
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
    return await JsonSerializer.DeserializeAsync<List<Category>>(await response.Content.ReadAsStreamAsync()) ?? [];
  }

}
