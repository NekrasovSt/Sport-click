using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    /// <summary>
    /// Абстаркный клас зависимых от Товара сущностей: категории, производители.
    /// </summary>
    /// <typeparam name="Т"></typeparam>
    public abstract class БазовыйДляЭлементовТовара<Т> : БазовыйСПоиском<Т, ИПоискПоИмени<Т>> where Т : class, ИИзображениеТовар, new()
    {
        [Authorize(Roles = РолиПользователей.Администратор)]
        public override ActionResult Удалить(int Ид)
        {
            if (_сущность.Найти(Ид).Товар.Count != 0)
            {
                Error("Нельзя удалять есть товары!");
                return RedirectToAction(Действия.Все, new { Ид = Ид });
            }
            return base.Удалить(Ид);
        }
        public override ActionResult Все(int page = 1)
        {

            ViewBag.Title = ИмяСущностиВоМножественномЧисле;
            IQueryable<Т> res = _сущность.Все();
            if (res == null || res.Count() == 0)
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВоМножественномЧисле));
            PageableData<ИИзображениеТовар> data = new PageableData<ИИзображениеТовар>(res, page, Константы.КоличествоЭлементовНаСтранице);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Все", data);
            }
            return View(data);
        }
    }
}