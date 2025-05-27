using AngleSharp.Dom;
using HardveraproApi.Models.Ad;
using HardveraproApi.Models.Search;

namespace HardveraproApi.Parsers.Search;

internal class SearchResultParser(IParser<SearchResultAd> searchResultAdParser) : ParserBase<SearchResult>
{
    public override SearchResult Parse(IElement element)
    {
        var ads = element.QuerySelectorAll(".uad-list li.media").Select(searchResultAdParser.Parse).ToArray();
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