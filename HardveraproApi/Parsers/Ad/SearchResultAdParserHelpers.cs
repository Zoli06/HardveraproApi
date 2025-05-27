using AngleSharp.Dom;
using HardveraproApi.Models.Ad;
using HardveraproApi.Parsers.User;

namespace HardveraproApi.Parsers.Ad;

internal static class SearchResultAdParserHelpers
{
    public static SearchResultAd Parse(IElement element)
    {
        var titleElement = element.QuerySelector(".uad-col-title h1 a")!;
        var title = titleElement.TextContent;
        var link = new Uri(titleElement.GetAttribute("href")!);
        var priceContainer = element.QuerySelector(".uad-price")!;
        var priceText = priceContainer.QuerySelector("span")!.TextContent;
        var adType = AdParserHelpers.ParseAdType(priceText);
        var price = int.TryParse(priceText, out var priceInt) ? priceInt : (int?)null;
        var isFrozen = priceContainer.QuerySelectorAll("span").Length > 1;
        var coverImage = new Uri(element.QuerySelector(".uad-image img")!.GetAttribute("src")!);
        var seller = SearchResultUserParser.Parse(element.QuerySelector(".uad-user")!);
        var cities = element.QuerySelector(".uad-cities")!.TextContent.Split(", ");
        var createdAtString = element.QuerySelector(".uad-time")!.TextContent;
        var createdAt = ParserHelpers.ParseDateAndTime(createdAtString);
        var isClassified = element.QuerySelector(".fa-sort-amount-up") != null;

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