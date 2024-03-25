using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Demo1_Day2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Blazor;

namespace Demo1_Day2.Controllers
{
    public class StudentCoursesController : Controller
    {
       // ITIContext db=new ITIContext();
        IStudentRepo studentRepo;
        IDepartmentRepo departmentRepo;

        IStudentCourseRepo StdCrsRepo;
        public StudentCoursesController(IDepartmentRepo _departmentRepo, IStudentRepo _studentRepo, IStudentCourseRepo _StdCrsRepo)
        {
            studentRepo = _studentRepo;
            departmentRepo=_departmentRepo;
            StdCrsRepo=_StdCrsRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UpdateStudentDegrees(int deptId)
        {
            var model = departmentRepo.GetDeptWithStdById(deptId);
            var stdIds = model.Students.ToList();
            List<StudentCourse> degrees = new List<StudentCourse>();
            foreach (var item in stdIds)
            {
                var i1 = StdCrsRepo.GetDegrees(item.StudentId);
                if (i1!=null)
                    degrees.Add(i1);
                else
                    degrees.Add(new StudentCourse() { StdId=item.StudentId ,Degree=0});
                
            }
            ViewBag.LastDegrees= degrees;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateStudentDegrees(int crsId, Dictionary<int,int> stdDegree)
        {
            foreach (var item in stdDegree)
            {
                
                var model = StdCrsRepo.GetStudentCourse(item.Key , crsId);
                if (model != null)
                {
                    model.Degree = item.Value;
                }
                else
                {
                    StdCrsRepo.Add( item.Key, crsId ,item.Value);
                    
                }
            }
            return RedirectToAction("Index", "Department");
        }
    }
}
