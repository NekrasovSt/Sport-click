using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Данные.Интерфейсы;

namespace Данные.Модели
{
    class ИсторияЗаказаМетаданные
    {
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Коментарий к изменению статуса")]
        public string Коментарий { get; set; }
    }
    [MetadataType(typeof(ИсторияЗаказаМетаданные))]
    public partial class ИсторияЗаказа : ИИд
    {
       
    }
}
