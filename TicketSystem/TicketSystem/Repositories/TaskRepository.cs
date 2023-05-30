using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TicketSystem.DbContext;
using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public class TaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTaskAsync(ProjectTask task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProjectTaskDTO> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Tasks
                .Where(task => task.Id == id)
                .GroupJoin(
                    _dbContext.Employees,
                    task => task.EmployeeId,
                    employee => employee.Id,
                    (task, employees) => new { Task = task, Employees = employees })
                .SelectMany(
                    x => x.Employees.DefaultIfEmpty(),
                    (taskWithEmployees, employee) => new ProjectTaskDTO
                    {
                        Id = taskWithEmployees.Task.Id,
                        Name = taskWithEmployees.Task.Name,
                        Description = taskWithEmployees.Task.Description,
                        EmployeeId = taskWithEmployees.Task.EmployeeId,
                        EmployeeName = employee != null ? employee.Name : null,
                        EmployeeLastName = employee != null ? employee.Lastname : null,
                        CreatedDate = taskWithEmployees.Task.CreatedDate
                    })
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProjectTaskDTO>> GetAllTasksAsync()
        {
            return await _dbContext.Tasks
                .GroupJoin(
                    _dbContext.Employees,
                    task => task.EmployeeId,
                    employee => employee.Id,
                    (task, employees) => new { Task = task, Employees = employees })
                .SelectMany(
                    x => x.Employees.DefaultIfEmpty(),
                    (taskWithEmployees, employee) => new ProjectTaskDTO
                    {
                        Id = taskWithEmployees.Task.Id,
                        Name = taskWithEmployees.Task.Name,
                        Description = taskWithEmployees.Task.Description,
                        EmployeeId = taskWithEmployees.Task.EmployeeId,
                        EmployeeName = employee != null ? employee.Name : null,
                        EmployeeLastName = employee != null ? employee.Lastname : null,
                        CreatedDate = taskWithEmployees.Task.CreatedDate
                    })
                .ToListAsync();
        }

        //TODO get all task by employee

        public async Task UpdateTaskAsync(ProjectTask task)
        {
            _dbContext.Entry(task).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = new ProjectTask { Id = id };
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsyncV2(int id)
        {
            var taskDTO = await GetTaskByIdAsync(id);

            if (taskDTO != null)
            {
                //we can add checks here

                var task = new ProjectTask();
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
