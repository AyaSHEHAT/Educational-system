using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Repos
{
    public interface IStudentRepo
    {
        List<Student> GetAll();
        Student GetStdById(int id);
        void Add(Student student);
        void DeleteStd(int id);
        void UpdateStd(Student student);
        public void UpdateStudentImage(string stdImageName, int stdId);
    }
    public class StudentRepo:IStudentRepo
    {
        ITIContext db;
        public StudentRepo(ITIContext _db)
        {
            db=_db;
        }
        public List<Student> GetAll()
        {
            return db.Students.Include(a => a.DepartmentNavigation).ToList();
        }
        public Student GetStdById(int id)
        {
            return db.Students.FirstOrDefault(a => a.StudentId==id);
        }
        public void Add(Student std)
        {
            db.Students.Add(std);
            db.SaveChanges();
        }

        public void DeleteStd(int id)
        {
            var std = db.Students.FirstOrDefault(a => a.StudentId==id);
            db.Students.Remove(std);
            db.SaveChanges();
        }
        public void UpdateStd(Student std)
        {
            var model = db.Students.FirstOrDefault(a => a.StudentId==std.StudentId);
            model.StudentName=std.StudentName;
            model.Age=std.Age;
            model.Email=std.Email;
            db.Students.Update(model);
            db.SaveChanges();
        }
        public void UpdateStudentImage(string stdImageName, int stdId)
        {
            var std = db.Students.FirstOrDefault(s => s.StudentId == stdId);
            std.StdImgName = stdImageName;
            db.SaveChanges();
        }
    }

    public class StudentMoc : IStudentRepo
    {
        List<Student> db =
         [
             new Student() { StudentId=1, StudentName="Aya", Age=25, Email="aya@asdf"},
             new Student() { StudentId=2, StudentName="Asmaa", Age=23, Email="asmaa@qwer" },

         ];
        public List<Student> GetAll()
        {
            return db.ToList();
        }
        public Student GetStdById(int id)
        {
            return db.FirstOrDefault(a => a.StudentId==id);
        }
        public void Add(Student std)
        {
            db.Add(std);
        }

        public void DeleteStd(int id)
        {
            var std = db.FirstOrDefault(a => a.StudentId==id);
            db.Remove(std);
        }
        public void UpdateStd(Student std)
        {
            var model = db.FirstOrDefault(a => a.StudentId==std.StudentId);
            model.StudentName=std.StudentName;
            model.Age=std.Age;
            model.Email=std.Email;
            
        }

        public void UpdateStudentImage(string stdImageName, int stdId)
        {
            throw new NotImplementedException();
        }
    }
}
