using Demo1_Day2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo1_Day2
{
    public class StudentCourse
    {
        [ForeignKey("StudentNavigation")]
        public int StdId { get; set; }

        [ForeignKey("CourseNavigation")] 
        public int CrsId { get; set; }
        public int Degree { get; set; }
        public Student StudentNavigation { get; set; }
        public Course CourseNavigation { get; set;}

    }
}
