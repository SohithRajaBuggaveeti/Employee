# Employee Microservice:

Employee Microservice with 4 CRUD operations are implemented in this project.

It has 9 endpoints 

6 endpoints for the Get method like getting the Employees with employeeId, email, employeeName, Designation, Department and phone Number

1. GetByEmployeeId : It takes the employeeId as a parameter and checks if the employee is present. It return Ok with Employee details if the employee is found or else it Employee not found.

2. GetByEmail : It takes the email as a parameter and checks if the employee is present. It return Ok with Employee details if the employee is found or else it Employee not found.

3. GetByPhoneNumber:  It takes the phone number as a parameter and checks if the employee is present. It return Ok with Employee details if the employee is found or else it Employee not found.

4. GetByEmployeeName: It takes the employee name as a parameter and checks if the employee are present. It return Ok with List of Employees or  else it Employees not found.

5. GetByDesignation : It takes the designation as a parameter and checks if any employees are present. It return Ok with List of Employees else it Employees not found.

6. GetByDepartment:  It takes the department as a parameter and checks if the employees are present. It return Ok with List of Employees if the or  else it Employee not found.

7. AddNewEmployee: It takes the employee details and checks if the employee with same email or phone number is present. If an employee is found then it returns employee with same email or phone number already exists or else it creates a new employee with the 8 digit employee id and returns it.

8. deleteEmployee: It takes the employeeId as parameter and deletes the employee if the employee is found or else it returns Employee not found message to the front end.

9. updateEmployee: It takes the employeeId and Employee details as parameter and checksfor employee and updates the employee details or else it returns Employee not found message to front end.

I've also implemented Rate limiter in this project which limits 100 requests per min from an IP address. If an IP address exceeds that limit 429 message is returned