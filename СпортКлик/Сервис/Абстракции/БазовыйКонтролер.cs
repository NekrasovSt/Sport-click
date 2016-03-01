using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Сервис;
using Microsoft.Web.Mvc;
using СпортКлик.Models;
using СпортКлик.Фильтры;
using СпортКлик.Areas.Admin.Models;
using System.Web.Routing;

namespace СпортКлик.Сервис.Абстракции
{
    #region Базовый
    /// <summary>
    /// Абстрактный клас для CRUD логики
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class БазовыйКонтролерИд<T, S> : Controller
        where T : class, ИИд, new()
        where S : ИБазовыйРепозиторий<T>
    {
        protected abstract string ИмяСущностиВоМножественномЧисле { get; }
        protected abstract string ИмяСущностиВЕдинственномЧисле { get; }

        protected S _сущность;
        public virtual ActionResult Все(int page = 1)
        {

            ViewBag.Title = ИмяСущностиВоМножественномЧисле;
            IQueryable<T> res = _сущность.Все();
            if (res == null || res.Count() == 0)
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВоМножественномЧисле));
            PageableData<T> data = new PageableData<T>(res, page, Константы.КоличествоЭлементовНаСтранице);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Все", data);
            }
            return View(data);
        }

        [Authorize(Roles = РолиПользователей.Администратор)]
        public ActionResult Изменить(int Ид)
        {
            ViewBag.Title = ИмяСущностиВЕдинственномЧисле;
            T обк = _сущность.Найти(Ид);
            return View("Создать", обк);
        }
        [Authorize(Roles = РолиПользователей.Администратор)]
        [HttpPost]
        public ActionResult Изменить(T об)
        {
            if (ModelState.IsValid)
            {
                _сущность.Изменить(об);
                Success("Обьект успешно обнавлен!");
                return RedirectToAction("Все");
            }
            return View("Создать", об);
        }
        [Authorize(Roles = РолиПользователей.Администратор)]
        public virtual ActionResult Удалить(int Ид)
        {
            _сущность.Удалить(Ид);

            Information("Обьек успешно удален!");
            return RedirectToAction("Все");
        }
        public virtual ActionResult Детально(int Ид)
        {
            ViewBag.Title = ИмяСущностиВЕдинственномЧисле;
            T обк = _сущность.Найти(Ид);
            return View(обк);

        }
        protected int Ид
        {
            get
            {
                string ид = Маршрут(ЗначенияМаршрута.Ид);
                return string.IsNullOrEmpty(ид) ? -1 : int.Parse(ид);
            }
        }
        protected string ИмяКонтр
        {
            get { return Маршрут(ЗначенияМаршрута.Контролер); }
        }
        protected string Действие
        {
            get { return Маршрут(ЗначенияМаршрута.Действие); }
        }
        protected string Имя
        {
            get { return Маршрут(ЗначенияМаршрута.Имя); }
        }
        private string Маршрут(string имя)
        {
            RouteData данные = Request.RequestContext.RouteData;
            return данные.Values[имя] as string;
        }
        protected Пользователь Пользватель
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return new Пользователь(User.Identity.Name);
                }
                return null;
            }
        }

        #region Оповещалки
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
        #endregion

    }
    public abstract class БазовыйКонтролер<T, S> : БазовыйКонтролерИд<T, S>
        where T : class, ИБазовый, new()
        where S : ИБазовыйРепозиторий<T>
    {

        /// <summary>
        /// Для сайд бара
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Список()
        {
            IEnumerable<string> кат = _сущность.Все().Select(к => к.Имя).Distinct();
            ViewBag.ControllerName = this.GetType().Name.Replace("Controller", "");
            return PartialView(ЧастичныеПредставления.Список, кат);
        }
        /// <summary>
        /// Будет рендерить DropDownList, если ид не null то выделить.
        /// </summary>
        public virtual PartialViewResult DropDown(int? Ид)
        {
            T[] все = _сущность.Все().ToArray();
            DropDownListModel listDropDown = new DropDownListModel();
            listDropDown.KeyName = ИмяСущностиВЕдинственномЧисле + "Ид";
            if (Ид == null)
                listDropDown.List = все.Select(i => new SelectListItem() { Text = i.Имя, Value = i.Ид.ToString() });
            else
                listDropDown.List = все.Select(i => new SelectListItem() { Text = i.Имя, Value = i.Ид.ToString(), Selected = i.Ид == Ид });
            return PartialView(ЧастичныеПредставления.DropDown, listDropDown);
        }
    }
    #endregion
    #region БазовыйСКартинками
    /// <summary>
    /// Дополнительно работа с картинками
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class БазовыйСКартинкамиКонтролер<T, S> : БазовыйКонтролер<T, S>
        where T : class, ИЗображение, new()
        where S : ИБазовыйРепозиторий<T>
    {
        [HttpPost]
        public ActionResult ЗагрузитьКартинку(HttpPostedFileBase fileUpload, int Ид)
        {
            if (fileUpload != null)
            {
                //Папка где хранятся картинки
                string папка = AppDomain.CurrentDomain.BaseDirectory + Константы.ПапкаДляХраненияИзображений;
                string расширение = Path.GetExtension(fileUpload.FileName);
                //Формат такой имяМоделиИд.расширение например Категория3.jpg
                string имяФайла = typeof(T).Name + Ид.ToString() + расширение;
                //Файл без сжатия.
                string имяФайлыОригинал = "original_" + имяФайла;
                string путь = Path.Combine(папка, имяФайла);
                if (System.IO.File.Exists(путь))
                    System.IO.File.Delete(путь);
                string путьОригинал = Path.Combine(папка, имяФайлыОригинал);
                if (System.IO.File.Exists(путьОригинал))
                    System.IO.File.Delete(путьОригинал);
                fileUpload.ResizeAndSave(120, 160, путь);
                fileUpload.ResizeAndSave(270, 370, путьОригинал);
                //А теперь изменим запись у модели
                T об = _сущность.Найти(Ид);
                об.ИмяФайлаИзображения = имяФайла;
                _сущность.Изменить(об);
            }
            return RedirectToAction(Действия.Изменить, new { Ид = Ид });
        }
    }
    #endregion
    #region Базовый с поиском и картинками
    /// <summary>
    /// Дополнительно работа с картинками и поиском по имени
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class БазовыйСПоиском<T, S> : БазовыйСКартинкамиКонтролер<T, S>
        where T : class, ИЗображение, new()
        where S : ИПоискПоИмени<T>
    {
        public ActionResult ИскатьПоИмени(string частьИмени, int page = 1)
        {
            IQueryable<T> res = _сущность.Все(частьИмени);
            if (res == null || res.Count() == 0)
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВоМножественномЧисле));
            PageableData<T> data = new PageableData<T>(res, page, Константы.КоличествоЭлементовНаСтранице);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Все", data);
            }
            return View("Все", data);
        }
        /// <summary>
        /// Будем искать по имени а не по Ид
        /// </summary>
        public ActionResult ДетальноПоИмени(string Имя)
        {
            ViewBag.Title = ИмяСущностиВЕдинственномЧисле;
            T обк = _сущность.Найти(Имя);
            if (обк == null)
            {
                Attention(string.Format("К сожалению нет ни одного обьекта {0}", ИмяСущностиВЕдинственномЧисле));
                return View("Детально", new T());
            }
            return View("Детально", обк);
        }
        [Authorize(Roles = РолиПользователей.Администратор)]
        public virtual ActionResult Создать()
        {
            ViewBag.Title = ИмяСущностиВЕдинственномЧисле;
            return View(new T());
        }
        [HttpPost]
        [Authorize(Roles = РолиПользователей.Администратор)]
        public virtual ActionResult Создать(T об)
        {
            if (ModelState.IsValid)
            {
                T temp = _сущность.Найти(об.Имя);
                if (temp != null)
                {
                    Error("Имя должно быть уникально, найдена запись с именем " + temp.Имя);
                    return View(об);
                }
                _сущность.Создать(об);
                Success("Обьект успешно сохранен!");
                return RedirectToAction("Все");
            }
            else
            {
                Error("Некоторые поля заполнены не верно.");
                return View(об);
            }
        }

    }
    #endregion
}