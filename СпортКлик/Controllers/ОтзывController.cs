using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Абстракции;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.БазовыеКонтролеры;
using СпортКлик.Сервис;

namespace СпортКлик.Controllers
{
    public class ОтзывController : ОтзывБазовыйКонтроллер
    {
        public ОтзывController(ИОтзывыРепозиторий сущность) : base(сущность) { }
        public PartialViewResult ВсеПроверенные(int page = 1)
        {
            ViewBag.Title = ИмяСущностиВоМножественномЧисле;
            IQueryable<Отзыв> res = _сущность.ВсеРазрешенные();
            if (res == null || res.Count() == 0)
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВоМножественномЧисле));
            PageableData<Отзыв> data = new PageableData<Отзыв>(res, page, Константы.КоличествоЭлементовНаСтранице);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Все", data);
            }
            return PartialView("Все", data);
        }
    }
}
