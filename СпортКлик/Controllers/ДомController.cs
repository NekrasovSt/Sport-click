using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace СпортКлик.Controllers
{
    public class ДомController : Controller
    {
        //
        // GET: /Дом/

        public ActionResult Онас()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
