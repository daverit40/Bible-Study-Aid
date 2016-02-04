using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LukeMVC.Models;

namespace LukeMVC.Controllers
{
    public class verse_categoryController : Controller
    {
        private Luke2432Entities db = new Luke2432Entities();

        // GET: verse_category
        public ActionResult Index()
        {
            return View(db.verse_category.ToList());
        }

        public ActionResult listVerseCats()
        {
            return PartialView("VerseCatList", db.verse_category.ToList());
        }

        // GET: verse_category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category verse_category = db.verse_category.Find(id);
            if (verse_category == null)
            {
                return HttpNotFound();
            }
            return View(verse_category);
        }

        // GET: verse_category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: verse_category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "verse_category_id,verse_category_description,verse_category_name,active,text_color_name,text_color_code,text_size,text_bold,background_color_name,background_color_code,css_class_name,user_id")] verse_category verse_category)
        {
            if (ModelState.IsValid)
            {
                db.verse_category.Add(verse_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(verse_category);
        }

        // GET: verse_category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category verse_category = db.verse_category.Find(id);
            if (verse_category == null)
            {
                return HttpNotFound();
            }
            return View(verse_category);
        }

        // POST: verse_category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "verse_category_id,verse_category_description,verse_category_name,active,text_color_name,text_color_code,text_size,text_bold,background_color_name,background_color_code,css_class_name,user_id")] verse_category verse_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verse_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(verse_category);
        }

        // GET: verse_category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category verse_category = db.verse_category.Find(id);
            if (verse_category == null)
            {
                return HttpNotFound();
            }
            return View(verse_category);
        }

        // POST: verse_category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            verse_category verse_category = db.verse_category.Find(id);
            db.verse_category.Remove(verse_category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
