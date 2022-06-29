using System;
using System.Collections.Generic;
using GCast.Protocol.Discovery.Config;
using Xunit;

namespace GCast.Protocol.Test.Discovery.Config;

public class DeviceConfigReaderTests
{
    private readonly List<string> _getRawStrings = new ()
    {
        $"id={Guid.NewGuid()}",
        "md=audio",
        "ve=05",
        "ic=/setup/icon.png",
        "ca=5",
        "fn=LoremIpsum"
    };

    [Fact, Trait("type", "unit")]
    public void GetParsedDeviceConfig_ChecksParsedValues()
    {
        var deviceModel = DeviceConfigReader.GetDeviceModel(_getRawStrings);
        
        Assert.Equal(deviceModel.Id, _getRawStrings[0].Split("=")[1]);
        Assert.Equal(deviceModel.DeviceModel, _getRawStrings[1].Split("=")[1]);
        Assert.Equal(deviceModel.ProtocolVersion, _getRawStrings[2].Split("=")[1]);
        Assert.Equal(deviceModel.IconPath, _getRawStrings[3].Split("=")[1]);
        Assert.Equal(deviceModel.Certificate, _getRawStrings[4].Split("=")[1]);
        Assert.Equal(deviceModel.FriendlyName, _getRawStrings[5].Split("=")[1]);
    }
}