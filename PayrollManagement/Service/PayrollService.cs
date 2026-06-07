using PayrollManagement.Models;
using PayrollManagement.Repo;

namespace PayrollManagement.Service
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRep _payrollRep;

        public PayrollService(IPayrollRep payrollRep)
        {
            _payrollRep = payrollRep;
        }

        public async Task RunPayrollAsync(
            int month,
            int year)
        {
            await _payrollRep
                .RunPayrollAsync(month, year);
        }

        public async Task<IEnumerable<PayrollDetailDto>>
            GetPayrollAsync(int month, int year)
        {
            return await _payrollRep
                .GetPayrollAsync(month, year);
        }

        public async Task<PayslipDto?>
            GetPayslipAsync(
                int runId,
                int employeeId)
        {
            return await _payrollRep
                .GetPayslipAsync(runId, employeeId);
        }
    }
}
