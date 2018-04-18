using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.Controllers
{
    public class BaseDocumentsController : Controller
    {
        // GET: BaseDocuments
        public ActionResult Index()
        {
            return View();
        }
    }
}