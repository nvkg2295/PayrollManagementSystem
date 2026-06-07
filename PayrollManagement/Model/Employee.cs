namespace PayrollManagement.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public decimal BasicSalary { get; set; }
    }
}
