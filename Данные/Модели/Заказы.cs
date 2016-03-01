using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Данные.Интерфейсы;

namespace Данные.Модели
{
    class ЗаказыМетаданные
    {
        public System.DateTime ДатаСоздания { get; set; }
        public string ИмяКлиента { get; set; }
        public string ФамилияКлиента { get; set; }
        public string ОтчествоКлиента { get; set; }
        public string Телефон { get; set; }
        public string Почта { get; set; }
        public System.Guid ИдКлиента { get; set; }
        public System.Guid ИдСотрудника { get; set; }
        public System.DateTime ДатаИзменения { get; set; }
        public int ИдСтатуса { get; set; }
    }
    [MetadataType(typeof(ЗаказыМетаданные))]
    public partial class Заказ : ИЗаказы
    {      

    }
}
