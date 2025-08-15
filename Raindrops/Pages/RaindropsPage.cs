// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Raindrops;

internal sealed partial class RaindropsPage : ListPage
{
    private static readonly HttpClient httpClient = new();

    private const string ApiToken = "b2d98cc1-54f5-4d65-a232-409c9d51ca21";

    public RaindropsPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
        Title = "Raindrops";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        // For demo, use synchronous blocking call; you can improve with async/await.
        var bookmarks = GetBookmarksAsync().GetAwaiter().GetResult();

        // If no bookmarks, return placeholder
        if (bookmarks == null || bookmarks.Length == 0)
            return [new ListItem(new NoOpCommand()) { Title = "No bookmarks found." }];

        // Map bookmarks to ListItems
        return [.. bookmarks.Select(bookmark => new ListItem(
            new OpenUrlCommand(bookmark.Link) { Name = bookmark.Title }))
        ];
    }

    private async Task<Raindrop[]> GetBookmarksAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.raindrop.io/rest/v1/raindrops/0");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiToken);

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var raindropResponse = JsonSerializer.Deserialize<RaindropResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return raindropResponse?.Items ?? [];
    }
}

public class RaindropResponse
{
    public int Count { get; set; }
    public Raindrop[] Items { get; set; } = [];
}

public class Raindrop
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string[] Tags { get; set; } = [];
}