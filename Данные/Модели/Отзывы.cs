using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Данные.Интерфейсы;


namespace Данные.Модели
{

    public class ОтзывМетаданные
    {
        [Key]
        public int Ид { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст отзыва")]
        [Required(ErrorMessage = "Текст отзыва - обязательно поле")]
        public string Текст { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата-время добавления отзыва")]
        public System.DateTime Дата { get; set; }

        public bool Проверен { get; set; }
    }
    [MetadataType(typeof(ОтзывМетаданные))]
    public partial class Отзыв : ИИд
    {
    }
}
