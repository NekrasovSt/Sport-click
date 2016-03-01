using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using СпортКлик.Models;
using СпортКлик.Сервис;

namespace СпортКлик.Binders
{
    /// <summary>
    /// Будет осуществлять превязку модели корзины к сессии
    /// </summary>
    public class КорзинаПривязкаМодели : IModelBinder
    {
        object IModelBinder.BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
           Корзина корзина =  controllerContext.HttpContext.Session[Сессия.Корзина] as Корзина;
           if (корзина == null)
               корзина = new Корзина();
           controllerContext.HttpContext.Session[Сессия.Корзина] = корзина;
           return корзина;
        }
    }
}