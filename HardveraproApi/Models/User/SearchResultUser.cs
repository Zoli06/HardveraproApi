namespace HardveraproApi.Models.User;

public class SearchResultUser
{
    public required string Name { get; init; }
    public required int PositiveRating { get; init; }
    public required int NegativeRating { get; init; }
}