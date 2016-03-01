using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Сервис;
using СпортКлик.Сервис.Абстракции;

namespace СпортКлик.Areas.Admin.Controllers
{
    [Authorize(Roles = РолиПользователей.Администратор)]
    public class ИсторияЗаказаController : БазовыйКонтролерИд<ИсторияЗаказа, ИБазовыйРепозиторий<ИсторияЗаказа>>
    {
        public ИсторияЗаказаController(ИБазовыйРепозиторий<ИсторияЗаказа> история)
        {
            _сущность = история;
        }

        
        //public ActionResult История(int ЗаказИд)
        //{
        //    //_сущность.История(ЗаказИд);
        //}


        protected override string ИмяСущностиВоМножественномЧисле
        {
            get { return "Истории заказа"; }
        }

        protected override string ИмяСущностиВЕдинственномЧисле
        {
            get { return "Истрия заказа"; }
        }
    }
}
