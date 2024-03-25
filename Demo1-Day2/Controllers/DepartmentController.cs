using Demo1_Day2.Controllers.CustomFilters;
using Demo1_Day2.Data;
using Demo1_Day2.Models;
using Demo1_Day2.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Demo1_Day2.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepo deptRepo;
       
        public DepartmentController( IDepartmentRepo _deptrepo)
        {
            deptRepo=_deptrepo;
        }
        public IActionResult Index()
        {
            var departments= deptRepo.GetAll();
            return View(departments);
        }
        // [AuthFilter]
      
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new Department());
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (ModelState.IsValid)
            {
                dept.Status=true;
                deptRepo.AddDept(dept);
                return RedirectToAction("Index");
            }
            return View(dept);           
        }
       [Authorize(Roles = "Student")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();//return 400 status code
            }
            var dept = deptRepo.GetDeptById(id);
            if (dept == null)
            {
                return NotFound();//return 404 status code
            }
            return View(dept);
        }
        //[Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var dept = deptRepo.GetDeptById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }
        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (dept.DeptId==0 || dept.DeptName==null)
            {
                return View(dept);
            }
            deptRepo.UpdateDept(dept);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var dept = deptRepo.GetDeptById(id);
            if (dept == null)
            {
                return NotFound();
            }
            deptRepo.DeleteDeptSoft(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckDeptId(int deptId)
        {
            var model = deptRepo.GetDeptById(deptId);
            if (model == null)
            {
                return Json(true);
            }
          //  int x = db.Departments.Max(d => d.DeptId);
            return Json(false);
        }
    }
}
