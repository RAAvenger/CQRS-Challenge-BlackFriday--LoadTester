using BlackFriday.LoadTester.Commons.Metrics;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.UseCases;

internal class CategoriesGetter : ICategoriesGetter
{
	private const string RequestUri = "categories";
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ApiCallMeter _meter;

	public CategoriesGetter(IHttpClientFactory httpClientFactory, ApiCallMeter meter)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		_meter = meter ?? throw new ArgumentNullException(nameof(meter));
	}

	public async Task<IReadOnlyCollection<string>> GetCategoriesAsync(CancellationToken cancellationToken)
	{
		using var httpClient = _httpClientFactory.CreateClient("black_friday");
		var response = await httpClient.GetAsync(RequestUri, cancellationToken);
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
		return await response.Content.ReadFromJsonAsync<IReadOnlyCollection<string>>(cancellationToken);
	}
}
