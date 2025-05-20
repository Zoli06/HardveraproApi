using AngleSharp;
using HardveraproApi.Ad;
using HardveraproApi.Constants;

namespace HardveraproApi.Search;

public class Search
{
    public BasicAd[] Ads { get; }
    public int TotalResults { get; }
    public int Offset { get; }
    public int ResultsPerPage { get; }

    public int CurrentPage => Offset / ResultsPerPage + 1;
    public int TotalPages => (int)Math.Ceiling((double)TotalResults / ResultsPerPage);
    public bool HasNextPage => CurrentPage < TotalPages;
    public bool HasPreviousPage => CurrentPage > 1;
    
    private Search(BasicAd[] ads, int totalResults, int offset, int resultsPerPage)
    {
        Ads = ads;
        TotalResults = totalResults;
        Offset = offset;
        ResultsPerPage = resultsPerPage;
    }
    
    public static Search FromSearchQuery(string query)
    {
        var builder = new UriBuilder(Routes.Search)
        {
            Query = $"stext={query}"
        };
        
        using var client = new HttpClient();
        var response = client.GetAsync(builder.Uri).Result;
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch search results: {response.ReasonPhrase}");
        }
        var content = response.Content.ReadAsStringAsync().Result;
        var context = BrowsingContext.New(Configuration.Default);
        var document = context.OpenAsync(req => req.Content(content)).Result;
        var ads = document.QuerySelectorAll(".uad-list li.media").Select(BasicAd.FromSearchResultElement).ToArray();
        
        return new Search(ads, 0, 0, 0);
    }
}