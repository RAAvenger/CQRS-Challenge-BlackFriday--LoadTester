using System.Text.Json.Serialization;
using BlackFriday.LoadTester.Commons.Metrics;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class BasketCheckouter : IBasketCheckouter
{
	private const string RequestUri = "checkout-basket";
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ApiCallMeter _meter;

	public BasketCheckouter(IHttpClientFactory httpClientFactory, ApiCallMeter meter)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		_meter = meter ?? throw new ArgumentNullException(nameof(meter));
	}

	public async Task CheckoutBasketAsync(string userId, string basketId, CancellationToken cancellationToken)
	{
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		var response = await httpClient.PostAsJsonAsync(RequestUri,
			new CheckoutBasketRequestDto
			{
				BasketId = basketId,
				UserId = userId,
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

	private sealed record CheckoutBasketRequestDto
	{
		[JsonPropertyName("user-id")]
		public string UserId { get; set; }

		[JsonPropertyName("basket-id")]
		public string BasketId { get; set; }
	}
}
