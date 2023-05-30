
namespace TicketSystem.Models
{
    public class ProjectTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }

        public ProjectTaskDTO()
        {
        }

        public ProjectTaskDTO(ProjectTask projectTask, string employeeName, string employeeLastName)
        {
            Id = projectTask.Id;
            Name = projectTask.Name;
            Description = projectTask.Description;
            EmployeeId = projectTask.EmployeeId;
            CreatedDate = projectTask.CreatedDate;
            EmployeeName = employeeName;
            EmployeeLastName = employeeLastName;
        }
    }
}