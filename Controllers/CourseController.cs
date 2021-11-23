using AplicatieMeditatii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Controllers
{
    public class CourseController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        // GET: Course
        public ActionResult Index()
        {
            var Cursuri = db.Courses.AsQueryable();
            ViewBag.cursuri = Cursuri;
            var Materii = GetAllSubjects();
            ViewBag.materii = Materii;
            return View();
        }

        public ActionResult AddCourse()
        {
            Course course = new Course();
            course.Subjects = GetAllSubjects();
            return View(course);
        }

        [HttpPost]
        public ActionResult AddCourse(Course curs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(curs);
                    db.SaveChanges();
                    TempData["message"] = "Lectia a fost adaugata";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["massage"] = "Lectia nu a fost adaugata!";
                    return RedirectToAction("New");
                }
            }
            catch(Exception e)
            {
                TempData["massage"] = "Lectia nu a fost adaugata!";
                return RedirectToAction("New");
            }
        }



        [NonAction]
        public IEnumerable<SelectListItem> GetAllSubjects()
        {
            var selectList = new List<SelectListItem>();

            var subjects = db.Subjects;

            foreach (var subj in subjects)
            {
                var title = subj.Title.ToString();
                var year = subj.Year.ToString();
                selectList.Add(new SelectListItem
                {
                    Value = subj.Subjectid.ToString(),
                    Text = title + " " + year
                });
            }
            return selectList;
        }
    }
}