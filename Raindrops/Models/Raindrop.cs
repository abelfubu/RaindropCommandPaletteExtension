using System;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Raindrops.Models;

public class Raindrop
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string[] Tags { get; set; } = [];

    public static ListItem ToListItem(Raindrop item)
    {
        IconInfo icon = new(GetFaviconIconUrl(item.Link));

        return new ListItem(new OpenUrlCommand(item.Link))
        {
            Title = item.Title,
            Subtitle = item.Link,
            Icon = icon,
            MoreCommands = [
                new CommandContextItem(new CopyTextCommand(item.Link)) { Title = "Copy Link" },
                new CommandContextItem(new OpenUrlCommand(item.Link)) { Title = "Open Link" },
            ],
        };
    }

    private static string GetFaviconIconUrl(string link)
    {
        var domain = new Uri(link).AbsoluteUri;
        return $"https://www.google.com/s2/favicons?domain={domain}&sz=526";
    }
}
