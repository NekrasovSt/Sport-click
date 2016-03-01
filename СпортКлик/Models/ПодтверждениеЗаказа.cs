using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace СпортКлик.Models
{
    public class ПодтверждениеЗаказа
    {
        public IList<СтрокаВКорзине> Товары { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public string Отчество { get; set; }
        public string Почта { get; set; }
        public string Телефон { get; set; }
        public Guid ИдКлиента { get; set; }
    }
}