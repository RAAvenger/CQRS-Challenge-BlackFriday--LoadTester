using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;

internal interface ILoadTestScenarioProvider
{
	ILoadTestScenario MakeScenario(LoadTestScenarioType scenarioType);
}