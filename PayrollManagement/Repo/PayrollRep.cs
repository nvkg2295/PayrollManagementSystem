using Dapper;
using PayrollManagement.Model.Data;
using PayrollManagement.Models;
using System.Data;

namespace PayrollManagement.Repo
{
    public class PayrollRep : IPayrollRep
    {
        private readonly DapperDbContext _context;

        public PayrollRep(DapperDbContext context)
        {
            _context = context;
        }

        public async Task RunPayrollAsync(int month,int year)
        {
            using var connection =
                _context.CreateConnection();

            await connection.ExecuteAsync(
                "usp_RunPayroll",
                new
                {
                    Month = month,
                    Year = year
                },
                commandType:
                CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<PayrollDetailDto>>GetPayrollAsync(int month, int year)
        {
            var query = @"
            SELECT
                pr.PayrollRunId,
                e.EmployeeId,
                e.EmployeeName,
                e.BasicSalary,
                a.WorkingDays,
                a.DaysPresent,
                pd.GrossPay,
                pd.PfDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollRuns pr
            INNER JOIN PayrollDetails pd
                ON pr.PayrollRunId = pd.PayrollRunId
            INNER JOIN Employees e
                ON pd.EmployeeId = e.EmployeeId
            INNER JOIN Attendance a
                ON e.EmployeeId = a.EmployeeId
            WHERE pr.PayrollMonth = @Month
            AND pr.PayrollYear = @Year";

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<PayrollDetailDto>(
                query,
                new
                {
                    Month = month,
                    Year = year
                });
        }

        public async Task<PayslipDto?>GetPayslipAsync(int runId,int employeeId)
        {
            var query = @"
            SELECT
                pr.PayrollRunId,
                e.EmployeeId,
                e.EmployeeName,
                pr.PayrollMonth,
                pr.PayrollYear,
                e.BasicSalary,
                pd.GrossPay,
                pd.PfDeduction,
                pd.ProfessionalTax,
                pd.NetPay
            FROM PayrollRuns pr
            INNER JOIN PayrollDetails pd
                ON pr.PayrollRunId = pd.PayrollRunId
            INNER JOIN Employees e
                ON pd.EmployeeId = e.EmployeeId
            INNER JOIN Attendance a
                ON e.EmployeeId = a.EmployeeId

            WHERE pr.PayrollRunId = @RunId
            AND e.EmployeeId = @EmployeeId";

            using var connection =
                _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<PayslipDto>(
                query,
                new
                {
                    RunId = runId,
                    EmployeeId = employeeId
                });
        }
    }
}
