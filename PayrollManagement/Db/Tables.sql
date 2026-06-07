USE [PayrollDB]


 create  table departments(
 departmentid int  primary key identity(1,1),
 departmentname nvarchar(100) not null
 )

 create table employees(
 employeeid int primary key identity(1,1),
 employeename nvarchar(100) not null,
 departmentid int not null,
 basicsalary decimal(18,2) not null,
 foreign key (departmentid) references departments(departmentid)
 )

 create table attendance(
 attendanceid int primary key identity(1,1),
 employeeid int not null,
 attendancemonth int not null,
 attendanceyear int not null,
 workingdays int not null,
 dayspresent int not null,
 foreign key (employeeid) references employees(employeeid)
 )

 create table payrollruns(
 payrollrunid int primary key identity(1,1),
 payrollmonth int not null,
 payrollyear int  not null,
 createddate datetime not null default getdate(),
 unique(payrollmonth, payrollyear)
 )

 create table payrolldetails(
PayrollDetailsId int Primary key identity(1,1),
PayrollRunId int not null,
EmployeeId int not null,
GrossPay Decimal(18,2) not null,
PfDeduction Decimal(18,2) not null,
ProfessionalTax Decimal(18,2) not null,
NetPay Decimal(18,2) not null,
Foreign key (PayrollRunId) References PayrollRuns(PayrollRunId),
Foreign key (EmployeeId) references Employees(EmployeeId)
)
