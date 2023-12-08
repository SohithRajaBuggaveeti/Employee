using EmployeeService.Models;
using System.Collections;

namespace EmployeeService.Data
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDocument> GetByEmployeeId(int employeeId);
        Task<List<EmployeeDocument>> GetByEmployeeName(string employeeNmae);
        Task<List<EmployeeDocument>> GetByEmployeeDesignation(string designation);
        Task<EmployeeDocument> GetByEmail(string email);
        Task<List<EmployeeDocument>> GetByDepartment(string department);
        Task<EmployeeDocument> GetByEmployeePhoneNumer(string number);
        Task <EmployeeDocument>CreateEmployeeAsync(EmployeeDocument employeeDocument);

    }
}
