using System.Net.Http.Json;
using System.Text.Json.Serialization;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class BasketCheckouter : IBasketCheckouter
{
	private readonly IHttpClientFactory _httpClientFactory;

	public BasketCheckouter(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	public async Task CheckoutBasketAsync(string userId, string basketId, CancellationToken cancellationToken)
	{
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		var response = await httpClient.PostAsJsonAsync("checkout-basket",
			new CheckoutBasketRequestDto
			{
				BasketId = basketId,
				UserId = userId,
			},
			cancellationToken);
		response.EnsureSuccessStatusCode();
	}

	private sealed record CheckoutBasketRequestDto
	{
		[JsonPropertyName("user-id")]
		public string UserId { get; set; }

		[JsonPropertyName("basket-id")]
		public string BasketId { get; set; }
	}
}
