using AngleSharp.Dom;
using HardveraproApi.Models.User;

namespace HardveraproApi.Parsers.User;

internal static class SearchResultUserParser
{
    public static SearchResultUser Parse(IElement element)
    {
        var name = element.QuerySelector("a")!.TextContent;
        var positiveRating = int.Parse(element.QuerySelector(".uad-rating-positive")?.TextContent.Split(' ')[0] ?? "0");
        var negativeRating = int.Parse(element.QuerySelector(".uad-rating-negative")?.TextContent.Split(' ')[0] ?? "0");

        return new SearchResultUser
        {
            Name = name,
            PositiveRating = positiveRating,
            NegativeRating = negativeRating
        };
    }
}