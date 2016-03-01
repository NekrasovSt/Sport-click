using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Models;
using СпортКлик.Сервис.Абстракции;
using СпортКлик.Сервис;
using СпортКлик.БазовыеКонтролеры;

namespace СпортКлик.Controllers
{
    public class ЗаказController : ЗаказБазовыйController
    {
        public ЗаказController(ИЗаказыРепозиторий заказ, ИТоварыРепозиторий товар) : base(заказ, товар) { }
    }
}
