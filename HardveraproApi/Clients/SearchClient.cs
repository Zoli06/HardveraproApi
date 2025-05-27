using HardveraproApi.Models.Search;
using HardveraproApi.Parsers.Search;

namespace HardveraproApi.Clients;

public class SearchClient(HttpClient httpClient) : ClientBase(httpClient)
{
    public async Task<SearchResult> SearchAsync(SearchQuery query)
    {
        return SearchResultParser.Parse(await GetHtmlAsync(Endpoints.Search(query)));
    }
}