using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Models;
using PayrollManagement.Service;

namespace PayrollManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(
            IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost("run")]
        public async Task<IActionResult>RunPayroll(PayrollRunRequest request)
        {
            try
            {
                await _payrollService.RunPayrollAsync(request.Month,request.Year);

                return Created(string.Empty,
                    new
                    {
                        Message =
                        "Payroll generated successfully"
                    });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(
                    "Payroll already exists"))
                {
                    return Conflict(new
                    {
                        Message = ex.Message
                    });
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month:int}/{year:int}")]
        public async Task<IActionResult>GetPayroll(int month,int year)
        {
            var payroll = await _payrollService.GetPayrollAsync(month, year);

            if (!payroll.Any())
            {
                return NotFound(
                    "Payroll not found");
            }

            return Ok(payroll);
        }

        [HttpGet("{runId:int}/slip/{employeeId:int}")]
        public async Task<IActionResult>GetPayslip(int runId,int employeeId)
        {
            var payslip =await _payrollService.GetPayslipAsync(runId,employeeId);

            if (payslip == null)
            {
                return NotFound(
                    "Payslip not found");
            }
            return Ok(payslip);
        }
    }
}
