using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OMNI.Utilities.Constants
{
    public static class GeneralConstants
    {
        public static bool IsProduction { get; set; } = false;
        public static string NO = "N";
        public static string YES = "Y";

        public const string ErrorMessageFieldLength = "Field Length too Long";

        public static string GetLocalIPAddress()
        {
            string firstMacAddress = NetworkInterface
               .GetAllNetworkInterfaces()
               .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
               .Select(nic => nic.GetPhysicalAddress().ToString())
               .FirstOrDefault();
            var host = Dns.GetHostEntry(Dns.GetHostName());

            string ipAddressAll = string.Empty;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddressAll = ipAddressAll + ip.ToString() + ",";
                }

            }

            if (ipAddressAll == string.Empty)
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }

            return $"MAC_ADDRESS = {firstMacAddress} :: IP_ADDRESS = {ipAddressAll}";

        }
    }
}
