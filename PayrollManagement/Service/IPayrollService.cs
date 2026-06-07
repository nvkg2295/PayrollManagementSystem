using PayrollManagement.Models;

namespace PayrollManagement.Service
{
    public interface IPayrollService
    {
        Task RunPayrollAsync(int month, int year);

        Task<IEnumerable<PayrollDetailDto>>
            GetPayrollAsync(int month, int year);

        Task<PayslipDto?>
            GetPayslipAsync(int runId, int employeeId);
    }
}
