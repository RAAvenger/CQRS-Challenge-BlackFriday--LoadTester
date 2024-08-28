namespace BlackFriday.LoadTester.UseCases.Abstraction;

internal interface IItemsGetter
{
	Task GetItemsAsync(IReadOnlyCollection<string> itemIds, CancellationToken cancellationToken);
}
