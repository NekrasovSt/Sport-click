using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Данные.Интерфейсы;

namespace СпортКлик.Models
{
    public class ЭлементыТовара
    {
        /// <summary>
        /// Основные свойства, имя описание ...
        /// </summary>
        public ИИзображениеТовар Элемент { get; set; }
        /// <summary>
        /// Тип элемента: категории, производители и т.д.
        /// </summary>
        public string Тип { get; set; }
    }
}