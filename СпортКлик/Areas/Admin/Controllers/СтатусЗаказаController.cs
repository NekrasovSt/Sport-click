using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Areas.Admin.Models;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.Areas.Admin.Controllers
{
    public class СтатусЗаказаController : БазовыйКонтролер<СтатусЗаказа, ИБазовыйРепозиторий<СтатусЗаказа>>
    {
        public СтатусЗаказаController(ИБазовыйРепозиторий<СтатусЗаказа> сущность)
        {
            _сущность = сущность;
        }
        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Статусы"; }
        }
        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Статус"; }
        }
    }
}
