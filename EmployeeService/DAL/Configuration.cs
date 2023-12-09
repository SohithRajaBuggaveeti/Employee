namespace EmployeeService.DAL
{
    public class Configuration
    {

        public Configuration() { }
        public class CosmosDbSettings
        {
            public string EndpointUrl { get; set; }
            public string PrimaryKey { get; set; }
            public string DatabaseName { get; set; }
        }
    }
}
