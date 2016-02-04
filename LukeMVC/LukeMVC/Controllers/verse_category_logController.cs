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
    public class verseCategoryLogController : Controller
    {
        private Luke2432Entities db = new Luke2432Entities();

        // GET: verse_category_log
        public ActionResult Index()
        {
            var verse_category_log = db.verse_category_log.Include(v => v.verse_category);
            return View(verse_category_log.ToList());
        }

        // GET: verse_category_log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category_log verse_category_log = db.verse_category_log.Find(id);
            if (verse_category_log == null)
            {
                return HttpNotFound();
            }
            return View(verse_category_log);
        }

        // GET: verse_category_log/Create
        public ActionResult Create()
        {
            ViewBag.verse_category_id = new SelectList(db.verse_category, "verse_category_id", "verse_category_description");
            return View();
        }

        // POST: verse_category_log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "verse_id,verse_category_id")] verse_category_log verse_category_log)
        {
            if (ModelState.IsValid)
            {
                db.verse_category_log.Add(verse_category_log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.verse_category_id = new SelectList(db.verse_category, "verse_category_id", "verse_category_description", verse_category_log.verse_category_id);
            return View(verse_category_log);
        }

        // GET: verse_category_log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category_log verse_category_log = db.verse_category_log.Find(id);
            if (verse_category_log == null)
            {
                return HttpNotFound();
            }
            ViewBag.verse_category_id = new SelectList(db.verse_category, "verse_category_id", "verse_category_description", verse_category_log.verse_category_id);
            return View(verse_category_log);
        }

        // POST: verse_category_log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "verse_id,verse_category_id")] verse_category_log verse_category_log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verse_category_log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.verse_category_id = new SelectList(db.verse_category, "verse_category_id", "verse_category_description", verse_category_log.verse_category_id);
            return View(verse_category_log);
        }

        // GET: verse_category_log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            verse_category_log verse_category_log = db.verse_category_log.Find(id);
            if (verse_category_log == null)
            {
                return HttpNotFound();
            }
            return View(verse_category_log);
        }

        // POST: verse_category_log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            verse_category_log verse_category_log = db.verse_category_log.Find(id);
            db.verse_category_log.Remove(verse_category_log);
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
