using GCast.Protocol.Discovery.Args;
using GCast.Protocol.Discovery.Config;
using Makaretu.Dns;

namespace GCast.Protocol.Discovery;

public class DiscoveryDevices : IDisposable
{
    private const string InstanceName = "google-devices";
    private const string ServiceName = "_googlecast._tcp";
    private const int Port = 8009;

    private MulticastService? _mdns;
    private ServiceDiscovery? _sd;

    public void Dispose()
    {
        _sd?.Dispose();
        _mdns?.Dispose();
    }

    public event Events.OnDeviceDiscovered? OnDeviceDiscovered;

    public void Start()
    {
        _mdns = new MulticastService();
        _sd = new ServiceDiscovery(_mdns);

        _mdns.AnswerReceived += (_, args) =>
        {
            var message = args.Message.Answers[0];

            if (message.CanonicalName != "_googlecast._tcp.local") return;

            foreach (var record in args.Message.AdditionalRecords.Where(record =>
                         record.Type == DnsType.TXT && record.Name.Labels[0] != "google-devices"))
            {
                var txtRecord = (TXTRecord)record;
                var deviceModel = DeviceConfigReader.GetDeviceModel(txtRecord.Strings);

                var handler = OnDeviceDiscovered;
                handler?.Invoke(this, new DeviceDiscoveredEventArgs
                {
                    Device = deviceModel
                });
            }
        };

        _sd.Advertise(new ServiceProfile(InstanceName, ServiceName, Port));
        _mdns.Start();
        Console.ReadKey();
    }

    public void Stop()
    {
        _sd?.Dispose();
        _mdns?.Stop();
    }
}