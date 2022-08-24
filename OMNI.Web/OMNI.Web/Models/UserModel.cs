using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMNI.Web.Models
{
    public class UserModel
    {
        [JsonProperty("id")]
        public string UserId { get; set; }

        [JsonProperty("organizationId")]
        public string OrganizationId { get; set; }

        [JsonProperty("positionId")]
        public string PositionId { get; set; }

        [JsonProperty("accountEnabled")]
        public string AccountEnabled { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }
        [JsonProperty("companyCode")]
        public string CompanyCode { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("hireDate")]
        public string HireDate { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonProperty("officeLocation")]
        public string OfficeLocation { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
    }

    public class Oid
    {
        public static string Sub { get; set; } = "sub";
        public static string Idp { get; set; } = "idp";
        public static string Email { get; set; } = "email";
        public static string Scope { get; set; } = "scope";
    }

    public class EmpModel
    {
        public string id { get; set; }
        public string userId { get; set; }
        public Position position { get; set; }
        public string companyCode { get; set; }
        public string companyName { get; set; }
        public bool isActive { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string displayName { get; set; }
        public string employeeId { get; set; }
        public string lastName { get; set; }
        public string jobTitle { get; set; }
        public string email { get; set; }
        public string mobilePhone { get; set; }
        public string aboutMe { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string address { get; set; }
        public string photo { get; set; }
        public ExtensionAttributes extensionAttributes { get; set; }
        public string idp { get; set; }
        public string directoryId { get; set; }
        public string lastModified { get; set; }
        public string employeeNumber { get; set; }
        public string employeeType { get; set; }
        public string cultureInfo { get; set; }
        public string language { get; set; }
        public string dateFormat { get; set; }
        public string timeFormat { get; set; }
        public List<ApplicationParam> applicationParams { get; set; }
    }

    public class Application
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ApplicationParam
    {
        public Application application { get; set; }
        public List<CustomParameter> customParameters { get; set; }
    }

    public class CreatedBy
    {
        public List<object> additionalProp1 { get; set; }
        public List<object> additionalProp2 { get; set; }
        public List<object> additionalProp3 { get; set; }
    }

    public class CustomParameter
    {
        public List<object> additionalProp1 { get; set; }
        public List<object> additionalProp2 { get; set; }
        public List<object> additionalProp3 { get; set; }
    }

    public class ExtensionAttributes
    {
        public List<object> additionalProp1 { get; set; }
        public List<object> additionalProp2 { get; set; }
        public List<object> additionalProp3 { get; set; }
    }

    public class Organization
    {
        public string id { get; set; }
        public string name { get; set; }
        public string companyCode { get; set; }
    }

    public class Position
    {
        public string id { get; set; }
        public string name { get; set; }
        public Organization organization { get; set; }
        public string kbo { get; set; }
        public CreatedBy createdBy { get; set; }
        public string lastModified { get; set; }
        public bool isPublished { get; set; }
        public bool isHead { get; set; }
        public bool isOwner { get; set; }
        public bool isChief { get; set; }
    }

    public class Applications
    {
        public string id { get; set; }
        public string clientId { get; set; }
        public CreatedBy createdBy { get; set; }
        public string lastModified { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string photo { get; set; }
        public string clientSecret { get; set; }
        public string logoutUrl { get; set; }
        public string homePageUrl { get; set; }
        public string termofServiceUrl { get; set; }
        public string privacyStatementUrl { get; set; }
        public string publisherDomain { get; set; }
        public string redirectUrl { get; set; }
        public bool isPublished { get; set; }
        public string rattings { get; set; }
        public string technologies { get; set; }
        public string databases { get; set; }
        public string programs { get; set; }
        public string requirements { get; set; }
    }

    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
        public Position position { get; set; }
        public Applications application { get; set; }
    }

    public class RoleModel
    {
        public string type { get; set; }
        public List<Role> roles { get; set; }
    }
}
