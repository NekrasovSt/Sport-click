using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using СпортКлик.Сервис;

namespace СпортКлик.Areas.Admin.Controllers
{
    [Authorize(Roles = РолиПользователей.Администратор)]
    public class ПользователиController : Controller
    {

        public ActionResult Все()
        {
            MembershipUserCollection пользователи = Membership.GetAllUsers();
            return View(пользователи);
        }
        public ActionResult Разрешить(Guid Ид)
        {
            MembershipUser пользователь = Membership.GetUser(Ид, false);
            пользователь.IsApproved = true;
            Membership.UpdateUser(пользователь);
            return RedirectToAction(Действия.Все);
        }
        public ActionResult Запретить(Guid Ид)
        {
            MembershipUser пользователь = Membership.GetUser(Ид, false);
            пользователь.IsApproved = false;
            Membership.UpdateUser(пользователь);
            return RedirectToAction(Действия.Все);
        }
    }
}
