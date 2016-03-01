using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    public class ТоварБазовыйКонтролер : БазовыйСПоиском<Товар, ИТоварыРепозиторий>
    {
        private readonly ИПоискПоИмени<Категории> _категории;
        private readonly ИПоискПоИмени<Производители> _производители;
        public ТоварБазовыйКонтролер(ИТоварыРепозиторий сущность, ИПоискПоИмени<Категории> категории, ИПоискПоИмени<Производители> производители)
        {
            _сущность = сущность;
            _категории = категории;
            _производители = производители;
        }
        public ActionResult ВсеДляКатегории(string Имя, int page = 1)
        {
            var res = _категории.Найти(Имя).Товар.AsQueryable();
            PageableData<Товар> data = new PageableData<Товар>(res, page, Константы.КоличествоЭлементовНаСтранице);
            ViewBag.Действие = "ВсеДляКатегории";
            if (Request.IsAjaxRequest())
            {
                return PartialView(Действия.Все, data);
            }
            return View(Действия.Все, data);
        }
        public ActionResult ВсеДляПроизводителя(string Имя, int page = 1)
        {
            Uri url = Request.RequestContext.HttpContext.Request.UrlReferrer;
            var res = _производители.Найти(Имя).Товар.AsQueryable();
            PageableData<Товар> data = new PageableData<Товар>(res, page, Константы.КоличествоЭлементовНаСтранице);
            ViewBag.Действие = "ВсеДляПроизводителя";
            if (Request.IsAjaxRequest())
            {
                return PartialView(Действия.Все, data);
            }
            return View(Действия.Все, data);
        }

        /// <summary>
        /// Общий для производителей и категорий, обработчик
        /// </summary>
        public ActionResult ВсеДля(string Имя, string Тип, int page = 1)
        {
            PageableData<Товар> data = null;
            ViewBag.Имя = Имя;

            if (Тип.Equals(Контролеры.Производители, StringComparison.InvariantCultureIgnoreCase))
            {
                var res = _производители.Найти(Имя);
                if (res == null)
                {
                    Attention(string.Format("К сожалению нет ни одного обьекта {0} c названием {1}", ИмяСущностиВоМножественномЧисле, Имя));
                    return RedirectToAction(Действия.Все);
                }
                data = new PageableData<Товар>(res.Товар.AsQueryable(), page, Константы.КоличествоЭлементовНаСтранице);
                ViewBag.Действие = "производители";
            }
            //Либо из категория либо из корня.
            if (Тип.Equals(Контролеры.Категории, StringComparison.InvariantCultureIgnoreCase))
            {
                var res = _категории.Найти(Имя);
                if (res == null)
                {
                    Attention(string.Format("К сожалению нет ни одного обьекта {0} c названием {1}", ИмяСущностиВоМножественномЧисле, Имя));
                    return RedirectToAction(Действия.Все);
                }
                data = new PageableData<Товар>(res.Товар.AsQueryable(), page, Константы.КоличествоЭлементовНаСтранице);
                ViewBag.Действие = "категории";
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("Все", data);
            }
            return View(Действия.Все, data);

        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Товары"; }
        }

        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Товар"; }
        }
        public ActionResult ИскатьАвтозаполнение(string частьИмени)
        {
            string[] products = _сущность.Все(частьИмени).Select(i => i.Имя).ToArray();
            return new JsonResult() { Data = products, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}