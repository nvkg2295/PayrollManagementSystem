namespace PayrollManagement.Models
{
    public class PayrollDetailDto
    {
        public int PayrollRunId { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public decimal BasicSalary { get; set; }
        public int WorkingDays { get; set; }
        public int DaysPresent { get; set; }

        public decimal GrossPay { get; set; }
        public decimal PfDeduction { get; set; }
        public decimal ProfessionalTax { get; set; }
        public decimal NetPay { get; set; }
    }
}
