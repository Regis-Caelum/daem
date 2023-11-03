using System.Text.Json;
using YabieFrontend.IServices;
using YabieFrontend.Models;

namespace YabieFrontend.Services;

public class ProductService : IProductService
{
    public async Task<IEnumerable<Product>> ListAllProducts()
    {
        const string baseUrl = "https://yabie-test.azurewebsites.net/api/";

        // Create an HttpClient instance
        using var client = new HttpClient();

        // Set the base address for the HttpClient
        client.BaseAddress = new Uri(baseUrl);
        client.Timeout = new TimeSpan(0, 1, 0);

        try
        {
            // Send an HTTP GET request to the API endpoint that lists all products
            var response = await client.GetAsync("Product/GetProductModel").ConfigureAwait(false);


            // Check if the request was successful (status code 200)
            if (!response.IsSuccessStatusCode) return new List<Product>();

            // Read the response content as a string
            var jsonContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a list of Product objects
            var products = JsonSerializer.Deserialize<IEnumerable<Product>>(jsonContent);

            return products;

            // Handle the case where the request was not successful
            // You can log the error, throw an exception, or return an empty list
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the request
            // You can log the exception or rethrow it as needed
            System.Diagnostics.Debug.WriteLine("timeout");
            System.Diagnostics.Debug.WriteLine(ex);
            System.Diagnostics.Debug.WriteLine(ex.Message);
            System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            return new List<Product>();
        }
    }

    public async Task<Product> GetProduct(Guid productId)
    {
        const string baseUrl = "https://yabie-test.azurewebsites.net/api/";

        // Create an HttpClient instance
        using var client = new HttpClient();

        // Set the base address for the HttpClient
        client.BaseAddress = new Uri(baseUrl);
        client.Timeout = new TimeSpan(0, 1, 0);

        try
        {
            // Construct the URL with the productId
            string url = $"Product/GetProductModel/{productId}";

            // Send an HTTP GET request to the API endpoint for the specified product
            var response = await client.GetAsync(url).ConfigureAwait(false);

            // Check if the request was successful (status code 200)
            if (!response.IsSuccessStatusCode) return new Product();

            // Read the response content as a string
            var jsonContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a Product object
            var product = JsonSerializer.Deserialize<Product>(jsonContent);

            return product;

            // Handle the case where the request was not successful
            // You can log the error, throw an exception, or return an empty product
        }
        catch (Exception)
        {
            // Handle any exceptions that may occur during the request
            // You can log the exception or rethrow it as needed
            System.Diagnostics.Debug.WriteLine("timeout");
            return new Product();
        }
    }
}