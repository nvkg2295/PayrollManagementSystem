using PayrollManagement.Models;

namespace PayrollManagement.Service
{
    public interface IEmpService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
    }
}
