using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.БазовыеКонтролеры;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.Controllers
{
    public class КатегорииController : КатегорииБазовыйКонтроллер
    {
        public КатегорииController(ИПоискПоИмени<Категории> сущность)
            : base(сущность)
        {
        }
    }
}
