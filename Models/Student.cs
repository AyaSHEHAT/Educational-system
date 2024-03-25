using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1_Day2.Models
{
    public class Student
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentId { get; set; }
        [Required]
        [StringLength(15 ,MinimumLength = 3)]
        public string StudentName { get; set; }
        [Range(20,30)]
        //[Compare("StudentId")]
        public int Age { get; set;}
        [Required]
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]{2,4}")]
        public string Email { get; set;}
        public string? StdImgName { get; set; }
        public int DeptNo { get; set;}
        [ForeignKey("DeptNo")]
        public Department DepartmentNavigation { get; set; }
        public List<StudentCourse> StudentCourse { get; set;}=new List<StudentCourse>();

   
    }
}
