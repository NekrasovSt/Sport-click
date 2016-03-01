using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace СпортКлик.Models
{
    public class ДляПредставленияЗагрузкиКартинки
    {
        public string Контроллер { get; set; }
        public string Действие  { get; set; }
        public string АльтернативныйТекст { get; set; }
        public string ИмяФайлаИзображения { get; set; }
        public int    Ид { get; set; }
    }
}