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
    
    private BasicAd(string title, int? price, Uri coverImage, IBasicUser seller)
    {
        Title = title;
        Price = price;
        CoverImage = coverImage;
        Seller = seller;
    }
    
    public static BasicAd FromSearchResultElement(IElement element)
    {
        var title = element.QuerySelector(".uad-col-title h1 a")!.TextContent;
        int? price = int.TryParse(element.QuerySelector(".uad-price span")!.TextContent, NumberStyles.Currency,
            new CultureInfo("hu-HU"), out var priceInt)
            ? priceInt
            : null;
        var coverImage = new Uri(element.QuerySelector(".uad-image img")!.GetAttribute("src")!);
        var seller = BasicUser.FromProfileElement(element.QuerySelector(".uad-user")!);

        return new BasicAd(title, price, coverImage, seller);
    }
}