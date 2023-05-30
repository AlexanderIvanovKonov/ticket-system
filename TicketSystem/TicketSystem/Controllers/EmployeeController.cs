using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TicketSystem.Models;
using TicketSystem.Services;

namespace TicketSystem.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            if (employees == null || employees.Count == 0)
                return NoContent();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // GET: api/employees/department/{departmentId}
        [HttpGet("department/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await _employeeService.GetEmployeesByDepartmentIdAsync(departmentId);
            if (employees == null || employees.Count == 0)
                return NotFound();

            return Ok(employees);
        }

        // aproach different from the one in the DepartmentController
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(employee.Name))
                ModelState.AddModelError("Name", "The Name field is required.");

            if (string.IsNullOrWhiteSpace(employee.Lastname))
                ModelState.AddModelError("Lastname", "The Lastname field is required.");

            if (employee.DepartmentId <= 0)
                ModelState.AddModelError("DepartmentId", "The DepartmentId field is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _employeeService.AddEmployeeAsync(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid || id != employee.Id)
                return BadRequest(ModelState);

            await _employeeService.UpdateEmployeeAsync(employee);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }
    }
}
