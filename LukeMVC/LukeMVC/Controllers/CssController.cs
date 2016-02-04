using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LukeMVC.Views;

namespace LukeMVC.Controllers
{
    public class CssController : Controller
    {
        // GET: Css
        public ActionResult Index()
        {
            return new CssViewResult();
        }
    }
}