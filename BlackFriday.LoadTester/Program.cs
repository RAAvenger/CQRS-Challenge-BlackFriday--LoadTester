// See https://aka.ms/new-console-template for more information
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.Abstraction;
using BlackFriday.LoadTester.BackgroundServices.ScenarioExecution.LoadTestScenarios;
using BlackFriday.LoadTester.UseCases;
using BlackFriday.LoadTester.UseCases.Abstraction;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("LOADTESTER_");

builder.Services
	.AddOpenTelemetry()
	.WithMetrics(options => options.AddHttpClientInstrumentation()
		.AddRuntimeInstrumentation()
		.AddProcessInstrumentation()
		.AddPrometheusExporter());

builder.Services.AddHttpClient("black_friday", options =>
{
	options.BaseAddress = new Uri(builder.Configuration.GetValue("BLACKFRIDAY_SERVER_URI", "http://localhost:32768"));
});

builder.Services.AddHostedService<ScenarioExecutorBackgroundService>();

builder.Services.AddSingleton<IDiceRoller, DiceRoller>();
builder.Services.AddSingleton<ILoadTestScenarioProvider, LoadTestScenarioProvider>();

builder.Services.AddSingleton<ILoadTestScenario, GetCategoriesScenario>();
builder.Services.AddSingleton<ILoadTestScenario, BrowseCategoriesScenario>();
builder.Services.AddSingleton<ILoadTestScenario, BrowseItemsScenario>();
builder.Services.AddSingleton<ILoadTestScenario, FillBasketScenario>();
builder.Services.AddSingleton<ILoadTestScenario, CheckoutBasketScenario>();

builder.Services.AddSingleton<ICategoriesGetter, CategoriesGetter>();
builder.Services.AddSingleton<ICategoriesItemsGetter, CategoriesItemsGetter>();
builder.Services.AddSingleton<IItemsGetter, ItemsGetter>();
builder.Services.AddSingleton<IBasketFiller, BasketFiller>();
builder.Services.AddSingleton<IBasketCheckouter, BasketCheckouter>();

var app = builder.Build();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.Run();
