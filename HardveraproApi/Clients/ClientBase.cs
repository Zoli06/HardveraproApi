using AngleSharp;
using AngleSharp.Dom;

namespace HardveraproApi.Clients;

public class ClientBase
{
    private HttpClient HttpClient { get; }
    protected ClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;
        HttpClient.BaseAddress = Endpoints.Base;
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "HardveraproApi/1.0");
    }

    protected async Task<IElement> GetHtmlAsync(Uri uri)
    {
        var response = await HttpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var context = BrowsingContext.New(Configuration.Default);
        var document = await context.OpenAsync(req => req.Content(content));
        if (document is null)
        {
            throw new InvalidOperationException("Failed to parse HTML document.");
        }
        var element = document.QuerySelector("html");
        if (element is null)
        {
            throw new InvalidOperationException("Failed to find the root HTML element.");
        }
        return element;
    }
}