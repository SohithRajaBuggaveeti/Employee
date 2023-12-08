using EmployeeService.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Collections;
using System.Linq;
using System.Security.Policy;

namespace EmployeeService.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CosmosClient cosmosClient;

        private readonly IConfiguration configuration;

        private readonly  Container _employeeContainer;
        public EmployeeRepository(CosmosClient cosmosClnt,IConfiguration config) {
        this.cosmosClient = cosmosClnt;
            this.configuration = config;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var employeeContainerName = "Employees";
            _employeeContainer = cosmosClient.GetContainer(databaseName, employeeContainerName);

        }

        public async Task<EmployeeDocument> GetByEmployeeId(int employeeId)
        {
            Console.WriteLine(employeeId);
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.EmployeeId == employeeId.ToString()).Take(1).ToFeedIterator();
            EmployeeDocument employeeDocument = null;
            if(query.HasMoreResults)
{
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
               employeeDocument = response.FirstOrDefault(); // Get the first (and only) item
            }
            if (employeeDocument!=null)
            {
                return employeeDocument;
            }
            return null;
        }

        public async Task<List<EmployeeDocument>> GetByEmployeeName(string employeeName)
        {
            
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.EmployeeName==employeeName).ToFeedIterator();
            var employeeDocuments=new List<EmployeeDocument>();
            while(query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocuments.AddRange(response);
            }
            return employeeDocuments;
        }

       public async Task<List<EmployeeDocument>> GetByDepartment(string department)
        {
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.Department == department).Take(1).ToFeedIterator();
            List<EmployeeDocument> employeeDocuments = new List<EmployeeDocument>();
            while (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocuments.Add(response.FirstOrDefault());
            }
            return employeeDocuments;
        }

       public async Task<EmployeeDocument> GetByEmail(string email)
        {
            Console.WriteLine(email);
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.Email == email).Take(1).ToFeedIterator();
            EmployeeDocument employeeDocument = null;
            if (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocument = response.FirstOrDefault(); // Get the first (and only) item
            }
            if (employeeDocument != null)
            {
                return employeeDocument;
            }
            return null;
        }

       public async Task<List<EmployeeDocument>> GetByEmployeeDesignation(string designation)
        {
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.Designation == designation).Take(1).ToFeedIterator();
           var employeeDocuments = new List<EmployeeDocument>();
            while (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocuments.AddRange(response);
            }
            return employeeDocuments;
        }

        public async Task<EmployeeDocument> GetByEmployeePhoneNumer(string number)
        {
            Console.WriteLine(number);
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.PhoneNumber == number).Take(1).ToFeedIterator();
            EmployeeDocument employeeDocument = null;
            if (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocument = response.FirstOrDefault(); // Get the first (and only) item
            }
            if (employeeDocument != null)
            {
                return employeeDocument;
            }
            return null;
        }

        public Task<EmployeeDocument> CreateEmployeeAsync(EmployeeDocument employeeDocument)
        {
            throw new NotImplementedException();
        }
    }
}
