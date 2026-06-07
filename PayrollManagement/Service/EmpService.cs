using PayrollManagement.Models;
using PayrollManagement.Repo;

namespace PayrollManagement.Service
{
    public class EmpService: IEmpService
    {
        private readonly IEmployeeRep _employeeRep;
        public EmpService(IEmployeeRep employeeRep)
        {
            _employeeRep = employeeRep;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            return await _employeeRep.GetEmployeesAsync();
        }

    }
}
