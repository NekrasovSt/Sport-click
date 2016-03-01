using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Controllers;
using СпортКлик.Models;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    public class ЗаказБазовыйController : БазовыйКонтролерИд<Заказ, ИЗаказыРепозиторий>
    {
        
        private ИТоварыРепозиторий _товары;
        public ЗаказБазовыйController(ИЗаказыРепозиторий заказ, ИТоварыРепозиторий товар)
        {
            _сущность = заказ;
            _товары = товар;
        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Заказы"; }
        }
        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Заказ"; }
        }
        [Authorize]
        public ActionResult ОформитьЗаказ(Корзина корзина)
        {
            ПрофильПользователя profile = new ПрофильПользователя(User.Identity.Name);
            MembershipUser user = Membership.GetUser(User.Identity.Name);
            if (string.IsNullOrEmpty(profile.Имя) || string.IsNullOrEmpty(profile.Отчество) || string.IsNullOrEmpty(profile.Фамилия))
                Attention("Не указанны ФИО.");
            if (string.IsNullOrEmpty(profile.Телефон) || string.IsNullOrEmpty(user.Email))
            {
                Error("Не указанны данные для связи с вами.");
                return RedirectToAction(Действия.Изменить, Контролеры.Профили);
            }
            ПодтверждениеЗаказа заказ = new ПодтверждениеЗаказа()
            {
                Имя = profile.Имя,
                Фамилия = profile.Фамилия,
                Отчество = profile.Отчество,
                Почта = user.Email,
                Телефон = profile.Телефон,
                ИдКлиента = new Guid(user.ProviderUserKey.ToString())
            };
            //Корзина пуста выход
            if (корзина.ВсегоТоваров == 0)
            {
                Information("Корзина пуста");
                return RedirectToAction(Действия.Все, Контролеры.Товар);
            }
            заказ.Товары = корзина.Все;
            Session[Сессия.Заказ] = заказ;
            return View(заказ);
        }
        [Authorize]
        public ActionResult Подтвердить()
        {
            ПодтверждениеЗаказа заказ = null;
            try
            {
                заказ = Session[Сессия.Заказ] as ПодтверждениеЗаказа;
            }
            catch
            {
                Error("Произошла ошибка попробуйте снова сформировать заказ.");
                return RedirectToAction(Действия.ВКорзине, Контролеры.Товар);
            }
            Заказ новыйЗаказ = new Заказ()
            {
                ДатаИзменения = DateTime.Now,
                ДатаСоздания = DateTime.Now,
                ИдКлиента = заказ.ИдКлиента,
                ИдСотрудника = заказ.ИдКлиента,
                ИдСтатуса = 1,/*Новый*/
                ИмяКлиента = заказ.Имя,
                ОтчествоКлиента = заказ.Отчество,
                ФамилияКлиента = заказ.Фамилия,
                Телефон = заказ.Телефон,
                Почта = заказ.Почта
            };
            List<СвойствоЗаказа> свойство = new List<СвойствоЗаказа>();
            foreach (var item in заказ.Товары)
            {
                СвойствоЗаказа св = new СвойствоЗаказа()
                {
                    ИдТовара = item.товар.Ид,
                    ИмяПроизводителя = item.товар.Производители.Имя,
                    ИмяТовара = item.товар.Имя,
                    Количество = item.количество,
                    Скидка = item.товар.Скидка,
                    Цена = item.товар.Цена
                };
                свойство.Add(св);
            }
            _сущность.СформироватьЗаказ(новыйЗаказ, свойство);
            Information("Спасибо заказ успешно обработан!");
            Session.Remove(Сессия.Заказ);
            Session.Remove(Сессия.Корзина);
            return RedirectToAction(Действия.Все, Контролеры.Производители);
        }

        
    }
}
