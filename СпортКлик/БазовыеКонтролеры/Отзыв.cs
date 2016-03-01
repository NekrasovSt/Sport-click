using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Данные.Абстракции;
using Данные.Модели;
using СпортКлик.Models;
using СпортКлик.Сервис;
using System.Web.Routing;
using Данные.Интерфейсы;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    public class ОтзывБазовыйКонтроллер : БазовыйКонтролерИд<Отзыв, ИОтзывыРепозиторий>
    {
        //protected ИОтзывыРепозиторий _сущность;
        public ОтзывБазовыйКонтроллер(ИОтзывыРепозиторий сущность)
        {
            _сущность = сущность;
        }
        [Authorize]
        public ActionResult Добавить()
        {
            Пользователь пользователь = Пользватель;
            //Если отзыв есть то не будем отображать формк ввода.
            if (_сущность.ЕстьОтзыв(Ид, пользователь.Ид))
                return PartialView(ЧастичныеПредставления.Отзыв, null);
            Отзыв отзыв = new Отзыв();
            отзыв.Сущность = ИмяКонтр;
            отзыв.СущностьИд = Ид;
            return PartialView(ЧастичныеПредставления.Отзыв, отзыв);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Добавить(Отзыв отзыв)
        {
            if (ModelState.IsValid)
            {
                отзыв.Дата = DateTime.Now;
                отзыв.Проверен = false;

                Пользователь пользователь = Пользватель;
                отзыв.Пользователь = пользователь.Членство.UserName;
                отзыв.ПользовательИд = пользователь.Ид;
                //_сущность.Создать(отзыв);
                Success("Спасибо за отзыв!");
                return PartialView(ЧастичныеПредставления.Отзыв, null);
            }
            else
            {
                Error("Отзыв не может быть пустым!");
                return PartialView(ЧастичныеПредставления.Отзыв, null);
            }
        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Отзывы"; }
        }

        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Отзыв"; }
        }
    }
}