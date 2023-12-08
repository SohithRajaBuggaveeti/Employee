using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EmployeeService.Models
{
    public class EmployeeDocument
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("employeename")]
        public string EmployeeName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("department")]
        public string Department {  get; set; }

        [JsonProperty("managerId")]
        public int ManagerId { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("phonenumber")]   
        public string PhoneNumber { get; set; }

        public static explicit operator EmployeeDocument(FeedResponse<EmployeeDocument> v)
        {
            throw new NotImplementedException();
        }
    }
    public class Address
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalcode")]
        public string PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

}
}
