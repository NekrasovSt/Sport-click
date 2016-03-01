using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Данные.Интерфейсы;

namespace Данные.Модели
{
    public class СтатусЗаказаМетаданные
    {
        [Key]
        //[HiddenInputAttribute(DisplayValue=false)]
        public int Ид { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название категории")]
        [Required(ErrorMessage = "Название статуса - обязательно поле")]
        public string Имя { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Описание статуса")]
        public string Описание { get; set; }

    }
    [MetadataType(typeof(СтатусЗаказаМетаданные))]
    public partial class СтатусЗаказа : ИБазовый
    {

    }
}
