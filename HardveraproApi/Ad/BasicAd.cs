using System.Globalization;
using AngleSharp.Dom;
using HardveraproApi.User;

namespace HardveraproApi.Ad;

public class BasicAd : IBasicAd
{
    public string Title { get; }
    public int? Price { get; }
    public Uri CoverImage { get; }
    public IBasicUser Seller { get; }
    public string[] Cities { get; }
    public DateTime CreatedAt { get; }
    public Uri Link { get; }
    
    private BasicAd(string title, int? price, Uri coverImage, IBasicUser seller, string[] cities, DateTime createdAt, Uri link)
    {
        Title = title;
        Price = price;
        CoverImage = coverImage;
        Seller = seller;
        Cities = cities;
        CreatedAt = createdAt;
        Link = link;
    }
    
    public static BasicAd FromSearchResultElement(IElement element)
    {
        var titleElement = element.QuerySelector(".uad-col-title h1 a")!;
        var title = titleElement.TextContent;
        var link = new Uri(titleElement.GetAttribute("href")!);
        int? price = int.TryParse(element.QuerySelector(".uad-price span")!.TextContent, NumberStyles.Currency,
            new CultureInfo("hu-HU"), out var priceInt)
            ? priceInt
            : null;
        var coverImage = new Uri(element.QuerySelector(".uad-image img")!.GetAttribute("src")!);
        var seller = BasicUser.FromProfileElement(element.QuerySelector(".uad-user")!);
        var cities = element.QuerySelector(".uad-cities")!.TextContent.Split(", ");
        var createdAtString = element.QuerySelector(".uad-time")!.TextContent;
        DateTime createdAt;
        if (createdAtString.Contains("ma"))
        {
            var time = createdAtString.Split().Last();
            createdAt = DateTime.Today.Add(TimeSpan.Parse(time));
        }
        else if (createdAtString.Contains("tegnap"))
        {
            var time = createdAtString.Split().Last();
            createdAt = DateTime.Today.AddDays(-1).Add(TimeSpan.Parse(time));
        }
        else
        {
            createdAt = DateTime.Parse(createdAtString.Split().Last());
        }

        return new BasicAd(title, price, coverImage, seller, cities, createdAt, link);
    }
}