using Newtonsoft.Json;

namespace GCast.Protocol.Discovery.Models;

public class DeviceModel
{
    [JsonProperty("bssid")] public string? BssId { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
    
    [JsonProperty("ip_address")] public string? IpAddress { get; set; }

    [JsonProperty("mac_address")] public string? MacAddress { get; set; }

    [JsonProperty("public_key")] public string? PublicKey { get; set; }

    [JsonProperty("build_version")] public string? BuildVersion { get; set; }

    [JsonProperty("version")] public int Version { get; set; }

    [JsonProperty("uptime")] public decimal Uptime { get; set; }

    public DiscoveryDeviceModel? DiscoveryModel { get; set; }
}