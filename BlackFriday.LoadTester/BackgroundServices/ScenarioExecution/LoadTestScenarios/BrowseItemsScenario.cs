using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.LoadTestScenarios;

internal class BrowseItemsScenario : ILoadTestScenario
{
	private readonly ICategoriesGetter _categoriesGetter;
	private readonly ICategoriesItemsGetter _categoriesItemsGetter;
	private readonly IItemsGetter _itemsGetter;

	public BrowseItemsScenario(ICategoriesGetter categoriesGetter,
		ICategoriesItemsGetter categoriesItemsGetter,
		IItemsGetter itemsGetter)
	{
		_categoriesGetter = categoriesGetter ?? throw new ArgumentNullException(nameof(categoriesGetter));
		_categoriesItemsGetter = categoriesItemsGetter ?? throw new ArgumentNullException(nameof(categoriesItemsGetter));
		_itemsGetter = itemsGetter ?? throw new ArgumentNullException(nameof(itemsGetter));
	}

	public LoadTestScenarioType TestScenarioType => LoadTestScenarioType.BrowseItems;

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var categories = await _categoriesGetter.GetCategoriesAsync(cancellationToken);
		var itemIds = await _categoriesItemsGetter.GetCategoriesItemsAsync(categories, cancellationToken);
		await _itemsGetter.GetItemsAsync(itemIds, cancellationToken);
	}
}
