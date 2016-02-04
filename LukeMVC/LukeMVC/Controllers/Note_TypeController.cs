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
    public class Note_TypeController : Controller
    {
        private Luke2432Entities db = new Luke2432Entities();

        // GET: Note_Type
        public ActionResult Index()
        {
            return View(db.Note_Type.ToList());
        }

        // GET: Note_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note_Type note_Type = db.Note_Type.Find(id);
            if (note_Type == null)
            {
                return HttpNotFound();
            }
            return View(note_Type);
        }

        // GET: Note_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "note_type_id,note_type_description")] Note_Type note_Type)
        {
            if (ModelState.IsValid)
            {
                db.Note_Type.Add(note_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(note_Type);
        }

        // GET: Note_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note_Type note_Type = db.Note_Type.Find(id);
            if (note_Type == null)
            {
                return HttpNotFound();
            }
            return View(note_Type);
        }

        // POST: Note_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "note_type_id,note_type_description")] Note_Type note_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(note_Type);
        }

        // GET: Note_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note_Type note_Type = db.Note_Type.Find(id);
            if (note_Type == null)
            {
                return HttpNotFound();
            }
            return View(note_Type);
        }

        // POST: Note_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note_Type note_Type = db.Note_Type.Find(id);
            db.Note_Type.Remove(note_Type);
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
