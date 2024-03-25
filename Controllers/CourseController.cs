using Demo1_Day2.Data;
using Demo1_Day2.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo1_Day2.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepo crsRepo;
        public CourseController(ICourseRepo _crsRepo)
        {
            crsRepo = _crsRepo;
        }
        public IActionResult Index()
        {
            var courses= crsRepo.GetAll();
            return View(courses);
        }
        public IActionResult Create()
        {
            return View(new Course());
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            if(ModelState.IsValid)
            {
                bool isDuplicate = crsRepo.Duplicate(course.CrsName);
                if (isDuplicate)
                {
                    ModelState.AddModelError("CrsName", "Not Valid Course name, it already exists.");
                    return View(course);
                }
                try
                {
                    crsRepo.Add(course);
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("CrsId", "Not Valid Course Id, it already exists.");
                    return View(course);
                }

            
                return RedirectToAction("Index");
            }
                return View(course);
                    
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var crs = crsRepo.GetById(id);
            if (crs == null)
            {
                return NotFound();
            }
            crsRepo.Delete(crs);
            return RedirectToAction("Index");
        }
    }
}
