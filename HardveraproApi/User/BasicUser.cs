using AngleSharp.Dom;

namespace HardveraproApi.User;

public class BasicUser : IBasicUser
{
    public string Name { get; }
    public int PositiveRating { get; }
    public int NegativeRating { get; }
    
    private BasicUser(string name, int positiveRating, int negativeRating)
    {
        Name = name;
        PositiveRating = positiveRating;
        NegativeRating = negativeRating;
    }
    
    public static BasicUser FromProfileElement(IElement element)
    {
        var name = element.QuerySelector(".uad-user-text a")!.TextContent;
        var positiveRating = int.Parse(element.QuerySelector(".uad-rating-positive")?.TextContent ?? "0");
        var negativeRating = int.Parse(element.QuerySelector(".uad-rating-negative")?.TextContent ?? "0");

        return new BasicUser(name, positiveRating, negativeRating);
    }
}