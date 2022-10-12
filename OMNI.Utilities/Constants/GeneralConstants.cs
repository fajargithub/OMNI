
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace OMNI.Utilities.Constants
{
    public static class GeneralConstants
    {

        //Role Names
        public const string OSMOSYS_SUPER_ADMIN = nameof(OSMOSYS_SUPER_ADMIN);
        public const string OSMOSYS_MANAGEMENT = nameof(OSMOSYS_MANAGEMENT);
        public const string OSMOSYS_GUEST_LOKASI = nameof(OSMOSYS_GUEST_LOKASI);
        public const string OSMOSYS_GUEST_NON_LOKASI = nameof(OSMOSYS_GUEST_NON_LOKASI);
        public const string OSMOSYS_ADMIN_REGION = nameof(OSMOSYS_ADMIN_REGION);
        public const string OSMOSYS_ADMIN_LOKASI = nameof(OSMOSYS_ADMIN_LOKASI);

        public static string SITConnectionMode = nameof(SITConnectionMode);
        public static string DevConnectionMode = nameof(DevConnectionMode);
        public static string ProdConnectionMode = nameof(ProdConnectionMode);

        public static bool IsProduction = true;
        /* Activity Type */
        public const string CREATE = nameof(CREATE);
        public const string UPDATE = nameof(UPDATE);
        public const string DELETE = nameof(DELETE);
        public const string LOGIN = nameof(LOGIN);
        public const string LOGOUT = nameof(LOGOUT);
        public const string REGISTER = nameof(REGISTER);
        public const string RESET_PASSWORD = nameof(RESET_PASSWORD);
        public const string LOGIN_EXTERNAL = nameof(LOGIN_EXTERNAL);

        /* Activity Status */
        public const string SUCCESS = nameof(SUCCESS);
        public const string FAILED = nameof(FAILED);
        public const string YES = "Y";
        public const string NO = "N";
        public const string YES_NAME = "YES";
        public const string NO_NAME = "NO";
        public const string OK = nameof(OK);
        public const string NEW = nameof(NEW);
        public const string PROCESSING = nameof(PROCESSING);
        public const string SOLVED = nameof(SOLVED);
        public const string REPAIR = nameof(REPAIR);
        public const string USED = nameof(USED);
        public const string PENDING = nameof(PENDING);
        public const string APPROVED = nameof(APPROVED);
        public const string REJECTED = nameof(REJECTED);
        public const string FINISHED = nameof(FINISHED);
        public const string VALIDATED = nameof(VALIDATED);
        public const string ACTIVED = nameof(ACTIVED);
        public const string INACTIVED = nameof(INACTIVED);
        public const string NOT_FILLED = nameof(NOT_FILLED);
        public const string FILLED = nameof(FILLED);
        public const string Filled = nameof(Filled);
        public const string UNFILLED = nameof(UNFILLED);
        public const string Unfilled = nameof(Unfilled);
        public const string NOT_YET_APPROVED = nameof(NOT_YET_APPROVED);
        public const string UNREAD = nameof(UNREAD);
        public const string READ = nameof(READ);
        public const string OPEN = nameof(OPEN);
        public const string CHECKING = nameof(CHECKING);
        public const string VERIFIED = nameof(VERIFIED);
        public const string ON_PROGRESS = nameof(ON_PROGRESS);
        public const string EXTEND = nameof(EXTEND);

        /* HSSE Insiden Status */
        public const string INSUBMITTED = "SUBMITTED";
        public const string INREVIEWED = "REVIEWED";
        public const string INCLOSED = "CLOSED";
        public const string ININVESTIGATION = "INVESTIGATION";
        public const string ININVEST_PROGRESSED = "INVEST PROGRESS";
        public const string ININVEST_CLOSED = "INVEST CLOSED";

        public const string FLAG_PREVENTIVE_IMMEDIATE = "IMMEDIATE";
        public const string FLAG_PREVENTIVE_BASIC = "BASIC";

        /* Bunker Product */
        public const string B30 = nameof(B30);
        public const string B0 = nameof(B0);
        public const string HSD_ONLY = "HSD Only";

        /* Employee Title */
        public const string VP_MARKETING = "VP. MARKETING";
        public const string MITRAKERJA = "MITRA KERJA";

        /* Notification Status */
        public const string APPROVAL = nameof(APPROVAL);

        /* User Role */
        public const string SADIS_ADMIN = nameof(SADIS_ADMIN);
        public const string MOVE_ADMIN = nameof(MOVE_ADMIN);
        public const string E_CATALOG_ADMIN = nameof(E_CATALOG_ADMIN);
        public const string PROCUREMENT_ADMIN = nameof(PROCUREMENT_ADMIN);
        public const string EXTERNAL_USER = nameof(EXTERNAL_USER);

        /* User Role elitigation */
        public const string ELITIGATION_ADMIN = nameof(ELITIGATION_ADMIN);
        public const string ELITIGATION_VIEW = nameof(ELITIGATION_VIEW);
        public const string ELITIGATION_DIRECTORS = nameof(ELITIGATION_DIRECTORS);
        public const string E_LITIGATION_ADMIN = nameof(E_LITIGATION_ADMIN);

        /* User Role cisd */
        public const string CISD_ADMIN = nameof(CISD_ADMIN);
        public const string CISD_PIC = nameof(CISD_PIC);
        public const string CISD_ADMIN_DOWNLOAD = nameof(CISD_ADMIN_DOWNLOAD);


        /* User Role Management Report */
        public const string MANAGEMENT_REPORT_ADMIN = nameof(MANAGEMENT_REPORT_ADMIN);
        public const string MANAGEMENT_REPORT_VIEW = nameof(MANAGEMENT_REPORT_VIEW);

        /* User Role Management Report */
        public const string ESMS_ADMIN = nameof(ESMS_ADMIN);
        public const string ESMS_VIEW = nameof(ESMS_VIEW);

        /* User Role Ship Insurance */
        public const string SHIP_INSURANCE_ADMIN = nameof(SHIP_INSURANCE_ADMIN);

        /* User Role Certo */
        public const string CERT_STATUTORY_ADMIN = nameof(CERT_STATUTORY_ADMIN);
        public const string CERT_CLASS_ADMIN = nameof(CERT_CLASS_ADMIN);
        public const string CERT_FINDING_ADMIN = nameof(CERT_FINDING_ADMIN);
        public const string CERT_VIEW = nameof(CERT_VIEW);
        public const string CERT_BPI_NTM_ADMIN = nameof(CERT_BPI_NTM_ADMIN);
        public const string CERT_BPI_NTM_VIEW = nameof(CERT_BPI_NTM_VIEW);
        public const string CERT_KKR_USER = nameof(CERT_KKR_USER);
        public const string CERT_SUPER_ADMIN = nameof(CERT_SUPER_ADMIN);


        /* Employee not Found Place Holder */
        public const string NOT_FOUND_EMPLOYEE = "EMPLOYEE";

        /* User Role Certo */
        public const string HSSE_ADMIN = nameof(CERT_STATUTORY_ADMIN);
        public const string HSSE_SUPERINTENDENT = nameof(HSSE_SUPERINTENDENT);
        public const string HSSE_MANAGER = nameof(HSSE_MANAGER);

        /* User Role Sharp */
        public const string SHARP_ADMIN = nameof(SHARP_ADMIN);

        /* User Role Datalenta */
        public const string DATALENTA_ADMIN = nameof(DATALENTA_ADMIN);

        /* Division Type */
        public const string BOD = nameof(BOD);
        public const string Directorate = nameof(Directorate);
        public const string Division = nameof(Division);
        public const string Branch = nameof(Branch);
        public const string Subsidiary = nameof(Subsidiary);
        public const string Region = nameof(Region);
        public const string Area = nameof(Area);
        public const string SubArea = nameof(SubArea);
        public const string Other = nameof(Other);

        /* User Role ICONSV2 */
        public const string ICONS_VIEWER = nameof(ICONS_VIEWER);
        public const string ICONS_USER = nameof(ICONS_USER);
        public const string ICONS_ADMIN = nameof(ICONS_ADMIN);
        public const string ICONS_APPROVER_LETTER = nameof(ICONS_APPROVER_LETTER);

        /* Emplyee Position */
        public const string Director = nameof(Director);
        public const string VP = "Vp/Head of Auditor/Director AP";
        public const string Manager = "Manager/Senior Auditor/Ka. Cabang";
        public const string AstMan = "Ast. Manager/Auditor";
        public const string Assist = "Assistant/Junior Auditor";
        public const string Contact = "Kontrak";
        public const string Outsourcing = nameof(Outsourcing);

        /* Error Message */
        public const string USER_LOCKED = "User account locked out.";
        public const string USER_NOT_FOUND = "User not Found. Please try again later";
        public const string INVALID_LOGIN_ATTEMPT = "Invalid login attempt.";


        /* Gender */
        public const string MALE = "M";
        public const string FEMALE = "F";

        /* MPPK Status */
        public const string ON = "ON";
        public const string OFF = "OFF";

        /* Education */
        public const string SD = "SD/MI";
        public const string SMP = "SMP/MTS";
        public const string SMA = "SMA/MA";
        public const string D3 = "D3";
        public const string D4 = "D4";
        public const string S1 = "S1";
        public const string S2 = "S2";
        public const string S3 = "S3";

        /* Marital Status */
        public const string SINGLE = "Single";
        public const string MARRIED = "Married";
        public const string DIVORCED = "Divorced";

        /* Delete View */
        public const string URL_CANCEL_DELETE = "~/Views/Shared/_DeleteCancel.cshtml";

        /* Upload */
        public const string URL_CREATE_UPLOAD = "D:/Sharing/PrideApp/wwwroot/upload/";
        public const string URL_VIEW_UPLOAD = "/upload/";
        public const string URL_DELETE_UPLOAD = "D:/Sharing/PrideApp/wwwroot";
        public const string URL_DOWNLOAD_UPLOAD = "D:/Sharing/PrideApp/wwwroot";

        public const string IMAGE_NOT_FOUND = "wwwroot/img/default.jpg";

        //public const string URL_CREATE_UPLOAD = "D:/KP/PROJECT/src/PTK.Engine/wwwroot/upload/";
        //public const string URL_VIEW_UPLOAD = "/upload/";
        //public const string URL_DELETE_UPLOAD = "D:/KP/PROJECT/src/PTK.Engine/wwwroot";
        //public const string URL_DOWNLOAD_UPLOAD = "D:/KP/PROJECT/src/PTK.Engine/wwwroot";

        /* Session Name */
        public const string SESSION_USER = nameof(SESSION_USER);
        public const string SESSION_BEAERER_TOKEN = nameof(SESSION_BEAERER_TOKEN);
        public const string SESSION_APP_METRICS = nameof(SESSION_APP_METRICS);

        public const string ErrorMessageFieldLength = "Field Length too Long";


        /* CSMS STATUS */
        //for question
        public const string ANSWERED = nameof(ANSWERED);


        //Vendor belom jawab
        public const string UNANSWERED = nameof(UNANSWERED);
        //HSSE belom nilai
        public const string PENDINGHSSE = nameof(PENDINGHSSE);
        //HSSSE belom set visit dan kasih nilai field score
        public const string PENDING_SET_VISIT = nameof(PENDING_SET_VISIT);
        //Proc Jadawl set visit
        public const string SET_VISIT_PLANNED = nameof(SET_VISIT_PLANNED);
        //HSSE Setelah Set Visit
        public const string DONE_SET_VISIT = nameof(DONE_SET_VISIT);
        //Vendor setuju/tidak dengan hasil
        public const string PENDING_VENDOR_VERIFICATION = nameof(PENDING_VENDOR_VERIFICATION);
        //Vendor diminta melakukan pengisian Soal, diminta oleh HSSE
        public const string RETURN = nameof(RETURN);
        //Vendor minta revise
        public const string REVISE = nameof(REVISE);
        //vendor udah setuju
        public const string BUILDCERT = nameof(BUILDCERT);
        //vendor Failed dan mohon untuk mengirim ulang Soal
        public const string RETRY = nameof(RETRY);
        //vendor Failed dan mohon untuk mengirim ulang Soal dan sudah dikirim ulang
        public const string RETRY_DONE = nameof(RETRY_DONE);
        //public const string PENDINGUSER = nameof(PENDINGUSER);
        //public const string PENDINGPROC = nameof(PENDINGPROC);


        /* Status CustomerCare */
        public const string WECARE_APP = "WECARE";
        public const string CUSTCARE_DRAFT = "DRAFT";
        public const string CUSTCARE_SUBMIT = "SUBMIT";
        public const string CUSTCARE_REVIEW = "REVIEW";
        public const string CUSTCARE_APROVE = "APPROVE";
        public const string CUSTCARE_DONE = "DONE";

        public const string reCAPTCHA_KEY = "6LdyTWMUAAAAAJNFi6yypn2WUDKPnATbQEL1R1oO";
        public const string reCAPTCHA_SECRET = "6LdyTWMUAAAAALnuacXqIEyjnLQDpa4re36BbDSb";

        /*For Excel Convert*/
        public const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        /* Delimiter */
        public const string COMMA_DELIMITER = ",";
        public const string DOUBLE_COLON_DELIMITER = "::";
        public const string SPACE_STRIP_SPACE = " - ";
        public const string EMPTY_STRING = "";
        public const string SPACE_STRING = " ";
        public const string UNDERSCORE_DELIMITER = "_";
        public const string SLASH_DELIMITER = "/";

        /* Currency */
        public const string IDR = nameof(IDR);
        public const string USD = nameof(USD);
        public const int EXCHANGE_RATE_USD_TO_IDR = 14186;

        /* Number */
        public const int ZERO = 0;
        public const int ONE = 1;
    

        /* Elearning Content Types */
        public const string ELEARNING_VIDEO = "VIDEO";
        public const string ELEARNING_DOC = "DOC";
        public const string ELEARNING_KEY = "6LdyTWMUAAAAALnuacXqIEyjnLQDpa4re36BbDSb";


        public static string GetLocalIPAddress()
        {
            String firstMacAddress = NetworkInterface
               .GetAllNetworkInterfaces()
               .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
               .Select(nic => nic.GetPhysicalAddress().ToString())
               .FirstOrDefault();
            var host = Dns.GetHostEntry(Dns.GetHostName());

            string ipAddressAll = EMPTY_STRING;
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddressAll = ipAddressAll + ip.ToString() + COMMA_DELIMITER;
                }

            }

            if (ipAddressAll == EMPTY_STRING)
            {
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }

            return $"MAC_ADDRESS = {firstMacAddress} :: IP_ADDRESS = {ipAddressAll}";

        }

        public static string ConvertToUserName(string email)
        {
            return email.Substring(0, email.IndexOf('@'));
        }

        public static string ConvertToEmail(string username)
        {
            return username + "@pertamina.com";
        }

        public static bool CheckIsPtkDomain(string email)
        {
            return "ptk-shipping.com" == email.Substring(email.IndexOf('@') + 1, (email.Length - email.IndexOf('@') - 1));

        }

        public static bool CheckIsPtmGroupDomain(string email)
        {
            return "ptk-shipping.com" == email.Substring(email.IndexOf('@') + 1, (email.Length - email.IndexOf('@') - 1)) ||
                    "mitrakerja.pertamina.com" == email.Substring(email.IndexOf('@') + 1, (email.Length - email.IndexOf('@') - 1)) ||
                    "pertamina.com" == email.Substring(email.IndexOf('@') + 1, (email.Length - email.IndexOf('@') - 1));
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static void SetSession<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void RemoveSession(this ISession session, string key)
        {
            session.Remove(key);
        }

        public static List<string> ConvertToListOfString(string param)
        {
            return JsonConvert.DeserializeObject<List<string>>(param);
        }

        public static List<string> ConvertToListFromSeparatedString(string data, string delimiter)
        {
            return data.Split(delimiter).ToList();
        }

        public static string ConvertToStringWithDelimiterFromList(List<string> paramList, string delimiter)
        {
            return string.Join(delimiter, paramList.ToArray());
        }

        public static DateTime ChangeYear(DateTime dt, int newYear)
        {
            return dt.AddYears(newYear - dt.Year);
        }

        public static List<string> GetMonthList()
        {
            return new List<string>
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
            };
        }

        public static T ConvertStreamDataToModelView<T>(string data, T obj) where T : new()
        {
            T classView = new T();
            if (data.Length > 0)
            {
                classView = JsonConvert.DeserializeObject<T>(data);
            }

            return classView;
        }

        public static DateTime ConvertStringToDateTime(string dateString, string format)
        {
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }

        public static List<DateTime> GetDates(DateTime fromDate, DateTime toDate)
        {
            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            //for (var date = new DateTime(year, month, 1); date <= DateTime.Now; date = date.AddDays(1))
            //{
            //    dates.Add(date);
            //}


            foreach (DateTime day in EachDay(fromDate, toDate))
            {
                dates.Add(day);
            }

            dates.Reverse();
            return dates;
        }

        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static List<string> GetAllOrgType()
        {
            return new List<string> { BOD, Directorate, Division, Branch, Subsidiary, Other };
        }
        public static List<string> GetAllOrgTypeNew()
        {
            return new List<string> { BOD, Directorate, Division, Region, Area };
        }
        public static List<string> GetTkjpOrgType()
        {
            return new List<string> { Directorate, Division, Branch, Subsidiary, Other };
        }

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="date">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static string GetYearAndMonthString(DateTime date)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(date).Ticks).Year - 1;
            DateTime PastYearDate = date.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }

            //int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;

            //return string.Format("{0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);

            return string.Format("{0} Year(s) {1} Month(s)", Years, Months);
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        #region ROSY
        public const string UNAVAILABLE = nameof(UNAVAILABLE);

        #region OutStanding Status
        public const string DONE = nameof(DONE);
        public const string TREASURY = nameof(TREASURY);
        public const string SP1 = nameof(SP1);
        public const string SP2 = nameof(SP2);
        public const string SP3 = nameof(SP3);
        public const string MEMO_TO_HR = nameof(MEMO_TO_HR);
        public const string OUTSTANDING = nameof(OUTSTANDING);
        #endregion

        #endregion

        #region Finishing Status
        public const string CREATED = nameof(CREATED);
        public const string SUBMITTED = nameof(SUBMITTED);
        #endregion

        /// <summary>  
        /// For upload  file path destination  
        /// </summary>  
        /// <param name="path">Enter path upload file destination</param>  
        /// <returns> upload file path in string</returns>  
        public static string CreateUploadPath(string path)
        {
            return $"{URL_CREATE_UPLOAD}{path}{DateTime.UtcNow.ToString("yyyyMMdd/")}";
        }



        public static string CreateUploadPathView(string path) => Path.Combine(Path.Combine(URL_VIEW_UPLOAD, path), DateTime.UtcNow.ToString("yyyyMMdd/"));

    }
}
