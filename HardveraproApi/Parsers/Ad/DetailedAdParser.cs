using AngleSharp.Dom;
using HardveraproApi.Models.Ad;
using HardveraproApi.Models.User;

namespace HardveraproApi.Parsers.Ad;

// TODO: Make a different parser for detailed ads
internal class DetailedAdParser(IParser<SearchResultUser> userParser) : AdParser<DetailedAd>
{
    public override DetailedAd Parse(IElement element)
    {
        var title = element.QuerySelector(".uad h1")!.TextContent.Trim();

        var detailsDivs = element.QuerySelectorAll(".uad-details div");
        var detailsTableCells = detailsDivs[2].QuerySelectorAll("td");
        var priceText = detailsDivs[0].TextContent;
        var adType = ParseAdType(priceText, detailsTableCells[1].TextContent);
        var price = ParsePrice(priceText);
        var condition = ParseCondition(detailsTableCells[0].TextContent);
        var brand = detailsTableCells.Length > 2 ? detailsTableCells[2].TextContent : null;
        var seller = userParser.Parse(element.QuerySelector(".uad div:nth-child(2) table")!);
        var createdAt = ParseDateAndTime(element.QuerySelector("span:has(.fa-clock)")!.TextContent);
        var cities = element.QuerySelector("span:has(.fa-map-marker)")!.TextContent.Trim().Split(", ");
        var willingToShip = element.QuerySelector(".fa-truck") != null;
        var isFrozen = element.QuerySelector(".fa-snowflake") != null;
        var link = new Uri(element.QuerySelector("meta[property='og:url']")!.Attributes["content"]!.Value);
        var description = element.QuerySelector(".mgt0")!.TextContent;
        var lastBumpElement = element.QuerySelector("span:has(.fa-arrow-alt-to-top)");
        var lastBump = lastBumpElement != null
            ? DateTimeOffset.Now - ParseElapsedTime(lastBumpElement.TextContent)
            : (DateTimeOffset?)null;
        var images = element.QuerySelectorAll("#uad-images-carousel .carousel-inner img")
            .Select(img => new Uri(img.GetAttribute("src")!))
            .ToArray();
        var coverImage = images[0];
        var isClassified = element.QuerySelector(".fa-sort-amount-up") != null;

        return new DetailedAd
        {
            Title = title,
            Price = price,
            CoverImage = coverImage,
            Seller = seller,
            Cities = cities,
            CreatedAt = createdAt,
            Link = link,
            IsFrozen = isFrozen,
            Type = adType,
            WillingToShip = willingToShip,
            Condition = condition,
            Brand = brand,
            Description = description,
            Images = images,
            LastBump = lastBump,
            IsClassified = isClassified
        };
    }

    private static AdCondition ParseCondition(string conditionText)
    {
        return conditionText switch
        {
            "új" => AdCondition.New,
            "használt" => AdCondition.Used,
            _ => throw new ArgumentException("Unknown condition")
        };
    }
}