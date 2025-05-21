using AngleSharp;
using AngleSharp.Dom;
using HardveraproApi.Ad;
using HardveraproApi.Constants;

namespace HardveraproApi.Search;

public class Search
{
    private Search(BasicAd[] ads, int totalResults, int offset, int resultsPerPage)
    {
        Ads = ads;
        TotalResults = totalResults;
        Offset = offset;
        ResultsPerPage = resultsPerPage;
    }

    public BasicAd[] Ads { get; }
    public int TotalResults { get; }
    public int Offset { get; }
    public int ResultsPerPage { get; }

    public int CurrentPage => Offset / ResultsPerPage + 1;
    public int TotalPages => (int)Math.Ceiling((double)TotalResults / ResultsPerPage);
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;

    public static Search FromSearchQuery(string query, int offset = 0)
    {
        var builder = new UriBuilder(Routes.Search)
        {
            Query = $"stext={query}&offset={offset}"
        };

        using var client = new HttpClient();
        var response = client.GetAsync(builder.Uri).Result;
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Failed to fetch search results: {response.ReasonPhrase}");
        var content = response.Content.ReadAsStringAsync().Result;
        var context = BrowsingContext.New(Configuration.Default);
        var document = context.OpenAsync(req => req.Content(content)).Result;
        var ads = document.QuerySelectorAll(".uad-list li.media").Select(BasicAd.FromSearchResultElement).ToArray();

        var totalResults =
            int.TryParse(document.QuerySelector(".uad-list li")!.TextContent.Split(' ')[0], out var outTotalResults)
                ? outTotalResults
                : 0;
        
        var resultsPerPage = int.Parse(document.QuerySelector("div:has(#forum-nav-top) ul:last-child li:first-child a b:first-child")!.TextContent);
        
        return new Search(ads, totalResults, offset, resultsPerPage);
    }
}