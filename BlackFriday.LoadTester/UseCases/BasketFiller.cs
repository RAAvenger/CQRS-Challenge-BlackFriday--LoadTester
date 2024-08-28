using System.Text.Json.Serialization;
using BlackFriday.LoadTester.Commons.Metrics;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class BasketFiller : IBasketFiller
{
	private const string RequestUri = "add-item-to-basket";
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ApiCallMeter _meter;

	public BasketFiller(IHttpClientFactory httpClientFactory, ApiCallMeter meter)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		_meter = meter ?? throw new ArgumentNullException(nameof(meter));
	}

	public async Task FillBasketAsync(string userId, string basketId, IReadOnlyCollection<string> itemIds, CancellationToken cancellationToken)
	{
		var itemsCount = Random.Shared.Next(Math.Min(itemIds.Count, 1), Math.Min(itemIds.Count, 30));
		var randomItems = itemIds.OrderBy(x => Random.Shared.Next()).Take(itemsCount);
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		foreach (var itemId in randomItems)
		{
			var response = await httpClient.PostAsJsonAsync(RequestUri,
				new AddItemToBasketRequestDto
				{
					BasketId = basketId,
					UserId = userId,
					ProductId = itemId,
				},
				cancellationToken);
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

	private sealed record AddItemToBasketRequestDto
	{
		[JsonPropertyName("product-id")]
		public string ProductId { get; set; }

		[JsonPropertyName("user-id")]
		public string UserId { get; set; }

		[JsonPropertyName("basket-id")]
		public string BasketId { get; set; }
	}
}
