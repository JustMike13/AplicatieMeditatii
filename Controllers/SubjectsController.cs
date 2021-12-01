using AplicatieMeditatii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Controllers
{
    public class SubjectsController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        // GET: Subjects
        public ActionResult Index()
        {
            var Materii = db.Subjects.AsQueryable();
            ViewBag.materii = Materii;
            return View();
        }
        public ActionResult New()
        {
            Subject subject = new Subject();
            return View(subject);
        }

        [HttpPost]
        public ActionResult New(Subject materie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Subjects.Add(materie);
                    db.SaveChanges();
                    TempData["message"] = "Materia a fost adaugata";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["massage"] = "Materia nu a fost adaugata!";
                    return RedirectToAction("New");
                }
            }
            catch (Exception e)
            {
                TempData["massage"] = "Materia nu a fost adaugata!";
                return RedirectToAction("New");
            }
        }

        public ActionResult Edit(int id)
        {
            Subject materie = db.Subjects.Find(id);
            return View(materie);
        }

        [HttpPut]
        public ActionResult Edit(Subject materienoua)
        {
            var id = materienoua.Subjectid;
            try
            {
                if (ModelState.IsValid)
                {
                    Subject materie = db.Subjects.Find(id);
                    if (TryUpdateModel(materie))
                    {
                        materie.Title = materienoua.Title;
                        materie.Year = materienoua.Year;
                        db.SaveChanges();
                        ViewBag.message = "Materia a fost modificata";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.message = "Materia nu a fost modificata";
                        return View(materienoua);
                    }
                }
                else
                {
                    ViewBag.message = "Materia nu a fost modificata";
                    return View(materienoua);
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Materia nu a fost modificata";
                return View(materienoua);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Subject materie = db.Subjects.Find(id);

            db.Subjects.Remove(materie);
            db.SaveChanges();
            TempData["message"] = "Materie ștearsa cu succes.";
            return RedirectToAction("Index");
        }




    }
    }