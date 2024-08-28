using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;

internal interface ILoadTestScenario
{
	LoadTestScenarioType TestScenarioType { get; }

	Task ExecuteAsync(CancellationToken cancellationToken);
}