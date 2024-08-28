namespace BlackFriday.LoadTester.UseCases.Abstraction;

internal interface ICategoriesGetter
{
	Task<IReadOnlyCollection<string>> GetCategoriesAsync(CancellationToken cancellationToken);
}
