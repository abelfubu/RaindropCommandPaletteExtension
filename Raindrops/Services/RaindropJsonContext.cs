using System.Text.Json.Serialization;
using Raindrops.Models;

namespace Raindrops.Services;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true, WriteIndented = true)]
[JsonSerializable(typeof(RaindropResponse))]
internal sealed partial class RaindropJsonContext : JsonSerializerContext
{
}

