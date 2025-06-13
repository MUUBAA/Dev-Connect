namespace dev_connect.Server.Services.Cache
{
    public class CacheSetting(int absoluteExpiration, int slidingExpiration)
    {
        public const int HourInMinutes = 60;
        public const int DayInMinutes = 24 * HourInMinutes;
        public const int WeekInMinutes = 7 * DayInMinutes;

        public TimeSpan AbsoluteExpiration { get; } = TimeSpan.FromMinutes(absoluteExpiration);
        public TimeSpan SlidingExpiration { get; } = TimeSpan.FromMinutes(slidingExpiration);

        public static CacheSetting cacheForThreeHourAndExtendByHalfAndHour = new(HourInMinutes * 3, 30);
    }
}
