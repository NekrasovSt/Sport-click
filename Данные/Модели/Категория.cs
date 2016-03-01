using System;
using System.Linq;
using Данные.Интерфейсы;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
namespace Данные.Модели
{
    public class КатегорияМетаданные
    {
        [Key]
        //[HiddenInputAttribute(DisplayValue=false)]
        public int Ид { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название категории")]
        [Required(ErrorMessage = "Название категории - обязательно поле")]
        public string Имя { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        [Display(Name = "Описание категории")]
        public string Описание { get; set; }

        [Display(Name = "Изображение")]
        [HiddenInput(DisplayValue = false)]
        public string ИмяФайлаИзображения { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual ICollection<Товар> Товар { get; set; }
    }
    [MetadataType(typeof(КатегорияМетаданные))]
    public partial class Категории : ИИзображениеТовар
    {

    }
}
