# Payroll Management System

A lightweight Payroll Management System built using ASP.NET Core Web API (.NET 8), Dapper, SQL Server, and a simple frontend hosted from `wwwroot`.

The application allows users to:

* Employee management
* Payroll generation for a selected month and year
* Payroll summary view
* Employee payslip generation and printing

---

# Technology Stack

* ASP.NET Core 8 Web API
* C#
* Dapper
* SQL Server
* HTML
* CSS
* JavaScript

---

# Project Structure

PayrollManagement/
│
├── Controllers/
│   ├── EmpController.cs
│   └── PayrollController.cs
│
├── Repo/
├── Service/
├── Model/
├── wwwroot/
│   ├── index.html
│   ├── css/
│   └── js/
│       └── app.js
│
├── Program.cs
├── appsettings.json
│
└── db/
    ├── PayrollDB.sql
    ├── Tables.sql
    ├── StoreProcedure.sql
    └── SeedData.sql

# Prerequisites

Before running the application, ensure the following are installed:

* .NET 8 SDK
* SQL Server 
* Visual Studio 2022 or VS Code

---

# 1. Setup and Run the Project Locally

## Step 1: Clone the Repository

git clone <repository-url>
cd PayrollManagement

## Step 2: Database Setup

Open SQL Server Management Studio and execute the scripts in the following order:

db/
├── PayrollDB.sql
├── Tables.sql
├── StoreProcedure.sql
└── SeedData.sql

These scripts will:

* Create the PayrollDB database
* Create all required tables
* Create the payroll generation stored procedure (`usp_RunPayroll`)
* Insert sample departments, employees, and attendance data

## Step 3: Configure the Connection String

Update the connection string in:

PayrollManagement/appsettings.json

Example:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PayrollDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}


For SQL Authentication(Optional):

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PayrollDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True"
  }
}

## Step 4: Build the Application

dotnet restore
dotnet build

## Step 5: Run the Application

dotnet run

The application will start using the URLs configured in:

Properties/launchSettings.json
Typical local URLs:

https://localhost:5196
http://localhost:7164
---

# 2 Frontend

The project includes a lightweight frontend hosted by ASP.NET Core.

Frontend files are located in:

wwwroot/

Main files:

wwwroot/index.html
wwwroot/js/app.js

The frontend communicates with the backend APIs using JavaScript fetch requests.

After running the application, open:

https://localhost:5196

(or the URL shown in the console)

to access the Payroll Management UI.

---

# 3. Database Design

## Database Name

PayrollDB

## Database Scripts

The database scripts are available under:

db/
├── 01_CreateDatabase.sql
├── 02_CreateTables.sql
├── 03_SeedData.sql
└── 04_StoredProcedures.sql

## Tables

The following tables are created:

* Departments
* Employees
* Attendance
* PayrollRuns
* PayrollDetails

## Stored Procedure

usp_RunPayroll

This stored procedure:

* Prevents duplicate payroll generation
* Validates attendance availability
* Creates payroll run records
* Calculates employee payroll details

### Payroll Formula

Gross Pay: (BasicSalary / WorkingDays) × DaysPresent

PF Deduction: 12% of BasicSalary

Professional Tax: ₹200

Net Pay: GrossPay - PF Deduction - Professional Tax.

## Seed Data

Sample data includes:

### Departments

* HR
* IT
* Finance

### Employees

* Ravi Sharma
* Arun Kumar
* Priya Singh
* Neha Patel
* Vamshi Krishna

### Attendance

Attendance records for June 2026 are provided to allow payroll generation immediately after setup.

---

# API Endpoints

## Generate Payroll

POST /api/payroll/run

Request:

json
{
  "month": 6,
  "year": 2026
}

## Payroll Summary

GET /api/payroll/{month}/{year}

Example:

GET /api/payroll/6/2026

## Employee Payslip

GET /api/payroll/{runId}/slip/{employeeId}

## Employees

GET /api/employees

---

# 3. Assumptions

The following assumptions were made during implementation:

1. Attendance data is entered before payroll generation.
2. Payroll can be generated only once for a given month and year.
3. Professional Tax is fixed at ₹200 per employee.
4. PF deduction is calculated as 12% of Basic Salary.
5. Payroll calculations are performed in SQL Server through the `usp_RunPayroll` stored procedure.
6. Sample data is included only for demonstration and testing purposes.

---

# Notes

This project was developed as a sample payroll processing application demonstrating:

* ASP.NET Core Web API development
* Dapper-based data access
* SQL Server stored procedures
* Layered architecture (Controller → Service → Repository)
* Frontend integration using static files hosted from `wwwroot`
