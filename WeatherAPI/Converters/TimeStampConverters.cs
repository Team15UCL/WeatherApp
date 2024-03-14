namespace WeatherAPI.Converters;

public static class TimeStampConverters
{
	public static string ConvertToDay(int timestamp)
	{
		return DateTime.UnixEpoch.AddSeconds(timestamp).DayOfWeek.ToString();
	}

	public static string ConvertToTime(int timestamp)
	{
		return DateTime.UnixEpoch.AddSeconds(timestamp).ToShortTimeString();
	}

	public static int ConvertToUnix(this DateTime dateTime)
	{
		DateTimeOffset dateTimeOffset = new(dateTime.ToUniversalTime());
		return (int)dateTimeOffset.ToUnixTimeSeconds();
	}
}
