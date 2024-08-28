using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution;

internal class DiceRoller : IDiceRoller
{
	public LoadTestScenarioType RollTheDice()
	{
		return Random.Shared.Next(1, 6) switch
		{
			1 => LoadTestScenarioType.GetCategories,
			2 => LoadTestScenarioType.BrowseCategories,
			3 => LoadTestScenarioType.BrowseItems,
			4 => LoadTestScenarioType.AddToBasket,
			5 => LoadTestScenarioType.CheckoutBasket,
			_ => throw new NotSupportedException(),
		};
	}
}