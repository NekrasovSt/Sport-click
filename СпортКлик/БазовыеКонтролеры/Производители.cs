using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    public class ПроизводителиБазовыйКонтролер : БазовыйДляЭлементовТовара<Производители>
    {
        public ПроизводителиБазовыйКонтролер(ИПоискПоИмени<Производители> сущность)
        {
            _сущность = сущность;
        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Производители"; }
        }
        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Производитель"; }
        }
    }
}