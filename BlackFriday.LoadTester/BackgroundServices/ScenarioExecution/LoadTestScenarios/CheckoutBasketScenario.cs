using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.LoadTestScenarios;

internal class CheckoutBasketScenario : ILoadTestScenario
{
	private readonly IBasketCheckouter _basketCheckouter;
	private readonly IBasketFiller _basketFiller;
	private readonly ICategoriesGetter _categoriesGetter;
	private readonly ICategoriesItemsGetter _categoriesItemsGetter;
	private readonly IItemsGetter _itemsGetter;

	public CheckoutBasketScenario(ICategoriesGetter categoriesGetter,
		ICategoriesItemsGetter categoriesItemsGetter,
		IItemsGetter itemsGetter,
		IBasketFiller basketFiller,
		IBasketCheckouter basketCheckouter)
	{
		_categoriesGetter = categoriesGetter ?? throw new ArgumentNullException(nameof(categoriesGetter));
		_categoriesItemsGetter = categoriesItemsGetter ?? throw new ArgumentNullException(nameof(categoriesItemsGetter));
		_itemsGetter = itemsGetter ?? throw new ArgumentNullException(nameof(itemsGetter));
		_basketFiller = basketFiller ?? throw new ArgumentNullException(nameof(basketFiller));
		_basketCheckouter = basketCheckouter ?? throw new ArgumentNullException(nameof(basketCheckouter));
	}

	public LoadTestScenarioType TestScenarioType => LoadTestScenarioType.CheckoutBasket;

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var categories = await _categoriesGetter.GetCategoriesAsync(cancellationToken);
		var itemIds = await _categoriesItemsGetter.GetCategoriesItemsAsync(categories, cancellationToken);
		await _itemsGetter.GetItemsAsync(itemIds, cancellationToken);
		var userId = Guid.NewGuid().ToString();
		var basketId = Guid.NewGuid().ToString();
		await _basketFiller.FillBasketAsync(userId, basketId, itemIds, cancellationToken);
		await _basketCheckouter.CheckoutBasketAsync(userId, basketId, cancellationToken);
	}
}
