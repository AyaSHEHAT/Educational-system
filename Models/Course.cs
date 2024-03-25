using Demo1_Day2.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo1_Day2
{
    public class Course
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(100, int.MaxValue)]  
        public int CrsId { get; set; }
        [Required]
        public string CrsName { get; set; }
        public int? Duration { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<StudentCourse> StudentCourse { get; set; }= new List<StudentCourse>();
        
    }
}
