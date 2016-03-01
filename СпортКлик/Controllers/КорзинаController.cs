using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Models;
using СпортКлик.Сервис;

namespace СпортКлик.Controllers
{
    public class КорзинаController : Controller
    {
        private readonly ИТоварыРепозиторий _сервис;
        public КорзинаController(ИТоварыРепозиторий сервис)
        {
            _сервис = сервис;
        }
        //Будет отображать маленькую корзинку, и количество товаров в ней.
        public PartialViewResult Все(Корзина корзина)
        {
            int count = корзина.ВсегоТоваров;
            return PartialView(ЧастичныеПредставления.Корзина, count);
        }
        // Добавление товара в корзину
        public ActionResult Добавить(Корзина корзина, int Ид)
        {
            Товар товар = _сервис.Найти(Ид);
            if (товар != null)
            {
                корзина.Добавить(товар);
                TempData.Add(Alerts.INFORMATION, "Товар добавлен в корзину.");
            }
            else
            {
                TempData.Add(Alerts.ERROR, "Товар не найден");
            }
            if (Request.UrlReferrer == null)
                return RedirectToAction(Действия.Все);
            return RedirectPermanent(Request.UrlReferrer.LocalPath);
        }
        // Изменение количества товара
        [HttpPost]
        public ActionResult ИзменитьКоличество(Корзина корзина, int Ид, string количество)
        {
            int result = 0;
            if (int.TryParse(количество, out result))
            {
                корзина.ИзменитьКоличество(Ид, result);
            }
            else
                TempData.Add(Alerts.ERROR, "Не верное значение!");
            return RedirectToAction(Действия.ВКорзине, Контролеры.Корзина);
        }
        // Удаление товара из корзины
        public ActionResult Удалить(Корзина корзина, int Ид)
        {
            корзина.Удалить(Ид);
            return RedirectToAction(Действия.ВКорзине, Контролеры.Корзина);
        }
        /// <summary>
        /// Будет отображать содержимое крзины.
        /// </summary>
        /// <param name="корзина"></param>
        /// <returns></returns>
        public ActionResult ВКорзине(Корзина корзина)
        {
            return View(корзина);
        }
    }
}
