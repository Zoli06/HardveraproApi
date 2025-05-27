using HardveraproApi.Models.User;

namespace HardveraproApi.Models.Ad;

public class DetailedAd
{
    public required string[] Cities { get; init; }
    public required Uri CoverImage { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required bool IsFrozen { get; init; }
    public required Uri Link { get; init; }
    public required int? Price { get; init; }
    public required SearchResultUser Seller { get; init; }
    public required string Title { get; init; }
    public required AdType Type { get; init; }
    public required bool IsClassified { get; init; }
    public required string? Brand { get; init; }
    public required AdCondition Condition { get; init; }
    public required string Description { get; init; }
    public required Uri[] Images { get; init; }
    public required DateTimeOffset? LastBump { get; init; }
    public required bool WillingToShip { get; init; }
}