using HardveraproApi.Models.Search;
using HardveraproApi.Parsers;

namespace HardveraproApi.Clients;

public class SearchClient(HttpClient httpClient, IParser<SearchResult> searchResultParser) : ClientBase(httpClient)
{
    public async Task<SearchResult> SearchAsync(SearchQuery query)
    {
        return searchResultParser.Parse(await GetHtmlAsync(Endpoints.Search(query)));
    }
}