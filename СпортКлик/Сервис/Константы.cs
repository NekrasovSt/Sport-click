using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace СпортКлик.Сервис
{
    /// <summary>
    /// Сдесь будут константы
    /// </summary>
    public static class Константы
    {
        const int ОграничениеНаДлиннуОписания = 15;
        const string Ограничитель = "...";
        /// <summary>
        /// Выдает строку с учетом ограничения длинны, лишние символы заменяет ограничителем(напр "...");
        /// </summary>
        public static string Ограничить(string значение)
        {
            if (string.IsNullOrEmpty(значение))
                return String.Empty;
            int длОгр = Ограничитель.Length;
            if (значение.Length > ОграничениеНаДлиннуОписания)
            {
                значение = значение.Remove(ОграничениеНаДлиннуОписания - длОгр);
                значение += Ограничитель;
            }
            return значение;
        }
        public const string ПапкаДляХраненияИзображений = @"Content\Download";
        public const int КоличествоКартинокНаСтранице = 20;
        public const int КоличествоКартинокВСтроке = 4;
        public const int КоличествоЭлементовНаСтранице = 16;
        public const string ИмяМаршрута = "RouteName";
        public const string Контролер = "controller";
        public const string Имя = "Имя";
    }
    public static class РолиПользователей
    {
        public const string Администратор = "Admin";
        public const string Все = "all";
        public const string Залогированный = "logOn";
    }
    public static class ЗначенияМаршрута
    {
        public const string Ид = "Ид";
        public const string Имя = "Имя";
        public const string Контролер = "controller";
        public const string Действие = "action";
    }
    public static class ОбластиПроекта
    {
        public const string Админка = "admin";
        public const string Общая = null;
    }
    public static class ЧастичныеПредставления
    {
        public const string ЗагрузитьКартинку = "Частичные/ЗагрузитьКартинку";
        public const string СайдБар = "Частичные/СайдБар";
        public const string Список = "Частичные/Список";
        public const string Вход = "Частичные/Вход";
        public const string ПоискТовара = "Частичные/ПоискТовара";
        public const string Корзина  = "Частичные/Корзина";
        public const string DropDown = "Частичные/DropDown";
        public const string OK = "Частичные/_OK";
        public const string AjaxLogin = "Частичные/AjaxLogin";
        public const string ХлебныеКрошки = "Частичные/ХлебныеКрошки";
        public const string Все = "Частичные/_Все";
        public const string Отзыв = "Частичные/Отзыв";
        public const string Отзывы = "Частичные/Отзывы";
        public const string Миниатюра = "Миниатюра";
    }
    public static class Действия
    {
        public const string ДетальноПоИмени = "ДетальноПоИмени";
        public const string Все = "Все";
        public const string ВсеДля = "ВсеДля";
        public const string Создать = "Создать";
        public const string Изменить = "Изменить";
        public const string Удалить = "Удалить";
        public const string Детально = "Детально";
        public const string Список = "Список";
        public const string ЗагрузитьКартинку = "ЗагрузитьКартинку";
        public const string ИскатьПоИмени = "ИскатьПоИмени";
        public const string Login = "Login";
        public const string AjaxLogin = "Ajax";
        public const string Онас = "Онас";
        public const string LogOff = "LogOff";
        public const string Зарегистрироватся = "Зарегистрироватся";
        public const string Разрешить = "Разрешить";
        public const string Запретить = "Запретить";
        public const string СменитьПароль = "СменитьПароль";
        public const string Добавить = "Добавить";
        public const string ВКорзине = "ВКорзине";
        public const string ИзменитьКоличество = "ИзменитьКоличество";
        public const string ОформитьЗаказ = "ОформитьЗаказ";
        public const string Подтвердить = "Подтвердить";
        public const string DropDown = "DropDown";
        public const string Просмотреть = "Просмотреть";
        public const string ИзменитьСтатус = "ИзменитьСтатус";
    }
    public static class Контролеры
    {
        public const string Account = "Account";
        public const string Категории = "Категории";
        public const string Производители = "Производители";
        public const string Товар = "Товар";
        public const string ДомАдмин = "ДомАдмин";
        public const string Дом = "Дом";
        public const string Профили = "Профили";
        public const string Пользователи = "Пользователи";
        public const string Корзина = "Корзина";
        public const string Заказ = "Заказ";
        public const string СтатусЗаказа = "СтатусЗаказа";
        public const string Отзыв = "Отзыв";
    }
    public static class Сессия
    {
        public const string Корзина = "Корзина";
        public const string Заказ = "Заказ";
    }
}