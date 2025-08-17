using System;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Raindrops.Models;

public class Raindrop
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string[] Tags { get; set; } = [];
}
