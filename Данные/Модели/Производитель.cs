using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Данные.Интерфейсы;

namespace Данные.Модели
{
    public class ПроизводительМетаданные
    {
        [Key]
        //[HiddenInputAttribute(DisplayValue=false)]
        public int Ид { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название производителя")]
        [Required(ErrorMessage = "Название производителя - обязательно поле")]
        public string Имя { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Описание производителя")]
        public string Описание { get; set; }

        [Display(Name = "Изображение")]
        [HiddenInput(DisplayValue = false)]
        public string ИмяФайлаИзображения { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual ICollection<Товар> Товар { get; set; }
    }
    [MetadataType(typeof(ПроизводительМетаданные))]
    public partial class Производители : ИИзображениеТовар
    {

    }
}
