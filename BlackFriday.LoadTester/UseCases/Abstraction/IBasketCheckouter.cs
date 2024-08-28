namespace BlackFriday.LoadTester.UseCases.Abstraction;

internal interface IBasketCheckouter
{
	Task CheckoutBasketAsync(string userId, string basketId, CancellationToken cancellationToken);
}
