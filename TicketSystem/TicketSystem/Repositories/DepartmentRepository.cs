using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TicketSystem.DbContext;
using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Repositories
{
    public class DepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _dbContext.Entry(department).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var employees = await _dbContext.Employees
            .Where(e => e.DepartmentId == departmentId)
            .ToListAsync();

            foreach (var employee in employees)
            {
                employee.DepartmentId = null;
                _dbContext.Entry(employee).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            var department = await _dbContext.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
            if (department != null)
            {
                _dbContext.Departments.Remove(department);

                await _dbContext.SaveChangesAsync();
            }
        }
    }
}