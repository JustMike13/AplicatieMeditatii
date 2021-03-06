using AplicatieMeditatii.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
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
            var index = 0;
            var Contents = db.CourseContents.Where(p => p.Courseid == id);
            foreach(var con in Contents)
            {
                if (con.Index > index)
                    index = con.Index;
            }
            index += 1;
            content.Courseid = id;
            content.Type = "text";
            content.Index = index;
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
        public ActionResult NewText(CourseContent content)
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

        [HttpPost]
        public ActionResult NewImage(CourseContent content, HttpPostedFileBase uploadedImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileExtension = Path.GetExtension(uploadedImage.FileName);
                    string fileName = "Image_" + content.Id + fileExtension;
                    string path = "~/images/Course" + content.Courseid + "/";

                    content.ImagePath = Path.Combine(path.Remove(0, 1), fileName);

                    string absolutePath = HostingEnvironment.MapPath(path);
                    if (!System.IO.Directory.Exists(absolutePath))
                        System.IO.Directory.CreateDirectory(absolutePath);

                    uploadedImage.SaveAs(Path.Combine(absolutePath, fileName)); // The photo is saved in the project's directory
                    db.CourseContents.Add(content);
                    db.SaveChanges();
                    TempData["message"] = "Content-ul a fost adaugat";
                    return RedirectToAction("Show/" + content.Courseid, "Course");
                }
                else
                {
                    TempData["massage"] = "Content-ul nu a fost adaugat! else";
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
            

            db.CourseContents.Remove(content);
            db.SaveChanges();
            TempData["message"] = "Content șters cu succes.";
            return RedirectToAction("Show/" + courseId, "Course");
        }





    }
}