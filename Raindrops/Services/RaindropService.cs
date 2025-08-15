using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Models;

namespace Raindrops.Services;

internal sealed class RaindropService
{
    private static readonly HttpClient http = new();
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<ListItem[]> GetBookmarksAsync(
        string raindropApiKey,
        string searchTerm,
        CancellationToken cancellationToken)
    {
        var request = CreateRequest(searchTerm, raindropApiKey);
        var response = await http.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        var raindropResponse = JsonSerializer.Deserialize(json, RaindropJsonContext.Default.RaindropResponse);

        return MapToListItems(raindropResponse);
    }

    private static HttpRequestMessage CreateRequest(string searchTerm, string raindropApiKey)
    {
        var url = $"https://api.raindrop.io/rest/v1/raindrops/0?search={Uri.EscapeDataString(searchTerm)}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", raindropApiKey);

        return request;
    }

    private static ListItem[] MapToListItems(RaindropResponse? response)
    {
        return [.. (response?.Items ?? []).Select(Raindrop.ToListItem)];
    }
}