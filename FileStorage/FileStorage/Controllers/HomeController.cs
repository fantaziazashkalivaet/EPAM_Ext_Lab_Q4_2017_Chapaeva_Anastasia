namespace FileStorage.Controllers
{
    using System;
    using System.Web.Mvc;

    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Home/Error");
            }
        } 
    }
}