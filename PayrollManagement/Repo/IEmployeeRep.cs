using PayrollManagement.Models;

namespace PayrollManagement.Repo
{
    public interface IEmployeeRep
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
    }
}
