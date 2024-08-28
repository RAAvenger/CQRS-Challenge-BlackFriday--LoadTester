using System.Net.Http.Json;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class CategoriesGetter : ICategoriesGetter
{
	private readonly IHttpClientFactory _httpClientFactory;

	public CategoriesGetter(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	public async Task<IReadOnlyCollection<string>> GetCategoriesAsync(CancellationToken cancellationToken)
	{
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		var response = await httpClient.GetAsync($"categories/", cancellationToken);
		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<string>>(cancellationToken);
	}
}
