using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Models;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.БазовыеКонтролеры
{
    public class КатегорииБазовыйКонтроллер : БазовыйДляЭлементовТовара<Категории>
    {
        public КатегорииБазовыйКонтроллер(ИПоискПоИмени<Категории> сущность)
        {
            _сущность = сущность;
        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Категории"; }
        }

        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Категроия"; }
        }
        
    }
}