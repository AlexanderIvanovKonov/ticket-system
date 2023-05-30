using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task AddDepartmentAsync(Department department)
        {
            return _departmentRepository.AddDepartmentAsync(department);
        }

        public Task<Department> GetDepartmentByIdAsync(int id)
        {
            return _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _departmentRepository.GetAllDepartmentsAsync();
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            return _departmentRepository.UpdateDepartmentAsync(department);
        }

        public Task DeleteDepartmentAsync(int id)
        {
            return _departmentRepository.DeleteDepartmentAsync(id);
        }
    }
}
