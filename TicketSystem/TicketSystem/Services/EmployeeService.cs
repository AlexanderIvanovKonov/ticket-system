using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task AddEmployeeAsync(Employee employee)
        {
            return _employeeRepository.AddEmployeeAsync(employee);
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public Task<List<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            return _employeeRepository.GetEmployeesByDepartmentIdAsync(departmentId);
        }

        public Task<List<Employee>> GetAllEmployeesAsync()
        {
            return _employeeRepository.GetAllEmployeesAsync();
        }

        public Task UpdateEmployeeAsync(Employee employee)
        {
            return _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public Task DeleteEmployeeAsync(int id)
        {
            return _employeeRepository.DeleteEmployeeAsync(id);
        }
    }
}
