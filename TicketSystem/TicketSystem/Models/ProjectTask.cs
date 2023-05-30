using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models
{
    [Table("task")]
    public class ProjectTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("employee")]
        [Column("employee_id")]
        public int? EmployeeId { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        public ProjectTask()
        {
        }

        public ProjectTask(int id, string name, string description, int? employeeId, DateTime createdDate)
        {
            Id = id;
            Name = name;
            Description = description;
            EmployeeId = employeeId;
            CreatedDate = createdDate;
        }

        public ProjectTask(ProjectTaskDTO projectTaskDTO)
        {
            Id = projectTaskDTO.Id;
            Name = projectTaskDTO.Name;
            Description = projectTaskDTO.Description;
            EmployeeId = projectTaskDTO.EmployeeId;
            CreatedDate = projectTaskDTO.CreatedDate;
        }
    }
}
