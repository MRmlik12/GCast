using GCast.Protocol.Discovery.Models;

namespace GCast.Protocol.Discovery.Args;

public class DeviceDiscoveredEventArgs : EventArgs
{
    public DeviceModel DeviceModel { get; set; } = null!;
}