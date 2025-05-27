using AngleSharp.Dom;
using HardveraproApi.Models.Ad;
using HardveraproApi.Models.User;

namespace HardveraproApi.Parsers.Ad;

internal class SearchResultAdParser(IParser<SearchResultUser> userParser) : AdParser<SearchResultAd>
{
    public override SearchResultAd Parse(IElement element)
    {
        var titleElement = element.QuerySelector(".uad-col-title h1 a")!;
        var title = titleElement.TextContent;
        var link = new Uri(titleElement.GetAttribute("href")!);
        var priceContainer = element.QuerySelector(".uad-price")!;
        var priceText = priceContainer.QuerySelector("span")!.TextContent;
        var adType = ParseAdType(priceText);
        var price = ParsePrice(priceText);
        var isFrozen = priceContainer.QuerySelectorAll("span").Length > 1;
        var coverImage = new Uri(element.QuerySelector(".uad-image img")!.GetAttribute("src")!);
        var seller = userParser.Parse(element.QuerySelector(".uad-user")!);
        var cities = element.QuerySelector(".uad-cities")!.TextContent.Split(", ");
        var createdAtString = element.QuerySelector(".uad-time")!.TextContent;
        var isClassified = element.QuerySelector(".fa-sort-amount-up") != null;
        var createdAt = isClassified ? (DateTimeOffset?)null : ParseDateAndTime(createdAtString);

        return new SearchResultAd
        {
            Title = title,
            Price = price,
            CoverImage = coverImage,
            Seller = seller,
            Cities = cities,
            CreatedAt = createdAt,
            Link = link,
            IsFrozen = isFrozen,
            Type = adType,
            IsClassified = isClassified
        };
    }
}