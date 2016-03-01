using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using СпортКлик.Models;
using СпортКлик.Сервис;

namespace СпортКлик.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Ajax()
        {
            return PartialView(new LogOnViewModel());
        }
        [HttpPost]
        public ActionResult Ajax(LogOnViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.UserName, model.Password))
                {
                    //Перезагружает страницу вызвав JavaScript.
                    return View(ЧастичныеПредставления.OK);
                }
                else
                {
                    // Неверное имя пользователя или пароль
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                    return PartialView();
                }
            }
            return PartialView(new LogOnViewModel());
        }
        public ActionResult Login()
        {
            return View();
        }
        private bool Authenticate(string username, string password)
        {
            bool result = Membership.ValidateUser(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
        [HttpPost]
        public ActionResult Login(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action(Действия.Все, Контролеры.Категории));
                }
                else
                {
                    // Неверное имя пользователя или пароль
                    ModelState.AddModelError("", "Неверное имя пользователя или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(Действия.Все, Контролеры.Категории);
        }
        public ViewResult Зарегистрироватся()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Зарегистрироватся(RegisterModel пользователь)
        {
            if (ModelState.IsValid)
            {
                // Попытка зарегистрировать пользователя
                try
                {
                    Membership.CreateUser(пользователь.UserName, пользователь.Password, пользователь.Email);
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    return View();
                }
                FormsAuthentication.SetAuthCookie(пользователь.UserName, false);
                TempData.Add(Alerts.SUCCESS, "Вы успешно зарегистрированны!");
                return Redirect(Url.Action(Действия.Все, Контролеры.Категории));
            }
            return View();
        }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Полный список кодов состояния см. по адресу http://go.microsoft.com/fwlink/?LinkID=177550
            //.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Указан недопустимый пароль. Введите допустимое значение пароля.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Указан недопустимый адрес электронной почты. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Указан недопустимый ответ на вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Указан недопустимый вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Указано недопустимое имя пользователя. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос создания пользователя был отменен. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                default:
                    return "Произошла неизвестная ошибка. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";
            }
        }
        [Authorize]
        public ActionResult СменитьПароль()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult СменитьПароль(ChangePassword model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                MembershipUser u = Membership.GetUser(User.Identity.Name);
                if (u.ChangePassword(model.Password, model.NewPassword))
                {
                    TempData.Add(Alerts.SUCCESS, "Пароль успешно изменен!");
                    return Redirect(returnUrl ?? Url.Action(Действия.Все, Контролеры.Категории));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный текущий пароль или недопустимый новый пароль.");
                }
                return View();
            }
            else
                return View(model);
        }
    }
}
