using Microsoft.CommandPalette.Extensions.Toolkit;
using Raindrops.Models;

namespace Raindrops.Extensions;

internal static class RaindropExtensions
{
    public static ListItem ToListItem(this Raindrop item)
    {
        IconInfo icon = new(GetFaviconIconUrl(item.Link));

        return new(new OpenUrlCommand(item.Link))
        {
            Title = item.Title,
            Subtitle = item.Link,
            Icon = icon,
            MoreCommands = [
                new CommandContextItem(new CopyTextCommand(item.Link)) { Title = "Copy Link" },
                new CommandContextItem(new OpenUrlCommand(item.Link)) { Title = "Open Link" }],
        };
    }

    private static string GetFaviconIconUrl(string link)
    {
        var domain = new Uri(link).AbsoluteUri;
        return $"https://www.google.com/s2/favicons?domain={domain}&sz=128";
    }

}