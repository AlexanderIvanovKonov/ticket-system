using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Lastname { get; set; }

        [ForeignKey("department")]
        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [NotMapped]
        public string? DepartmentName { get; set; }


        public string Skills { get; set; }


        public Employee()
        {
        }

        public Employee(Employee employee, string? departmentName)
        {
            Id = employee.Id;
            Name = employee.Name;
            Lastname = employee.Lastname;
            DepartmentId = employee.DepartmentId;
            DepartmentName = departmentName;
            Skills = employee.Skills;
        }
    }
}
