# Sample project for .NET developers candidates

## Project name:  HR Management

>Project goal: To create a simple sample project that simulates a HR Management System application using a set of technologies that are used in our group’s supported systems.  
Project description: The project’s goal is to keep a record from the HR resources for an X enterprise and to provide a simple tool for salaries calculation.
* The system should keep a record of all the workers related to the entity. The relevant information is the following:
- Name (required)
- Last Name (required)
- Email (required)
- Personal Address
- Phone
- Working start date (required)
-  >For each user, the system should have a record of their roles inside the company. Each user may have one or many roles. The list of roles is as follows:
   - Worker
   - Specialist
   - Manager
   - 
The system should allow the CRUD operations to manage users, roles and the users 
- roles relations.
- It is necessary to calculate the salaries for each person based on the time that they have been working in the company. To achieve this, for each user, a starting salary is entered into the system, and periodically (on demand) the system should re calculate the salary based on the following rules:
- The salary revision should be performed every 3 months only, if the process is executed for a user before the 3 months after the last revision, no action should be taken. If the process is executed for a user after 3 months from the last revision, it should increase the salary for all the missing 3 months’ periods.
- If a user is a Worker, the salary should be increased by a 5% every 3 months. For Specialists, the increase will be an 8%, for Managers, it will be a 12%. 
- The system should provide the following information:
- HR list: a list of all the workers in the company, including their roles.
- Historical salaries per user: for a given user, the list of the salaries increases for each period, including the starting salary, and the dates for each increase.

>For convenience, the reports should be implemented in a T-SQL stored procedure.


## Project structure: 
- SQL Server or My SQL database: a simple database using Microsoft SQL Server (2018) or MySQL. The express SQL Server instance within Visual Studio also may be used. The table or tables within the project must have a primary key and a constraint. Must be included all the stored procedures to grant the implementation of the reports. 
- Data access layer / service layer: it must be implemented through a web API using any of the following versions: .Net 4.7, .NET Core 3.1 or .NET 6.0 framework. Other libraries, patterns and considerations on the design, will be at your decision. 
- Test suite: for testing purposes, it is required the use of Swagger or Postman. 
