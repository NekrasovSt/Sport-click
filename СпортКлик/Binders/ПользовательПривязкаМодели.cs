using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using СпортКлик.Models;

namespace СпортКлик.Binders
{
    public class ПользовательПривязкаМодели : IModelBinder
    {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Пользователь пользователь = new Пользователь(controllerContext.HttpContext.User.Identity.Name);
            return пользователь;
        }
    }
}