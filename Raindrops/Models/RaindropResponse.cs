namespace Raindrops.Models;

public class RaindropResponse
{
    public int Count { get; set; }
    public Raindrop[] Items { get; set; } = [];
}