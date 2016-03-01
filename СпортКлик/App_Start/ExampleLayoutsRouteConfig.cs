using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NavigationRoutes;
using СпортКлик.Controllers;
using СпортКлик.Areas.Admin.Controllers;
using СпортКлик.Сервис;

namespace BootstrapMvcSample
{
    public class ExampleLayoutsRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Для админа
            routes.MapNavigationRoute<СпортКлик.Areas.Admin.Controllers.КатегорииController>("Все категории", c => c.Все(1), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СпортКлик.Areas.Admin.Controllers.ТоварController>("Все товары", c => c.Все(1), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СпортКлик.Areas.Admin.Controllers.ПроизводителиController>("Все производители", c => c.Все(1), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<ПользователиController>("Все пользователи", c => c.Все(), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СтатусЗаказаController>("Все статусы", c => c.Все(1), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СпортКлик.Areas.Admin.Controllers.ЗаказController>("Просмотреть заказы", c => c.Просмотреть(), ОбластиПроекта.Админка, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СпортКлик.Areas.Admin.Controllers.ОтзывController>("Отзывы", c => c.Все(1), ОбластиПроекта.Админка, РолиПользователей.Администратор)
                .AddChildRoute<СпортКлик.Areas.Admin.Controllers.ОтзывController>("Не проверенные", c => c.ВсеНеПроверенные(1), ОбластиПроекта.Админка, РолиПользователей.Администратор)
                .AddChildRoute<СпортКлик.Areas.Admin.Controllers.ОтзывController>("Проверенные", c => c.ВсеПроверенные(1), ОбластиПроекта.Админка, РолиПользователей.Администратор);

            //Для всех 
            routes.MapNavigationRoute<СпортКлик.Controllers.ДомController>("Домой", c => c.Онас(), ОбластиПроекта.Общая, РолиПользователей.Все);
            routes.MapNavigationRoute<СпортКлик.Controllers.ДомController>("Домой", c => c.Онас(), ОбластиПроекта.Общая, РолиПользователей.Администратор);
            routes.MapNavigationRoute<СпортКлик.Controllers.ПроизводителиController>("Производители", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Все);
            routes.MapNavigationRoute<СпортКлик.Controllers.КатегорииController>("Категории", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Все);
            routes.MapNavigationRoute<СпортКлик.Controllers.ТоварController>("Все товары", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Все);
            //routes.MapNavigationRoute<СпортКлик.Controllers.AccountController>("Вход", c => c.Login(), ОбластиПроекта.Общая, РолиПользователей.Все, "icon-home icon-black");
            
            //Для вошедших 
            routes.MapNavigationRoute<СпортКлик.Controllers.ПроизводителиController>("Все производители", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Залогированный);
            routes.MapNavigationRoute<СпортКлик.Controllers.КатегорииController>("Категории", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Залогированный);
            routes.MapNavigationRoute<СпортКлик.Controllers.ДомController>("Домой", c => c.Онас(), ОбластиПроекта.Общая, РолиПользователей.Залогированный);
            routes.MapNavigationRoute<СпортКлик.Controllers.ТоварController>("Все товары", c => c.Все(1), ОбластиПроекта.Общая, РолиПользователей.Залогированный);
            routes.MapNavigationRoute<СпортКлик.Controllers.AccountController>("Выход", c => c.LogOff(), ОбластиПроекта.Общая, РолиПользователей.Залогированный)
                .AddChildRoute<СпортКлик.Controllers.ПрофилиController>("Профиль", c => c.Изменить(), ОбластиПроекта.Общая, РолиПользователей.Залогированный)
                .AddChildRoute<СпортКлик.Controllers.AccountController>("Сменить пароль", c => c.СменитьПароль(), ОбластиПроекта.Общая, РолиПользователей.Залогированный);
        }
    }
}
