using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Demo1_Day2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Controllers
{
    public class StudentController : Controller
    {
        IDepartmentRepo deptRepo;
        IStudentRepo stdRepo;
        public StudentController(IStudentRepo _studentRepo, IDepartmentRepo _deptrepo)
        {
            stdRepo = _studentRepo;
            deptRepo= _deptrepo;
        }
        public IActionResult Index()
        {
            var students = stdRepo.GetAll();

            return View(students);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = deptRepo.GetAll();
            return View(new Student());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student std,IFormFile stdImg)
        {
            if (ModelState.IsValid)
            {
                var department = deptRepo.GetDeptWithStdById(std.DeptNo);
                if (department != null)
                {
                    if (department.Students.Count() >= department.Capacity)
                    {
                        ModelState.AddModelError("Capacity", $"The department can only have {department.Capacity} students.Unfortunately, this department has reached its limit, choose another one. ");
                        ViewBag.Departments = deptRepo.GetAll();
                        return View(std);
                    }
                    stdRepo.Add(std);
                    if(stdImg == null)
                    {
                        ModelState.AddModelError("StdImgName", $"Choose an image please");
                        ViewBag.Departments = deptRepo.GetAll();
                        return View(std);
                    }
                    string filename = $"{std.StudentId}.{stdImg.FileName.Split(".").Last()}";
                    using(var fs=new FileStream("wwwroot/images/"+filename, FileMode.Create))
                    {
                        await stdImg.CopyToAsync(fs);
                        std.StdImgName=filename;
                        stdRepo.UpdateStudentImage(filename, std.StudentId);
                    }
                    return RedirectToAction("Index");
                }
               
            }
            ViewBag.Departments = deptRepo.GetAll();
            return View(std);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var std = stdRepo.GetStdById(id??0);
            if (std == null)
            {
                return NotFound();
            }
            ViewBag.Departments = deptRepo.GetAll();
            return View(std);
        }
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (std.StudentId==0 || std.StudentName==null)
            {
                return View(std);
            }
            stdRepo.UpdateStd(std);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var std = stdRepo.GetStdById(id);
            if (std == null)
            {
                return NotFound();
            }
            stdRepo.DeleteStd(id);
            return RedirectToAction("Index");
        }

       
    }
}
