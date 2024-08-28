using BlackFriday.LoadTester.Commons.Metrics;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class ItemsGetter : IItemsGetter
{
	private const string RequestUri = "items/{itemId}";
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ApiCallMeter _meter;

	public ItemsGetter(IHttpClientFactory httpClientFactory, ApiCallMeter meter)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		_meter = meter ?? throw new ArgumentNullException(nameof(meter));
	}

	public async Task GetItemsAsync(IReadOnlyCollection<string> itemIds, CancellationToken cancellationToken)
	{
		var itemsCount = Random.Shared.Next(Math.Min(itemIds.Count, 14), Math.Min(itemIds.Count, 50));
		var randomItems = itemIds.OrderBy(x => Random.Shared.Next()).Take(itemsCount);
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		foreach (var itemId in randomItems)
		{
			var response = await httpClient.GetAsync($"items/{itemId}", cancellationToken);
			try
			{
				response.EnsureSuccessStatusCode();
				_meter.CalledApiSuccessfully(RequestUri);
			}
			catch
			{
				_meter.ApiCallFailed(RequestUri);
				throw;
			}
		}
	}
}
