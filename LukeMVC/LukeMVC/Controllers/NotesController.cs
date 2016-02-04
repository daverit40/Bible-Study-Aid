using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
//using System.Data.Objects;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LukeMVC.Models;

namespace LukeMVC.Controllers
{
    public class NotesController : Controller
    {
        private Luke2432Entities db = new Luke2432Entities();

        // GET: Notes
        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Note_Type);
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.note_type_id = new SelectList(db.Note_Type, "note_type_id", "note_type_description");

           // .note_id = db.Notes.OrderByDescending(n => n.note_id).FirstOrDefault().note_id + 1;
            //ViewData.Add("noteId", db.Notes.OrderByDescending(n => n.note_id).FirstOrDefault().note_id + 1);
            return PartialView();// View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "note_id,note_type_id,note_title,passage,note_body,user,modified")] Note note)
        {
            if (ModelState.IsValid)
            {
                int newNoteId = 0;
               
                var lst = db.Notes.OrderByDescending(n => n.note_id).ToList();

                if (lst.Count > 0)
                {
                    newNoteId = lst.FirstOrDefault().note_id + 1;
                }
                else
                {
                    newNoteId = 1;
                }                

                note.note_id = newNoteId;

               // note.note_id = int.Parse(nt.Max().note_id.ToString()) + 1;
                note.modified = DateTime.Now;
                note.user = "dyork";
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Search", "Search", new { txtSearch = TempData["SearchText"] });
            }

            ViewBag.note_type_id = new SelectList(db.Note_Type, "note_type_id", "note_type_description", note.note_type_id);
            return View(note);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.note_type_id = new SelectList(db.Note_Type, "note_type_id", "note_type_description", note.note_type_id);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "note_id,note_type_id,passage,note_body,user,modified")] Note note)
        {
            if (ModelState.IsValid)
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.note_type_id = new SelectList(db.Note_Type, "note_type_id", "note_type_description", note.note_type_id);
            return View(note);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = db.Notes.Find(id);
            db.Notes.Remove(note);
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
