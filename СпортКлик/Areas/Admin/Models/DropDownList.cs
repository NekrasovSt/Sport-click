using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace СпортКлик.Areas.Admin.Models
{
    public class DropDownListModel
    {
        /// <summary>
        /// Элементы списка
        /// </summary>
        public IEnumerable<SelectListItem> List { get; set; }
        /// <summary>
        /// Имя ключевого элемента
        /// </summary>
        public string KeyName { get; set; }
    }
}