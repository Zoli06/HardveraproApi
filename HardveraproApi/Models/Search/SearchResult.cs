using HardveraproApi.Models.Ad;

namespace HardveraproApi.Models.Search;

public class SearchResult
{
    public required SearchResultAd[] Ads { get; init; }

    public required Uri? NextPage { get; init; }
    public required Uri? PreviousPage { get; init; }
}