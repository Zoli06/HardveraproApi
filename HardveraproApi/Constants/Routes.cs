namespace HardveraproApi.Constants;

public static class Routes
{
    private static Uri Base => new("https://hardverapro.hu/");
    public static Uri Search => new(Base, "aprok/keres.php");
}