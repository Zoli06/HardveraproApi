namespace HardveraproApi.User;

public interface IBasicUser
{
    public string Name { get; }
    public int PositiveRating { get; }
    public int NegativeRating { get; }
}