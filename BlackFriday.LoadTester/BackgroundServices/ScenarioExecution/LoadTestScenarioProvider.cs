using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Enums;
using BlackFriday.LoadTester.Exceptions;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution;

internal class LoadTestScenarioProvider : ILoadTestScenarioProvider
{
	private readonly Dictionary<LoadTestScenarioType, ILoadTestScenario> _scenarios;

	public LoadTestScenarioProvider(IEnumerable<ILoadTestScenario> loadTestScenarios)
	{
		_scenarios = loadTestScenarios.ToDictionary(x => x.TestScenarioType);
	}

	public ILoadTestScenario MakeScenario(LoadTestScenarioType scenarioType)
	{
		if (_scenarios.TryGetValue(scenarioType, out var scenario))
		{
			return scenario;
		}
		throw new LoadTestScenarioNotFoundException($"Scenario {scenarioType} not found.");
	}
}