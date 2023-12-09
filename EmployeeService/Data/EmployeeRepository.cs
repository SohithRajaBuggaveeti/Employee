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
            var databaseName =configuration["CosmosDbSettings:DatabaseName"];
            var employeeContainerName = "Employees";
            _employeeContainer = cosmosClient.GetContainer(databaseName, employeeContainerName);

        }

        public async Task<EmployeeDocument> GetByEmployeeId(string employeeId)
        {
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                .Where(e => e.EmployeeId == employeeId).Take(1).ToFeedIterator();
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
                .Where(e => e.Department == department).ToFeedIterator();
            List<EmployeeDocument> employeeDocuments = new List<EmployeeDocument>();
            while (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDocuments.AddRange(response);
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
                .Where(e => e.Designation == designation).ToFeedIterator();
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

        public async Task<EmployeeDocument> CreateEmployeeAsync(EmployeeDocument employeeDocument)
        {
            
            
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                              .Where(e => e.Email == employeeDocument.Email||e.PhoneNumber==employeeDocument.PhoneNumber).ToFeedIterator();

            var result = await query.ReadNextAsync();

            var existingEmployee=result.FirstOrDefault();

            if (existingEmployee !=null)
            {
                // Handle the situation where an employee with the same Eamil ID already exists
                // You might want to throw an exception or return null
                throw new InvalidOperationException($"An employee with same email or phone number already exists.");
            }

            string uniqueemployeeId;
            do
            {
                var guid = Guid.NewGuid();
                int hash = Math.Abs(guid.GetHashCode());
                uniqueemployeeId = (hash % 100000000).ToString();
                query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                              .Where(e => e.EmployeeId == uniqueemployeeId).ToFeedIterator();
                result = await query.ReadNextAsync();
                existingEmployee = result.FirstOrDefault();

            } while (string.IsNullOrEmpty(uniqueemployeeId) || uniqueemployeeId.Length > 1023 || (existingEmployee!=null));

            employeeDocument.EmployeeId = uniqueemployeeId;
            employeeDocument.Id = uniqueemployeeId;
            // Add the new employee to the container
            var response = await _employeeContainer.CreateItemAsync(employeeDocument, new PartitionKey(employeeDocument.EmployeeId));

            // Return the created employee document
            // You can also return the response object if you need more details about the operation
            return response.Resource;
        }

        public async Task<bool> deleteEmployee(string employeeId)
        {
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
         .Where(e => e.Id == employeeId).Take(1).ToFeedIterator();

            if (query.HasMoreResults)
            {
                // Read the query response and get the first (and only) item
                var response = await query.ReadNextAsync();
                var employeeDoc = response.FirstOrDefault();

                // If the document is found, delete it
                if (employeeDoc != null)
                {
                    await _employeeContainer.DeleteItemAsync<EmployeeDocument>(employeeDoc.Id, new PartitionKey(employeeDoc.EmployeeId));
                    return true;
                }
            }
            return false;
        }

        public async Task<EmployeeDocument> updateEmployee(string id, EmployeeDocument employeeDocument)
        {
            var query = _employeeContainer.GetItemLinqQueryable<EmployeeDocument>()
                 .Where(e => e.Id == id.ToString()).Take(1).ToFeedIterator();
            EmployeeDocument employeeDoc = null;
            if (query.HasMoreResults)
            {
                FeedResponse<EmployeeDocument> response = await query.ReadNextAsync();
                employeeDoc = response.FirstOrDefault(); // Get the first (and only) item
            }
            
            employeeDoc.EmployeeName = employeeDocument.EmployeeName!=null?employeeDocument.EmployeeName:employeeDoc.EmployeeName;
            employeeDoc.Email=employeeDocument.Email!=null?employeeDocument.Email:employeeDoc.Email;
            employeeDoc.Designation=employeeDocument.Designation!=null?employeeDocument.Designation:employeeDoc.Designation;
            employeeDoc.Department = employeeDoc.Department != null ? employeeDocument.Department : employeeDoc.Department;
            employeeDoc.ManagerId=employeeDoc.ManagerId!=null?employeeDocument.ManagerId:employeeDoc.ManagerId;
            employeeDoc.Address = employeeDoc.Address != null ? employeeDocument.Address : employeeDoc.Address;
            employeeDoc.PhoneNumber = employeeDoc.PhoneNumber != null ? employeeDocument.PhoneNumber : employeeDoc.PhoneNumber;


            await _employeeContainer.ReplaceItemAsync(employeeDoc, employeeDoc.Id);
            return employeeDoc;
        }
    }
}
