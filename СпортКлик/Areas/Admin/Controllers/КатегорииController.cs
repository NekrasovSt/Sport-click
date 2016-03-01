using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Areas.Admin.Models;
using СпортКлик.Models;
using СпортКлик.БазовыеКонтролеры;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.Areas.Admin.Controllers
{
    [Authorize(Roles=РолиПользователей.Администратор)]
    public class КатегорииController : КатегорииБазовыйКонтроллер
    {
        public КатегорииController(ИПоискПоИмени<Категории> сущность)
            : base(сущность)
        {
        }
    }
}
