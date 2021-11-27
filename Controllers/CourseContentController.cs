using AplicatieMeditatii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicatieMeditatii.Controllers
{
    public class CourseContentController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        // GET: CourseContent
        public ActionResult NewText(int id)
        {
            CourseContent content = new CourseContent();
            content.Courseid = id;
            content.Type = "text";
            return View(content);
        }
        public ActionResult NewImage(int id)
        {
            // id == CourseId
            var maxIndexContent = db.CourseContents.Where(p => p.Courseid == id).OrderByDescending(p => p.Index);
            var index = maxIndexContent.First().Index + 1;
            CourseContent content = new CourseContent();
            content.Courseid = id;
            content.Type = "image";
            content.Index = index;
            return View(content);
        }

        [HttpPost]
        public ActionResult New(CourseContent content)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CourseContents.Add(content);
                    db.SaveChanges();
                    TempData["message"] = "Content-ul a fost adaugat";
                    return RedirectToAction("Show/" + content.Courseid, "Course");
                }
                else
                {
                    TempData["massage"] = "Content-ul nu a fost adaugat! else";
                    if(content.Type == "text")
                    {
                        return View(content);
                    }else
                    {
                        return View(content);
                    }

                }
            }
            catch (Exception e)
            {
                TempData["massage"] = "Content-ul nu a fost adaugat! catch";
                if (content.Type == "text")
                {
                    return View(content);
                }
                else
                {
                    return View(content);
                }
            }
        }

        public ActionResult Edit(int id)
        {
            CourseContent con = db.CourseContents.Find(id);
            return View(con);
        }

        [HttpPut]
        public ActionResult Edit(CourseContent newContent)
        {
            var id = newContent.Id;
            try
            {
                if (ModelState.IsValid)
                {
                    CourseContent content = db.CourseContents.Find(id);
                    if (TryUpdateModel(content))
                    {
                        content.Text = newContent.Text;
                        content.ImagePath = newContent.ImagePath;
                        db.SaveChanges();
                        ViewBag.message = "Content-ul a fost modificat";
                        return RedirectToAction("Show/" + newContent.Courseid,"Course");
                    }
                    else
                    {
                        ViewBag.message = "Content-ul nu a fost modificat else 1";
                        return View(newContent);
                    }
                }
                else
                {
                    ViewBag.message = "Content-ul nu a fost modificat else 2";
                    return View(newContent);
                }
            }
            catch (Exception e)
            {
                ViewBag.message = "Content-ul nu a fost modificat catch";
                return View(newContent);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            CourseContent content = db.CourseContents.Find(id);
            var courseId = content.Courseid;
            var index = content.Index;
            var otherContents = db.CourseContents.Where(p => p.Courseid == courseId);
            foreach(var oc in otherContents)
            {
                if(oc.Index > index)
                {
                    if (TryUpdateModel(oc))
                    {
                        oc.Index = oc.Index - 1;
                        db.SaveChanges();
                    }
                }
                
            }

            db.CourseContents.Remove(content);
            db.SaveChanges();
            TempData["message"] = "Content șters cu succes.";
            return RedirectToAction("Show/" + courseId, "Course");
        }





    }
}