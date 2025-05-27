using AngleSharp.Dom;
using HardveraproApi.Models.Search;
using HardveraproApi.Parsers.Ad;

namespace HardveraproApi.Parsers.Search;

internal static class SearchResultParser
{
    public static SearchResult Parse(IElement element)
    {
        var ads = element.QuerySelectorAll(".uad-list li.media").Select(SearchResultAdParserHelpers.Parse).ToArray();
        var prevPage = element.QuerySelector("a[rel=\"prev\"]")?.Attributes["href"]!.Value;
        var nextPage = element.QuerySelector("a[rel=\"next\"]")?.Attributes["href"]!.Value;

        return new SearchResult
        {
            Ads = ads,
            PreviousPage = prevPage != null ? new Uri(prevPage, UriKind.RelativeOrAbsolute) : null,
            NextPage = nextPage != null ? new Uri(nextPage, UriKind.RelativeOrAbsolute) : null
        };
    }
}