using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Данные.Модели;
using System.ComponentModel.DataAnnotations;

namespace СпортКлик.Models
{
    public class СтрокаВКорзине
    {
        /// <summary>
        /// Товар в корзине 
        /// </summary>
        public Товар товар { get; set; }
        /// <summary>
        /// Количество товаров в корзине
        /// </summary>
        [Range(0, 100, ErrorMessage = "Значение не может быть меньше 0")]
        public int количество { get; set; }
        /// <summary>
        /// Общая сумма
        /// </summary>
        public decimal ИтогоБезСкидки
        {
            get
            {
                return товар.Цена * количество;
            }
        }
        /// <summary>
        /// Общая сумма за вычетом скидки
        /// </summary>
        public decimal ИтогоСоСкидкой
        {
            get
            {
                int скидка = товар.Скидка == null ? 0 : (int)товар.Скидка;
                return (товар.Цена - товар.Цена * скидка / 100) * количество;
            }
        }

    }
    public class Корзина
    {
        private List<СтрокаВКорзине> записи;
        public Корзина()
        {
            записи = new List<СтрокаВКорзине>();
        }
        public List<СтрокаВКорзине> Все
        {
            get { return записи; }
        }
        public int ВсегоЗаписей
        {
            get { return записи.Count(); }
        }
        public int ВсегоТоваров
        {
            get { return записи.Sum(i => i.количество); }
        }
        public void Добавить(Товар товар)
        {
            СтрокаВКорзине result = записи.Where(i => i.товар.Ид == товар.Ид).FirstOrDefault();
            if (result == null)
                записи.Add(new СтрокаВКорзине() { товар = товар, количество = 1 });
            else
                result.количество += 1;
        }
        public void ИзменитьКоличество(int Ид, int количество)
        {
            СтрокаВКорзине result = записи.Where(i => i.товар.Ид == Ид).FirstOrDefault();
            if (result != null)
                result.количество = количество;
        }
        public void Удалить(int Ид)
        {
            СтрокаВКорзине result = записи.Where(i => i.товар.Ид == Ид).FirstOrDefault();
            if (result != null)
                записи.Remove(result);
        }
    }
}