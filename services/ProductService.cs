using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp.models;

namespace BlazorApp.services;

public class ProductService
{
  private readonly HttpClient _client;

  private readonly JsonSerializerOptions _options;

  public ProductService(HttpClient client, JsonSerializerOptions options)
  {
    _client = client;
    _options = options;
  }

  public async Task<List<Product>> Get()
  {
    var response = await _client.GetAsync("v1/products");
    return await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync()) ?? [];
  }

  public async Task<Product?> Add(Product newProduct)
  {
    var response = await _client.PostAsync("v1/products", JsonContent.Create(newProduct));
    // var content = await response.Content.ReadAsStringAsync();

    return response.IsSuccessStatusCode ? newProduct : null;
  }

  public async Task<Product?> Update(int productId, Product product)
  {
    var response = await _client.PutAsync($"v1/products/{productId}", JsonContent.Create(product));
    
    return response.IsSuccessStatusCode ? product : null;
  }
  
  public async Task<bool> Remove(int productId)
  {
    var response = await _client.DeleteAsync($"v1/products/{productId}");

    return response.IsSuccessStatusCode;
  }
}
