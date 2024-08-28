namespace BlackFriday.LoadTester.UseCases.Abstraction;

internal interface ICategoriesItemsGetter
{
	Task<IReadOnlyCollection<string>> GetCategoriesItemsAsync(IReadOnlyCollection<string> categories, CancellationToken cancellationToken);
}
