using HardveraproApi.Models.User;

namespace HardveraproApi.Models.Ad;

public class SearchResultAd
{
    public required string[] Cities { get; init; }
    public required Uri CoverImage { get; init; }
    public required DateTimeOffset? CreatedAt { get; init; }
    public required bool IsFrozen { get; init; }
    public required Uri Link { get; init; }
    public required int? Price { get; init; }
    public required SearchResultUser Seller { get; init; }
    public required string Title { get; init; }
    public required AdType Type { get; init; }
    public required bool IsClassified { get; init; }
}