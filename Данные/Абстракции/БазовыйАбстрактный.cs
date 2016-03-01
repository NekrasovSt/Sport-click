using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Данные.Интерфейсы;
using Данные.Модели;

namespace Данные.Абстракции
{
    public class БазовыйАбстрактный<Т> : ИБазовыйРепозиторий<Т> where Т : class, ИИд, new()
    {
        // Объект Entity Framework
        protected DbSet<Т> _сущность;
        protected СпортКлик _кон;
        //http://stackoverflow.com/questions/14520328/entity-framework-update-entity-an-object-with-the-same-key-already-exists-in
        public БазовыйАбстрактный(СпортКлик контекст)
        {
            _кон = контекст;
            _сущность = _кон.Set<Т>();
        }
        public IQueryable<Т> Все()
        {
            return _сущность.OrderBy(i => i.Ид);
        }

        public Т Найти(int Ид)
        {
            return _сущность.Where(i => i.Ид == Ид).FirstOrDefault();
        }
        public void Создать(Т об)
        {
            _сущность.Add(об);
            _кон.SaveChanges();
        }        
        public void Удалить(int Ид)
        {
            Т с = Найти(Ид);
            _сущность.Attach(с);
            _кон.Entry<Т>(с).State = EntityState.Deleted;
            _кон.SaveChanges();
        }

        public virtual void Изменить(Т об)
        {
            Т с = _сущность.Find(об.Ид);
            _кон.Entry<Т>(с).CurrentValues.SetValues(об);
            _кон.SaveChanges();
        }
    }
}
