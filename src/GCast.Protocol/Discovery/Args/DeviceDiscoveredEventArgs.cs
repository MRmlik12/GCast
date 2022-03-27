using GCast.Protocol.Discovery.Models;

namespace GCast.Protocol.Discovery.Args;

public class DeviceDiscoveredEventArgs : EventArgs
{
    public DiscoveryDeviceModel Device { get; set; } = null!;
}