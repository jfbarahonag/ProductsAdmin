using System.Net.Http.Json;
using System.Text.Json;
using BlazorApp.Models;

namespace BlazorApp.Services;

public class ProductService : IProductService
{
  private readonly HttpClient _client;

  private readonly JsonSerializerOptions _options;

  public ProductService(HttpClient client)
  {
    _client = client;
    _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };;
  }

  public async Task<List<Product>> Get()
  {
    var response = await _client.GetAsync("v1/products");
    var content = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<List<Product>>(content, _options) ?? [];
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

public interface IProductService
{
  Task<List<Product>> Get();

  Task<Product?> Add(Product newProduct);

  Task<Product?> Update(int productId, Product product);

  Task<bool> Remove(int productId);
}
