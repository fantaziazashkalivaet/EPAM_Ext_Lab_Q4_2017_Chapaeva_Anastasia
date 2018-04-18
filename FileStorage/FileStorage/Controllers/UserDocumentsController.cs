using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.Controllers
{
    public class UserDocumentsController : Controller
    {
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }
    }
}