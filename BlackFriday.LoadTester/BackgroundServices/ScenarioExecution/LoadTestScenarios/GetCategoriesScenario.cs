using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;
using BlackFriday.LoadTester.UseCases.Abstraction;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.LoadTestScenarios;

internal class GetCategoriesScenario : ILoadTestScenario
{
	private readonly ICategoriesGetter _categoriesGetter;

	public GetCategoriesScenario(ICategoriesGetter categoriesGetter)
	{
		_categoriesGetter = categoriesGetter ?? throw new ArgumentNullException(nameof(categoriesGetter));
	}

	public LoadTestScenarioType TestScenarioType => LoadTestScenarioType.GetCategories;

	public async Task ExecuteAsync(CancellationToken cancellationToken)
	{
		await _categoriesGetter.GetCategoriesAsync(cancellationToken);
	}
}