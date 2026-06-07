using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Service;

namespace PayrollManagement.Controllers
{
    // Expose endpoint as /api/employees to match the specification
    [Route("api/employees")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private IEmpService _empservice;
        public EmpController(IEmpService empservice)
        {
            _empservice = empservice;
        }
        [HttpGet]
       public async Task<IActionResult> GetEmployees()
        {
            var employees = await _empservice.GetEmployeesAsync();
            return Ok(employees);
        }
    }
}
