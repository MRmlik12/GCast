using System.Net;
using Flurl;
using Flurl.Http;
using GCast.Protocol.Discovery.Models;

namespace GCast.Protocol.Discovery;

internal static class Requests
{
    public static async Task<DeviceModel> GetDeviceInfo(IPEndPoint deviceAddress)
    {
        return await $"http://{deviceAddress.Address}:8008"
            .AppendPathSegment("setup/eureka_info")
            .WithTimeout(15000)
            .GetJsonAsync<DeviceModel>();
    }
}