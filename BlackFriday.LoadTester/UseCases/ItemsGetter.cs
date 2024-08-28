using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class ItemsGetter : IItemsGetter
{
	private readonly IHttpClientFactory _httpClientFactory;

	public ItemsGetter(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	public async Task GetItemsAsync(IReadOnlyCollection<string> itemIds, CancellationToken cancellationToken)
	{
		var itemsCount = Random.Shared.Next(Math.Min(itemIds.Count, 14), Math.Min(itemIds.Count, 50));
		var randomItems = itemIds.OrderBy(x => Random.Shared.Next()).Take(itemsCount);
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		foreach (var itemId in randomItems)
		{
			var response = await httpClient.GetAsync($"items/{itemId}", cancellationToken);
			response.EnsureSuccessStatusCode();
		}
	}
}
