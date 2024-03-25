using Demo1_Day2.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Repos
{
    public interface IStudentCourseRepo
    {
        public void Add(int stdId, int crsId, int degree);
        public StudentCourse GetStudentCourse(int _stdid, int _crsId);
        public StudentCourse GetDegrees(int _stdid);
    }
    public class StudentCourseRepo:IStudentCourseRepo
    {
        ITIContext db;
        public StudentCourseRepo(ITIContext _db)
        {
            db=_db;
        }
        public void Add(int stdId, int crsId, int degree)
        {
            db.StudentCourse.Add(new StudentCourse
            {
                StdId = stdId,
                CrsId = crsId,
                Degree = degree
            });
            db.SaveChanges();
        }
        public StudentCourse GetStudentCourse(int _stdid, int _crsId)
        {
            return db.StudentCourse.FirstOrDefault(sc => sc.StdId == _stdid  && sc.CrsId == _crsId);
        }
        public StudentCourse GetDegrees(int _stdid)
        {
            return db.StudentCourse.FirstOrDefault(s => s.StdId == _stdid && s.Degree != null);
        }

    }
}
