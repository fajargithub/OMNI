using System;
using System.Collections.Generic;
using System.IO;
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
        public static readonly string REPORT = nameof(REPORT);
        public static readonly string REPORT_EXCEL = nameof(REPORT_EXCEL);
        public static readonly string ACTIVITY = nameof(ACTIVITY);
        public static readonly string OMNI_LLP = nameof(OMNI_LLP);
        public static readonly string OMNI_PERSONIL = nameof(OMNI_PERSONIL);
        public static readonly string OMNI_LATIHAN = nameof(OMNI_LATIHAN);
        public static readonly string ITEM = nameof(ITEM);
        public static readonly string OPTION_IMAGE = nameof(OPTION_IMAGE);
        public static readonly string UW_ATTACHMENT_FILE = nameof(UW_ATTACHMENT_FILE);
        public static readonly string ANSWER_IMAGE = nameof(ANSWER_IMAGE);

        //LAMPIRAN
        public static readonly string OSMOSYS_PENILAIAN = nameof(OSMOSYS_PENILAIAN);
        public static readonly string OSMOSYS_PENGESAHAN = nameof(OSMOSYS_PENGESAHAN);
        public static readonly string OSMOSYS_VERIFIKASI = nameof(OSMOSYS_VERIFIKASI);

        public static readonly string IMG_PLACEHOLDER = "wwwroot/img/place_holder.png";

        public const string CREATE = nameof(CREATE);
        public const string UPDATE = nameof(UPDATE);
        public const string DELETE = nameof(DELETE);
        public const string LOGIN = nameof(LOGIN);
        public const string LOGOUT = nameof(LOGOUT);
        public const string REGISTER = nameof(REGISTER);
        public const string RESET_PASSWORD = nameof(RESET_PASSWORD);
        public const string LOGIN_EXTERNAL = nameof(LOGIN_EXTERNAL);

        public const string SUCCESS = nameof(SUCCESS);
        public const string FAILED = nameof(FAILED);

        public const string URL_CREATE_UPLOAD_NEW = "D:/Sharing/";
        public const string URL_VIEW_UPLOAD = "/upload/";
        public static bool IsProduction { get; set; } = false;
        public static string NO = "N";
        public static string YES = "Y";

        public const string ErrorMessageFieldLength = "Field Length too Long";

        public static string CreateUploadPathView(string path) => Path.Combine(Path.Combine(URL_VIEW_UPLOAD, path), DateTime.UtcNow.ToString("yyyyMMdd/"));
        public static string CreateUploadPathNew(string path)
        {
            return Path.Combine(URL_CREATE_UPLOAD_NEW, Path.Combine("upload/", Path.Combine(path, DateTime.UtcNow.ToString("yyyyMMdd/"))));
        }
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
