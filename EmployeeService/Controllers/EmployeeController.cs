using EmployeeService.Data;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;

        }
        [HttpGet("employeeId/{employeeId}")]
        public async Task<ActionResult<EmployeeDocument>> GetByEmployeeId(string employeeId)
        {
            var employee = await _employeeRepository.GetByEmployeeId(employeeId);
            return employee != null ?Ok(employee):BadRequest("EmployeeId is not present");
        }
        [HttpGet("employeeName/{employeeName}")]
        public async Task<ActionResult<List<EmployeeDocument>>> GetByEmployeeName(string employeeName)
        {
            var employees = await _employeeRepository.GetByEmployeeName(employeeName);
            return employees.Count!=0 ? Ok(employees) : BadRequest("Cannot find any employees with the given name");
        }
        [HttpGet("designation/{designation}")]
        public async Task<ActionResult<EmployeeDocument>> GetByEmployeeDesignation(string designation)
        {
            var employee = await _employeeRepository.GetByEmployeeDesignation(designation);
            return employee.Count != 0 ? Ok(employee) : BadRequest("Cannot find any employees with the given designation");
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<EmployeeDocument>> GetByEmail(string email)
        {
            var employee = await _employeeRepository.GetByEmail(email);
            return employee != null ? Ok(employee) : BadRequest("Employee not found");
        }
        [HttpGet("department/{department}")]
        public async Task<ActionResult<List<EmployeeDocument>>> GetByDepartment(string department)
        {
            var employee = await _employeeRepository.GetByDepartment(department);
            return employee != null ? Ok(employee) : BadRequest("Cannot find any employees in the Department");
        }

        [HttpGet("phonenumber/{phonenumber}")]
        public async Task<ActionResult<EmployeeDocument>> GetByPhoneNumber(string phoneNumber)
        {
            var employee = await _employeeRepository.GetByEmployeePhoneNumer(phoneNumber);
            return employee != null ? Ok(employee) : BadRequest("Employee not found");
        }
        [HttpPost("addemployee")]
        public async Task<ActionResult<EmployeeDocument>> AddNewEmployee([FromBody]EmployeeDocument employeeDocument)
        {
            EmployeeDocument createEmployee = null;
            try
            {
                 createEmployee = await _employeeRepository.CreateEmployeeAsync(employeeDocument);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return createEmployee != null ? Ok(createEmployee) : BadRequest("Cannot create a new employee");
        }
        [HttpPut("updateemployee/{employeeId}")]
        public async Task<ActionResult<EmployeeDocument>> UpdateEmployee(string employeeId, EmployeeDocument employeeDocument)
        {
            var existingEmployee = await _employeeRepository.GetByEmployeeId(employeeId);
            if(existingEmployee==null)
            {
                return BadRequest("Employee not found");
            }
            var updateEmployee = await _employeeRepository.updateEmployee(employeeId,employeeDocument);
            return Ok(updateEmployee);
        }
        [HttpDelete("deleteemployee/{employeeId}")]
        public async Task<ActionResult> Delete(string employeeId)
        {
            var existingEmployee = await _employeeRepository.GetByEmployeeId(employeeId);
            if (existingEmployee == null)
            {
                return BadRequest("Employee not found");
            }
            if (await _employeeRepository.deleteEmployee(employeeId))
            {
                return Ok("Employee deleted sucessfully");
            }
            else
            {
                return BadRequest($"Could not delete an employee");
            }
        }
        [HttpGet("test/{randomstring}")]
        public async Task<ActionResult<string>> GetTest(string randomstring    )
        {
            return Ok(randomstring);
        }
    }
}
