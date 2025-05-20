using HardveraproApi.User;

namespace HardveraproApi.Ad;

public interface IBasicAd
{
    public string Title { get; }
    public int? Price { get; }
    public Uri CoverImage { get; }
    public IBasicUser Seller { get; }
}