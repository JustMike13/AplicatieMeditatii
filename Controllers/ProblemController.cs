using AplicatieMeditatii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Controllers
{
    public class ProblemController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        // GET: Problem
        public ActionResult NoSubjectIndex()
        {
            var Materii = GetAllSubjects();
            ViewBag.materii = Materii;
            return View();
        }
        public ActionResult Index(int id)
        {
            var probleme = db.Problems.Where(p => p.SubjectId == id).AsQueryable();
            ViewBag.probleme = probleme;
            var Materie = db.Subjects.Find(id);
            ViewBag.materie = Materie;
            return View();
        }

        public ActionResult New()
        {
            Problem problem = new Problem();
            problem.Subjects = GetAllSubjects();
            problem.Title = "titlu";
            return View(problem);
        }

        [HttpPost]
        public ActionResult New(Problem problem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Problems.Add(problem);
                    db.SaveChanges();
                    TempData["message"] = "Problema a fost adaugata";
                    return RedirectToAction("Index/" + problem.SubjectId);
                }
                else
                {
                    ViewBag.message = "Problema nu a fost adaugata! else";
                    problem.Subjects = GetAllSubjects();
                    return View(problem);
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Problema nu a fost adaugata! catch";
                problem.Subjects = GetAllSubjects();
                return View(problem);
            }
        }

        public ActionResult Show(int id)
        {
            Problem problem = db.Problems.Find(id);

            problem.Correct = null; // Nu trimit raspunsul corect in View
            return View(problem);
        }

        public ActionResult Edit(int id)
        {
            Problem problema = db.Problems.Find(id);
            problema.Subjects = GetAllSubjects();
            return View(problema);
        }

        [HttpPut]
        public ActionResult Edit(Problem problemaNoua)
        {
            var id = problemaNoua.ProblemId;
            problemaNoua.Subjects = GetAllSubjects();
            try
            {
                if (ModelState.IsValid)
                {
                    Problem problema = db.Problems.Find(id);
                    if (TryUpdateModel(problema))
                    {
                        problema.Title = problemaNoua.Title;
                        problema.Question = problemaNoua.Question;
                        problema.a = problemaNoua.a;
                        problema.b = problemaNoua.b;
                        problema.c = problemaNoua.c;
                        problema.d = problemaNoua.d;
                        problema.Correct = problemaNoua.Correct;
                        problema.SubjectId = problemaNoua.SubjectId;
                        db.SaveChanges();
                        ViewBag.message = "Problema a fost modificata";
                        return RedirectToAction("Show/"+id);
                    }
                    else
                    {
                        ViewBag.message = "Problema nu a fost modificata 1 ";
                        return View(problemaNoua);
                    }
                }
                else
                {
                    ViewBag.message = "Problema nu a fost modificata 2 ";
                    return View(problemaNoua);
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Problema nu a fost modificata 3 ";
                return View(problemaNoua);
            }
        }



        public ActionResult Solve(int id1, int id2)
        {
            Problem problem = db.Problems.Find(id1);

            if (id2.ToString() == problem.Correct)
            {
                problem.Answer = "Correct";
            }
            else
            {
                problem.Answer = "Gresit";
                problem.Correct = null;
            }

            return View("./Show", problem);
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