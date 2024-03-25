using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Demo1_Day2.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public int DeptId { get; set; }
        [Remote("CheckDeptId","Department")]

        [Required]
        
        public string DeptName { get; set; }
        public int Capacity { get; set; }
        
        public bool Status { get; set; }
        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public List<Course> Courses { get; set; }=new List<Course>();

    }
}
