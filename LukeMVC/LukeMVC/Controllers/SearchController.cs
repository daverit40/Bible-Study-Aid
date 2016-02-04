using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LukeMVC.Models;
using ESVTest.Classes;
using LukeMVC.Classes;

namespace LukeMVC.Controllers
{

    public class SearchController : Controller
    {

        private Luke2432Entities db = new Luke2432Entities();
        // GET: Search
        public ActionResult Index()
        {
           return View(db.Notes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string txtSearch)
        {
            //esvWrapper wrapper = new esvWrapper("passageQuery", txtSearch, "crossway-xml-1.0");
            TempData["SearchText"] = txtSearch;

            return Search(txtSearch);
        }

        public ActionResult Search(string txtSearch)
        {
            TempData["SearchText"] = txtSearch;

            esvWrapper wrapper = new esvWrapper("passageQuery", txtSearch, "crossway-xml-1.0");

            ViewBag.Notes = db.Notes.ToList();
            SearchResultsModel search = new SearchResultsModel();

            search = wrapper.getEsvXml();

            return View("SearchResults", search);
        }

        public ActionResult searchXML(string passage)
        {
            esvWrapper wrapper = new esvWrapper("passageQuery", passage, "crossway-xml-1.0");
            string xml = "";
            xml = wrapper.getEsvXml().ToString();


            ViewBag["returnXML"] = wrapper.getEsvXml();
            return View("searchXML");
        }





        /*

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        */
    }
}
