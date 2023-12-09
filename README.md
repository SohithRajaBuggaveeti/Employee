# Employee Microservice:

Employee Microservice with 4 CRUD operations are implemented in this project. The project was implemented using C#, .NET 8.0 and we used Azure CosmosDB.

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

I've
 also implemented Rate limiter in this project which limits 100 requests per min from an IP address. If an IP address exceeds that limit 429 message is returned

Post Request: 
![post](https://github.com/SohithRajaBuggaveeti/Employee/assets/39987263/d065f4ce-2051-48fd-be18-cdc5c816ef43)

Get Requests : 
![getByDesignation](https://github.com/SohithRajaBuggaveeti/Employee/assets/39987263/b39b3412-0495-4f43-9dff-c81f01521752)

![getByEmployeeId](https://github.com/SohithRajaBuggaveeti/Employee/assets/39987263/af7f2a18-c7d1-4a05-b57a-f186b82cf133)

Put Request : 
![put](https://github.com/SohithRajaBuggaveeti/Employee/assets/39987263/34021ad7-a1f6-4695-8159-57abd83f4a90)

Delete Request : 
![delete](https://github.com/SohithRajaBuggaveeti/Employee/assets/39987263/15844648-94b4-43ee-96bc-a62b06c2e6c1)



