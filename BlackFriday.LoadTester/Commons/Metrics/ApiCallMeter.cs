using System.Diagnostics.Metrics;

namespace BlackFriday.LoadTester.Commons.Metrics;

public class ApiCallMeter
{
	private readonly Counter<int> _counter;

	public ApiCallMeter(IMeterFactory meterFactory)
	{
		ArgumentNullException.ThrowIfNull(meterFactory);
		var meter = meterFactory.Create("BlackFriday.LoadTester");
		_counter = meter.CreateCounter<int>(nameof(ApiCallMeter));
	}

	public void ApiCallFailed(string api)
	{
		_counter.Add(1, new KeyValuePair<string, object?>("api", api), new KeyValuePair<string, object?>("success", false));
	}

	public void CalledApiSuccessfully(string api)
	{
		_counter.Add(1, new KeyValuePair<string, object?>("api", api), new KeyValuePair<string, object?>("success", true));
	}
}
