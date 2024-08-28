using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;

internal interface IDiceRoller
{
	LoadTestScenarioType RollTheDice();
}