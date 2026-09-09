using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using NetBondDispatcher.Models;

namespace NetBondDispatcher.Services;

public class NetworkScanner
{
    /// <summary>
    /// Scans and returns all operational IPv4 network adapters currently connected.
    /// </summary>
    public List<NetworkAdapterInfo> GetActiveAdapters()
    {
        var adapters = new List<NetworkAdapterInfo>();
        var interfaces = NetworkInterface.GetAllNetworkInterfaces();

        foreach (var ni in interfaces)
        {
            // Filter only active and non-loopback interfaces
            if (ni.OperationalStatus != OperationalStatus.Up)
                continue;

            if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel)
                continue;

            var ipProps = ni.GetIPProperties();
            var unicastAddresses = ipProps.UnicastAddresses;

            foreach (var addr in unicastAddresses)
            {
                if (addr.Address.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(addr.Address))
                {
                    adapters.Add(new NetworkAdapterInfo
                    {
                        Id = ni.Id,
                        Name = ni.Name,
                        Description = ni.Description,
                        IpAddress = addr.Address,
                        InterfaceType = ni.NetworkInterfaceType,
                        Status = ni.OperationalStatus,
                        Speed = ni.Speed
                    });
                }
            }
        }

        // Return sorted by name
        return adapters.OrderBy(a => a.Name).ToList();
    }
}
