using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Данные.Абстракции;
using Данные.Интерфейсы;
using Данные.Модели;
using System.Transactions;
using System.Data;
namespace Данные.Модели
{
    public class РепозиторийКатегорий : БазовыйАбстрактныйСИменем<Категории>
    {
        public РепозиторийКатегорий(СпортКлик контекст)
            : base(контекст)
        {
        }
    }
    public class РепозиторийТоваров : БазовыйАбстрактныйСИменем<Товар>, ИТоварыРепозиторий
    {
        public РепозиторийТоваров(СпортКлик контекст)
            : base(контекст)
        {
        }
        public IQueryable<Товар> ВсеДляКатегории(int КатегорияИд)
        {
            return _сущность.Where(i => i.КатегорияИд == КатегорияИд).OrderBy(i => i.Имя);
        }
        public IQueryable<Товар> ВсеДляПроизводителя(int ПроизводительИд)
        {
            return _сущность.Where(i => i.ПроизводительИд == ПроизводительИд).OrderBy(i => i.Имя);
        }

    }
    public class РепозиторийПроизводителей : БазовыйАбстрактныйСИменем<Производители>
    {
        public РепозиторийПроизводителей(СпортКлик контекст)
            : base(контекст)
        {
        }
    }
    public class РепозиторийСтатусовЗаказа : БазовыйАбстрактный<СтатусЗаказа>
    {
        public РепозиторийСтатусовЗаказа(СпортКлик контекст)
            : base(контекст)
        {
        }
    }
    public class РепозиторийЗаказов : БазовыйАбстрактный<Заказ>, ИЗаказыРепозиторий
    {
        public РепозиторийЗаказов(СпортКлик контекст)
            : base(контекст)
        {

        }
        public IQueryable<Заказ> ПолучитьВсеЗаказыВСтатусе(int СтатусИд)
        {
            return _сущность.Where(i => i.ИдСтатуса == СтатусИд).OrderBy(i => i.ДатаИзменения);
        }
        public void СформироватьЗаказ(Заказ заказ, IList<СвойствоЗаказа> записи)
        {
            _сущность.Add(заказ);
            foreach (var item in записи)
            {
                _кон.СвойствоЗаказа.Add(item);
            }
            using (TransactionScope tran = new TransactionScope())
            {
                _кон.SaveChanges();
                tran.Complete();
            }
        }


        public void СменитьСтатусЗаказа(ИсторияЗаказа история)
        {
            Заказ заказ = Найти(история.ИдЗаказ);
            заказ.ДатаИзменения = история.ДатаИзменения;
            заказ.ИдСотрудника = история.ИдСотрудника;
            заказ.ИдСтатуса = история.СтатусИд;
            заказ.ИсторияЗаказа.Add(история);
            using (TransactionScope tran = new TransactionScope())
            {
                _кон.SaveChanges();
                tran.Complete();
            }
        }
    }
    public class РепозиторийИсторииЗаказов : БазовыйАбстрактный<ИсторияЗаказа>
    {
        public РепозиторийИсторииЗаказов(СпортКлик контекст)
            : base(контекст)
        {
        }
        public IQueryable<ИсторияЗаказа> История(int заказИд)
        {
            return _сущность.Where(i => i.ИдЗаказ == заказИд);
        }
    }
    public class РепозиторийОтзывов : БазовыйАбстрактный<Отзыв>, ИОтзывыРепозиторий
    {
        public РепозиторийОтзывов(СпортКлик контекст)
            : base(контекст)
        {
        }
        public bool ЕстьОтзыв(int Ид, Guid ИдПользователя)
        {
            return _сущность.Where(i => i.СущностьИд == Ид && i.ПользовательИд == ИдПользователя).FirstOrDefault() != null;
        }
        public IQueryable<Отзыв> ВсеРазрешенные()
        {
            return _сущность.Where(i => i.Проверен == true).OrderBy(i => i.Дата);
        }
        public IQueryable<Отзыв> ВсеНеРазрешенные()
        {
            return _сущность.Where(i => i.Проверен != true).OrderBy(i => i.Дата);
        }
    }
}
