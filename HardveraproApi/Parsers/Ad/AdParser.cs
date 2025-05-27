using System.Globalization;
using HardveraproApi.Models.Ad;

namespace HardveraproApi.Parsers.Ad;

internal abstract class AdParser<T> : ParserBase<T>
{
    protected static AdType ParseAdType(string priceText, string? intentionText = null)
    {
        var hasPrice = int.TryParse(priceText, NumberStyles.Currency,
            new CultureInfo("hu-HU"), out _);

        return hasPrice && intentionText == "keres" ? AdType.WantedWithPrice :
            hasPrice ? AdType.Offer :
            priceText == "Csere" ? AdType.Exchange :
            priceText == "Ingyenes" ? AdType.Free :
            priceText == "Keresem" ? AdType.Wanted :
            throw new ArgumentException("Unknown ad type");
    }
    
    protected static int? ParsePrice(string priceText)
    {
        return int.TryParse(priceText, NumberStyles.Currency, new CultureInfo("hu-HU"), out var price) 
            ? price 
            : null;
    }
}