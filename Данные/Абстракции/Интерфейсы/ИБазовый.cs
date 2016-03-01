using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Данные.Модели;

namespace Данные.Интерфейсы
{

    public interface ИИд
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        int Ид { set; get; }
    }
    /// <summary>
    /// Описывает основные элементы сущности
    /// </summary>
    public interface ИБазовый : ИИд
    {       
        /// <summary>
        /// Имя сущности
        /// </summary>
        string Имя { set; get; }
        /// <summary>
        /// Описание сущности
        /// </summary>
        string Описание { set; get; }

    }
    public interface ИЗаказы : ИИд
    {
        /// <summary>
        /// Статус заказа
        /// </summary>
        int ИдСтатуса { set; get; }
    }

    /// <summary>
    /// Поддержка сушностей с изображением
    /// </summary>
    public interface ИЗображение : ИБазовый
    {
        /// <summary>
        /// Имя файла изображение вида Ид.расширение
        /// </summary>
        string ИмяФайлаИзображения { get; set; }
    }
    /// <summary>
    /// Будет использоватся для категорий и товаров
    /// </summary>
    public interface ИИзображениеТовар : ИЗображение
    {
        ICollection<Товар> Товар { get; set; }
    }
    /// <summary>
    /// Описывает базовый репозиторий обьектов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ИБазовыйРепозиторий<T>
    {
        /// <summary>
        /// Получить все обьекты
        /// </summary>
        IQueryable<T> Все();
        /// <summary>
        /// Найти обьект по его ид
        /// </summary>
        /// <param name="Ид">Идентификатор</param>
        /// <returns>Найденый обьект</returns>
        T Найти(int Ид);
        /// <summary>
        /// Добавить обьект
        /// </summary>
        /// <param name="об">Обьект</param>
        void Создать(T об);
        /// <summary>
        /// Удалить обьект
        /// </summary>
        void Удалить(int Ид);
        /// <summary>
        /// Изменить обьект
        /// </summary>
        /// <param name="об">Обьект</param>
        void Изменить(T об);
    }
    public interface ИПоискПоИмени<T> : ИБазовыйРепозиторий<T>
    {
        /// <summary>
        /// Получить все обьекты, в которы содержится фраза
        /// </summary>
        IQueryable<T> Все(string частьИмени);
        /// <summary>
        /// Получить обьект с именем
        /// </summary>
        T Найти(string Имя);
    }

    /// <summary>
    /// Интерфейс для товаров
    /// </summary>
    public interface ИТоварыРепозиторий : ИПоискПоИмени<Товар>
    {
        IQueryable<Товар> ВсеДляКатегории(int КатегорияИд);
        IQueryable<Товар> ВсеДляПроизводителя(int ПроизводительИд);
    }
    public interface ИЗаказыРепозиторий : ИБазовыйРепозиторий<Заказ>
    {
        /// <summary>
        /// Получить все заказы для определенного статуса
        /// </summary>
        IQueryable<Заказ> ПолучитьВсеЗаказыВСтатусе(int ИдСтатуса);
        /// <summary>
        /// Записывает заказ в базу, причем с использованием транзакции
        /// </summary>
        void СформироватьЗаказ(Заказ заказ, IList<СвойствоЗаказа> записи);
        void СменитьСтатусЗаказа(ИсторияЗаказа история);
    }
    public interface ИОтзывыРепозиторий : ИБазовыйРепозиторий<Отзыв>
    {
        bool ЕстьОтзыв(int Ид, Guid ИдПользователя);
        IQueryable<Отзыв> ВсеРазрешенные();
        IQueryable<Отзыв> ВсеНеРазрешенные();
    }
    
}
