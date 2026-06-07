USE [PayrollDB]

 INSERT INTO Departments
(
    DepartmentName
)
VALUES
('HR'),
('IT'),
('Finance');

INSERT INTO Employees
(
    EmployeeName,
    DepartmentId,
    BasicSalary
)
VALUES
('Ravi Sharma',1,30000),
('Arun Kumar',1,25000),
('Priya Singh',2,40000),
('Neha Patel',2,45000),
('Vamshi Krishna',3,35000);



INSERT INTO Attendance
(
    EmployeeId,
    AttendanceMonth,
    AttendanceYear,
    WorkingDays,
    DaysPresent
)
VALUES
(1,6,2026,26,24),
(2,6,2026,26,25),
(3,6,2026,26,26),
(4,6,2026,26,22),
(5,6,2026,26,23);