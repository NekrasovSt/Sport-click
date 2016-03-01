using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Данные.Интерфейсы;
using Данные.Модели;
using СпортКлик.Binders;
using СпортКлик.Models;

namespace СпортКлик.Сервис
{
    public class ControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public ControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            ДобавитьСвязи();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        // Здесь размещаются дополнительные привязки
        private void ДобавитьСвязи()
        {
            Данные.Модели.СпортКлик кон = new Данные.Модели.СпортКлик();
            ninjectKernel.Bind<ИПоискПоИмени<Категории>>().To<РепозиторийКатегорий>().WithConstructorArgument("контекст", кон);
            ninjectKernel.Bind<ИПоискПоИмени<Производители>>().To<РепозиторийПроизводителей>().WithConstructorArgument("контекст", кон);
            ninjectKernel.Bind<ИТоварыРепозиторий>().To<РепозиторийТоваров>().WithConstructorArgument("контекст", кон);
            ninjectKernel.Bind<ИБазовыйРепозиторий<СтатусЗаказа>>().To<РепозиторийСтатусовЗаказа>().WithConstructorArgument("контекст", кон);
            ninjectKernel.Bind<ИЗаказыРепозиторий>().To<РепозиторийЗаказов>().WithConstructorArgument("контекст", кон);
            ninjectKernel.Bind<ИОтзывыРепозиторий>().To<РепозиторийОтзывов>().WithConstructorArgument("контекст", кон);
        }
    }
}