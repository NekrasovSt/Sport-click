using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Данные.Интерфейсы;
using Данные.Модели;

namespace Данные.Абстракции
{
    public class БазовыйАбстрактныйСИменем<Т> : БазовыйАбстрактный<Т>, ИПоискПоИмени<Т> where Т : class, ИБазовый, new()
    {
        public БазовыйАбстрактныйСИменем(СпортКлик контекст)
            : base(контекст)
        {
        }

        public IQueryable<Т> Все(string частьИмени)
        {
            return _сущность.Where(i => i.Имя.Contains(частьИмени)).OrderBy(i => i.Имя);
        }
        public Т Найти(string Имя)
        {
            return _сущность.Where(i => i.Имя.Equals(Имя, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}
