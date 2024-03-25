using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Demo1_Day2.Repos
{
    public interface IDeptCourseRepo
    {
        public void Saving();
    }
    public class DeptCourseRepo : IDeptCourseRepo
    {
        ITIContext db;
        public DeptCourseRepo(ITIContext _db)
        {
            db=_db;
        }
        public void Saving()
        {
            db.SaveChanges();
        }
    }
}
