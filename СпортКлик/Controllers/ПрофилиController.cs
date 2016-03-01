using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using СпортКлик.Models;
using СпортКлик.Сервис;

namespace СпортКлик.Controllers
{
    [Authorize]
    public class ПрофилиController : Controller
    {
        public ActionResult Детально()
        {
            ПрофильПользователя profile = new ПрофильПользователя(User.Identity.Name);
            return View(profile);
        }
        public ActionResult Изменить()
        {
            ПрофильПользователя profile = new ПрофильПользователя(User.Identity.Name);
            return View(profile);
        }
        [HttpPost]
        public ActionResult Изменить(FormCollection collection)
        {
            ПрофильПользователя profile = new ПрофильПользователя(User.Identity.Name);
            if (ModelState.IsValid)
            {
                UpdateModel(profile, collection);
                profile.Save();
                TempData.Add(Alerts.INFORMATION, "Профиль изменен!");
            }
            return RedirectToAction(Действия.Все, Контролеры.Категории);
        }

    }
}
