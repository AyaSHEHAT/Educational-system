using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Repos
{
    public interface IDepartmentRepo
    {
        public List<Department> GetAll();
        public Department GetDeptById(int? id);
        public Department GetDeptWithStdById(int id);
        public Department GetDeptWithCoursesById(int id);
        public void DeleteDeptSoft(int id);
        public void DeleteDeptHard(int id);
        public void AddDept(Department dept);
        public void UpdateDept(Department dept);
    }
    public class DepartmentRepo : IDepartmentRepo
    {
        ITIContext db;
        public DepartmentRepo(ITIContext _db)
        {
            db=_db;
        }
        public List<Department> GetAll()
        {
            return db.Departments.Where(a=>a.Status==true).ToList();
        }
        public Department GetDeptById(int? id)
        {
            return db.Departments.Where(a => a.Status==true).FirstOrDefault(a => a.DeptId==id);
        }
        public Department GetDeptWithStdById(int id)
        {
            return db.Departments.Include(d => d.Students).FirstOrDefault(d => d.DeptId == id);
        }
        public Department GetDeptWithCoursesById(int id)
        {
            return db.Departments.Include(d => d.Courses).FirstOrDefault(d => d.DeptId == id);
        }
        public void DeleteDeptSoft(int id)
        {
            var dept = db.Departments.Where(a => a.Status==true).FirstOrDefault(a => a.DeptId==id);
            dept.Status=false;
            db.SaveChanges();
        }
        public void DeleteDeptHard(int id)
        {
            var dept = db.Departments.FirstOrDefault(a => a.DeptId==id);
            db.Departments.Remove(dept);
            db.SaveChanges();
        }
        public void AddDept(Department dept)
        {
            db.Departments.Add(dept);
            db.SaveChanges();
        }
        public void UpdateDept(Department dept)
        {
            var model = db.Departments.FirstOrDefault(a => a.DeptId==dept.DeptId);
            model.DeptName=dept.DeptName;
            model.Status=dept.Status;
            model.Capacity=dept.Capacity;
            //db.Departments.Update(model);
            db.SaveChanges();
        }
    }

    public class DepartmentMoc : IDepartmentRepo
    { 
        List<Department> db =
         [
             new Department() { DeptId=6, DeptName="mobile native", Capacity=5 },
             new Department() { DeptId=7, DeptName="UI-UX", Capacity=5 },

         ];
        public List<Department> GetAll()
        {
            return db.Where(a => a.Status==true).ToList();
        }
        public Department GetDeptById(int? id)
        {
            return db.Where(a => a.Status==true).FirstOrDefault(a => a.DeptId==id);
        }
        public void DeleteDeptSoft(int id)
        {
            var dept = db.Where(a => a.Status==true).FirstOrDefault(a => a.DeptId==id);
            dept.Status=false;
        }
        public void DeleteDeptHard(int id)
        {
            var dept = db.FirstOrDefault(a => a.DeptId==id);
            db.Remove(dept);
        }
        public void AddDept(Department dept)
        {
            db.Add(dept);
         
        }
        public void UpdateDept(Department dept)
        {
            var model = db.FirstOrDefault(a => a.DeptId==dept.DeptId);
            model.DeptName=dept.DeptName;
            model.Status=dept.Status;
            model.Capacity=dept.Capacity;
            
        }

        public Department GetDeptWithStdById(int id)
        {
            throw new NotImplementedException();
        }

        public Department GetDeptWithCoursesById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
