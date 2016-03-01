using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.БазовыеКонтролеры;

namespace СпортКлик.Controllers
{
    public class ПроизводителиController : ПроизводителиБазовыйКонтролер
    {
        public ПроизводителиController(ИПоискПоИмени<Производители> сущность)
            : base(сущность)
        {
        }
    }
}
