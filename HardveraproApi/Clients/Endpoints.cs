using HardveraproApi.Models.Search;

namespace HardveraproApi.Clients;

internal static class Endpoints
{
    public static Uri Base => new("https://hardverapro.hu/");

    public static Uri Search(SearchQuery searchQuery)
    {
        return new Uri(
            Base,
            $"aprok/keres.php?stext={Uri.EscapeDataString(searchQuery.Query)}"
        );
    }
}