using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Данные.Интерфейсы;

namespace Данные.Модели
{
    public partial class ТоварМетаданные
    {
        [DataType(DataType.Text)]
        [Display(Name = "Название категории")]
        [Required(ErrorMessage = "Название товара - обязательно поле")]
        public string Имя { get; set; }

        [Required(ErrorMessage = "Цена - обязательно поле")]
        [Range(0, 1000000)]
        public decimal Цена { get; set; }

        [Range(0,100,ErrorMessage="Скидка должна быть от 0 до 100 %" )]
        public Nullable<int> Скидка { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание товара")]
        public string Описание { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int КатегорияИд { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Nullable<int> ПроизводительИд { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual Категории Категории { get; set; }
    }
    [MetadataType(typeof(ТоварМетаданные))]
    public partial class Товар : ИЗображение
    {

    }
}
