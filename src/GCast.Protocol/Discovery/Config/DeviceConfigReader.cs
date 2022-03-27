using GCast.Protocol.Discovery.Models;

namespace GCast.Protocol.Discovery.Config;

internal static class DeviceConfigReader
{
    public static DiscoveryDeviceModel GetDeviceModel(List<string> data)
    {
        var discoveryDeviceModel = new DiscoveryDeviceModel();

        foreach (var str in data)
        {
            var (key, value) = GetKeyAndValue(str);
            discoveryDeviceModel = WriteValueToProperty(ref discoveryDeviceModel, key, value);
        }

        return discoveryDeviceModel;
    }

    private static DiscoveryDeviceModel WriteValueToProperty(ref DiscoveryDeviceModel discoveryDeviceModel, string key,
        string value)
    {
        switch (key)
        {
            case "id":
                discoveryDeviceModel.Id = value;
                break;
            case "md":
                discoveryDeviceModel.DeviceModel = value;
                break;
            case "ve":
                discoveryDeviceModel.ProtocolVersion = value;
                break;
            case "ic":
                discoveryDeviceModel.IconPath = value;
                break;
            case "ca":
                discoveryDeviceModel.Certificate = value;
                break;
            case "fn":
                discoveryDeviceModel.FriendlyName = value;
                break;
        }

        return discoveryDeviceModel;
    }

    private static (string, string) GetKeyAndValue(string line)
    {
        var separatedString = line.Split("=");

        return (separatedString[0], separatedString[1]);
    }
}