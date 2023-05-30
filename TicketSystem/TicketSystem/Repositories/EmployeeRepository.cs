using Microsoft.EntityFrameworkCore;
using TicketSystem.DbContext;
using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        // works perfect, until you try to get department name when department id is null
        // raw sql works, but don't have the time to do it with the framework
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employees
                .Where(e => e.Id == id)
                .Join(
                    _dbContext.Departments,
                    employee => employee.DepartmentId,
                    department => department.Id,
                    (employee, department) => new Employee(employee, department.Name))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await _dbContext.Employees
                .Join(
                    _dbContext.Departments,
                    employee => employee.DepartmentId,
                    department => department.Id,
                    (employee, department) => new { Employee = employee, DepartmentName = department.Name })
                .ToListAsync();

            List<Employee> result = new List<Employee>();

            foreach (var item in employees)
            {
                if (item.Employee != null && !string.IsNullOrEmpty(item.DepartmentName))
                {
                    var employee = new Employee(item.Employee, item.DepartmentName);
                    result.Add(employee);
                }
            }

            return result;
        }

        // raw sql query examples
        /*
            SELECT employee.id, employee.name, employee.lastname, department.name AS department_name
            FROM employee
            LEFT JOIN department ON employee.department_id = department.id;
         */

        /*
            SELECT employee.id, employee.name, employee.lastname, COALESCE(department.name, 'No department') AS department_name
            FROM employee
            LEFT JOIN department ON employee.department_id = department.id;
         */

        //get all employees by department
        public async Task<List<Employee>> GetEmployeesByDepartmentIdAsync(int departmentId)
        {
            var list = await _dbContext.Employees
                .Where(e => e.DepartmentId == departmentId)
                .Join(
                    _dbContext.Departments,
                    employee => employee.DepartmentId,
                    department => department.Id,
                    (employee, department) => new Employee(employee, department.Name))
                .ToListAsync();

            return list;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Entry(employee).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            /* 
            -- Set the foreign key column in the "task" table to NULL for the employee to be deleted
            UPDATE task SET employee_id = NULL WHERE employee_id = <employee_id>;

            -- Delete the employee
            DELETE FROM employee WHERE id = <employee_id>;

            -- Delete the task if it has no employee (if needed??)
             */

            var employee = await GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }

        // TODO - using the framework
        //public async Task DeleteEmployeeAsync(int id)
        //{
        //    var employee = await _dbContext.Employees.FindAsync(id);
        //    if (employee != null)
        //    {
        //        // Set the foreign key column in the "task" table to NULL for the employee to be deleted
        //        var relatedTasks = await _dbContext.Tasks.Where(t => t.EmployeeId == id).ToListAsync();
        //        foreach (var task in relatedTasks)
        //        {
        //            task.EmployeeId = null;
        //        }

        //        _dbContext.Employees.Remove(employee);
        //        await _dbContext.SaveChangesAsync();
        //    }
        //}
    }
}
