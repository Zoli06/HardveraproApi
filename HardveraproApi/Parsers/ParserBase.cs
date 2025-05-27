using System.Globalization;
using AngleSharp.Dom;

namespace HardveraproApi.Parsers;

internal abstract class ParserBase<T> : IParser<T>
{
    public abstract T Parse(IElement input);
    
    protected static DateTimeOffset ParseDateAndTime(string raw)
    {
        var relevantPart = raw.Split().Last();

        if (raw.Contains("ma")) return DateTimeOffset.Now.Add(TimeSpan.Parse(relevantPart));
        if (raw.Contains("tegnap")) return DateTimeOffset.Now.AddDays(-1).Add(TimeSpan.Parse(relevantPart));

        var localtime = DateTime.Parse(relevantPart, CultureInfo.InvariantCulture);
        var cet = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var offset = cet.GetUtcOffset(localtime);
        return new DateTimeOffset(localtime, offset);
    }

    protected static TimeSpan ParseElapsedTime(string raw)
    {
        var parts = raw.Trim().Split(' ');
        if (parts.Length < 2) throw new ArgumentException("Invalid elapsed time format", nameof(raw));
        var value = int.Parse(parts[0]);
        return parts[1] switch
        {
            "másodperce" => TimeSpan.FromSeconds(value),
            "órája" => TimeSpan.FromHours(value),
            "napja" => TimeSpan.FromDays(value),
            "hónapja" => TimeSpan.FromDays(value * 30), // Approximation for months
            "hete" => TimeSpan.FromDays(value * 7),
            "éve" => TimeSpan.FromDays(value * 365.25), // Approximation for leap years
            _ => throw new ArgumentException("Invalid elapsed time unit", nameof(raw))
        };
    }
}