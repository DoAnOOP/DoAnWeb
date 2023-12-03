using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test1.Areas.Users.Controllers
{
    public class ProductController : Controller
    {
        // GET: Users/Product
        public ActionResult Product2()
        {
            return View();
        }
        public ActionResult categorie()
        {
            return View();
        }
        public ActionResult cart()
        {
            return View();
        }
        public ActionResult checkout()
        {
            return View();
        }
    }
}