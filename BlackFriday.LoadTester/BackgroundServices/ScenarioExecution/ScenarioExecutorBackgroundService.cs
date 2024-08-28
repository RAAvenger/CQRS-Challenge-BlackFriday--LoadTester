using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BlackFriday.LoadTester.BackgroundServices.ScenarioExecution;

internal class ScenarioExecutorBackgroundService : BackgroundService
{
	private readonly IConfiguration _configuration;
	private readonly IDiceRoller _diceRoller;
	private readonly ILoadTestScenarioProvider _scenarioFactory;

	public ScenarioExecutorBackgroundService(ILoadTestScenarioProvider scenarioFactory,
		IDiceRoller diceRoller,
		IConfiguration configuration)
	{
		_scenarioFactory = scenarioFactory ?? throw new ArgumentNullException(nameof(scenarioFactory));
		_diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
		_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var users = new List<Task>();
		foreach (var _ in Enumerable.Range(0, _configuration.GetValue("USERSCOUNT", 5)))
		{
			users.Add(Task.Run(async () =>
			{
				while (!stoppingToken.IsCancellationRequested)
				{
					try
					{
						var scenarioType = _diceRoller.RollTheDice();
						var scenario = _scenarioFactory.MakeScenario(scenarioType);
						await scenario.ExecuteAsync(stoppingToken);
					}
					catch { }
				}
			}, stoppingToken));
		}
		await Task.WhenAll(users);
	}
}
