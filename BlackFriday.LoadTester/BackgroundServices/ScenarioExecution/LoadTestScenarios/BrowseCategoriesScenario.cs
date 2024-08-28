using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.LoadTestScenarios;

internal class BrowseCategoriesScenario : ILoadTestScenario
{
	private readonly ICategoriesGetter _categoriesGetter;
	private readonly ICategoriesItemsGetter _categoriesItemsGetter;

	public BrowseCategoriesScenario(ICategoriesGetter categoriesGetter, ICategoriesItemsGetter categoriesItemsGetter)
	{
		_categoriesGetter = categoriesGetter ?? throw new ArgumentNullException(nameof(categoriesGetter));
		_categoriesItemsGetter = categoriesItemsGetter ?? throw new ArgumentNullException(nameof(categoriesItemsGetter));
	}

	public LoadTestScenarioType TestScenarioType => LoadTestScenarioType.BrowseCategories;

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		var categories = await _categoriesGetter.GetCategoriesAsync(cancellationToken);
		await _categoriesItemsGetter.GetCategoriesItemsAsync(categories, cancellationToken);
	}
}