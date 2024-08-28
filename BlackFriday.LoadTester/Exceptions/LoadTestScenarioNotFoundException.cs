namespace BlackFriday.LoadTester.Exceptions;

[Serializable]
public class LoadTestScenarioNotFoundException : Exception
{
	public LoadTestScenarioNotFoundException()
	{
	}

	public LoadTestScenarioNotFoundException(string? message) : base(message)
	{
	}

	public LoadTestScenarioNotFoundException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}
