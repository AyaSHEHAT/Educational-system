using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo1_Day2.Repos
{
    public interface ICourseRepo
    {
        public List<Course> GetAll();
        public Course GetById(int id);
        public bool Duplicate(string name);
        public void Add(Course course);
        public void Delete(Course course);
       
    }
    public class CourseRepo:ICourseRepo
    {
        ITIContext db;
        public CourseRepo(ITIContext _db)
        {
            db=_db;
        }
        public List<Course> GetAll()
        {
              return db.Courses.ToList();
           
        }
        public Course GetById(int id)
        {
            return db.Courses.FirstOrDefault(d => d.CrsId == id);

        }
        public bool Duplicate(string name)
        {
            return db.Courses.Any(c => c.CrsName == name);
        }
        public void Add(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        public void Delete(Course course)
        {
            
            db.Courses.Remove(course);
            db.SaveChanges();
        }
    }
}
