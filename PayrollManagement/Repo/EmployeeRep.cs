using Dapper;
using PayrollManagement.Model.Data;
using PayrollManagement.Models;

namespace PayrollManagement.Repo
{
    public class EmployeeRep: IEmployeeRep
    {
        private readonly DapperDbContext _context;
        public EmployeeRep(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var query = @"
                            SELECT 
                                e.EmployeeId,
                                e.EmployeeName,
                                d.DepartmentName,
                                e.BasicSalary
                            FROM Employees e
                            INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId";

            using var connection = _context.CreateConnection();

            var employees = await connection.QueryAsync<EmployeeDto>(query);

            return employees;
        }
    }
}
