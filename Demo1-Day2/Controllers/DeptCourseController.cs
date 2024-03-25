using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Demo1_Day2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Controllers
{
    public class DeptCourseController : Controller
    {
        //ITIContext db = new ITIContext();
        ICourseRepo crsRepo;
        IDepartmentRepo deptRepo;
        IDeptCourseRepo deptCrsRepo;
        public DeptCourseController(ICourseRepo _crsRepo, IDepartmentRepo _deptRepo, IDeptCourseRepo _deptCrsRepo)
        {
            crsRepo = _crsRepo;
            deptRepo = _deptRepo;
            deptCrsRepo = _deptCrsRepo;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowCoursesByDept(int id)
        { 
            var dept = deptRepo.GetDeptWithCoursesById(id);
            return View(dept);
        }
        [HttpGet]
        public IActionResult ManageDeptCourses(int id)
        {
            var dept = deptRepo.GetDeptWithCoursesById(id);
            var courses = dept.Courses;
            var allCourses = crsRepo.GetAll();
            var notInDept = allCourses.Except(courses).ToList();
            ViewBag.Dept = dept;
            ViewBag.NotInDept = notInDept;
            return View();
        }
        [HttpPost]
        public IActionResult ManageDeptCourses(int deptId, List<int> coursesToAdd,List<int> coursesToRemove)
        {
            
            var dept = deptRepo.GetDeptWithCoursesById(deptId);
            foreach (var item in coursesToRemove)
            {
                Course course =crsRepo.GetById(item);
                dept.Courses.Remove(course);
            }
            foreach (var item in coursesToAdd)
            {
                Course course = crsRepo.GetById(item);
                dept.Courses.Add(course);
            }
            deptCrsRepo.Saving();
            return RedirectToAction("Index", "Department");
        }

    }
}
