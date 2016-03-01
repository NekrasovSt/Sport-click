using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.БазовыеКонтролеры;
using СпортКлик.Сервис;

namespace СпортКлик.Areas.Admin.Controllers
{
    public class ЗаказController : ЗаказБазовыйController
    {
        public ЗаказController(ИЗаказыРепозиторий заказ, ИТоварыРепозиторий товар) : base(заказ, товар) { }
        [Authorize(Roles = РолиПользователей.Администратор)]
        public ActionResult Просмотреть()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = РолиПользователей.Администратор)]
        public ActionResult Просмотреть(int СтатусИд, int page=1)
        {
            var all = _сущность.ПолучитьВсеЗаказыВСтатусе(СтатусИд);
            if (all == null || all.Count() == 0)
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВоМножественномЧисле));
            PageableData<Заказ> data = new PageableData<Заказ>(all, page, Константы.КоличествоЭлементовНаСтранице);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Все", data);
            }
            return View("Все", data);
        }
        [Authorize(Roles = РолиПользователей.Администратор)]
        public ActionResult ИзменитьСтатус(int ИдЗаказ)
        {
            ИсторияЗаказа ист = new ИсторияЗаказа();
            ист.ИдЗаказ = ИдЗаказ;
            return View(ист);

        }
        [HttpPost]
        [Authorize(Roles = РолиПользователей.Администратор)]
        public ActionResult ИзменитьСтатус(ИсторияЗаказа ист)
        {
            ист.ДатаИзменения = DateTime.Now;
            ист.ИдСотрудника = Пользватель.Ид;
            _сущность.СменитьСтатусЗаказа(ист);
            Success("Статус заказа изменен");
            return RedirectToAction(Действия.Все);
        }
    }
}
