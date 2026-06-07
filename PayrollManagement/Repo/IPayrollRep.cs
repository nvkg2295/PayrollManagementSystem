using PayrollManagement.Models;
using System.Threading.Tasks;

namespace PayrollManagement.Repo
{
    public interface IPayrollRep
    {
        Task RunPayrollAsync(int month, int year);

        Task<IEnumerable<PayrollDetailDto>>GetPayrollAsync(int month, int year);

        Task<PayslipDto?>GetPayslipAsync(int runId, int employeeId);
    }
}
