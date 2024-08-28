using System.Net.Http.Json;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class CategoriesItemsGetter : ICategoriesItemsGetter
{
	private readonly IHttpClientFactory _httpClientFactory;

	public CategoriesItemsGetter(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	public async Task<IReadOnlyCollection<string>> GetCategoriesItemsAsync(IReadOnlyCollection<string> categories, CancellationToken cancellationToken)
	{
		var categoriesCount = Random.Shared.Next(3, 10);
		var randomCategories = categories.OrderBy(x => Random.Shared.Next()).Take(categoriesCount);
		var itemIds = new List<string>();
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		foreach (var category in randomCategories)
		{
			var response = await httpClient.GetAsync($"categories/{category}", cancellationToken);
			response.EnsureSuccessStatusCode();
			var products = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Product>>(cancellationToken);
			itemIds.AddRange(products.Select(x => x.Asin));
		}

		return itemIds;
	}

	private sealed class Product
	{
		public string Asin { get; set; }
		public long BoughtInLastMonth { get; set; }
		public string CategoryName { get; set; }
		public string ImgUrl { get; set; }
		public bool IsBestSeller { get; set; }
		public decimal Price { get; set; }
		public string ProductUrl { get; set; }
		public long Reviews { get; set; }
		public decimal Stars { get; set; }
		public string Title { get; set; }
	}
}
