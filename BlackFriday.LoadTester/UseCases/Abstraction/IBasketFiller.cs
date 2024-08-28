namespace BlackFriday.LoadTester.UseCases.Abstraction;

internal interface IBasketFiller
{
	Task FillBasketAsync(string userId, string basketId, IReadOnlyCollection<string> itemIds, CancellationToken cancellationToken);
}
