using GCast.Protocol.Discovery.Args;

namespace GCast.Protocol.Discovery;

public class Events
{
    public delegate void OnDeviceDiscovered(object sender, DeviceDiscoveredEventArgs args);
}