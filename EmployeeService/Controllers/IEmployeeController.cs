using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    public interface IEmployeeController
    {
        Task<ActionResult<EmployeeDocument>> GetByEmployeeId(string employeeId);
        Task<ActionResult<List<EmployeeDocument>>> GetByEmployeeName(string employeeName);
        Task<ActionResult<EmployeeDocument>> GetByEmployeeDesignation(string designation);
        Task<ActionResult<EmployeeDocument>> GetByEmail(string email);
        Task<ActionResult<List<EmployeeDocument>>> GetByDepartment(string department);
        Task<ActionResult<EmployeeDocument>> GetByPhoneNumber(string phoneNumber);
        Task<ActionResult<EmployeeDocument>> AddNewEmployee(EmployeeDocument employeeDocument);
        Task<ActionResult<EmployeeDocument>> UpdateEmployee(string employeeId, EmployeeDocument employeeDocument);
        Task<ActionResult> Delete(string employeeId);



    }
}
