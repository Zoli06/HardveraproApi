namespace HardveraproApi.Consts;

public static class Routes
{
    private static Uri BaseUrl => new("https://hardverapro.hu/");
    public static Uri SearchUrl => new(BaseUrl, "aprok/keres.php");
}