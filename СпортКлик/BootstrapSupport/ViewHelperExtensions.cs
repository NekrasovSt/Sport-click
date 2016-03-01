using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace BootstrapSupport
{
    public static class DefaultScaffoldingExtensions
    {
        /// <summary>
        /// Получаем имя контролера без приставки "Controller"
        /// </summary>
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", String.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static PropertyInfo[] VisibleProperties(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return elementType.GetProperties().Where(info => info.Name != elementType.IdentifierPropertyName()).ToArray();
        }
        /// <summary>
        /// Получает массив видемых свойтв(не Ид)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static PropertyInfo[] ВидимыеСвойства(this Object model)
        {
            //var атр = model.GetType().GetElementType().ПолучитьАтрибут;
            return model.GetType().GetProperties().Where(info => info.Name != model.IdentifierPropertyName() && info.Name != "ИмяФайлаИзображения"  && !info.AttributeExists<HiddenInputAttribute>()).ToArray();
        }
        public static PropertyInfo Изображение(this Object model)
        {
            return model.GetType().GetProperties().Where(i => i.Name == "ИмяФайлаИзображения").FirstOrDefault();
        }
        public static RouteValueDictionary GetIdValue(this object model)
        {
            var v = new RouteValueDictionary();
            v.Add(model.IdentifierPropertyName(), model.GetId());
            return v;
        }

        public static object GetId(this object model)
        {
            return model.GetType().GetProperty(model.IdentifierPropertyName()).GetValue(model,new object[0]);
        }


        public static string IdentifierPropertyName(this Object model)
        {
            return IdentifierPropertyName(model.GetType());
        }

        public static string IdentifierPropertyName(this Type type)
        {
            if (type.GetProperties().Any(info => info.PropertyType.АтрибутСуществует<System.ComponentModel.DataAnnotations.KeyAttribute>()))
            {
                return
                    type.GetProperties().First(
                        info => info.PropertyType.АтрибутСуществует<System.ComponentModel.DataAnnotations.KeyAttribute>())
                        .Name;
            }
            else if (type.GetProperties().Any(p => p.Name.Equals("ид", StringComparison.CurrentCultureIgnoreCase)))
            {
                return
                    type.GetProperties().First(
                        p => p.Name.Equals("ид", StringComparison.CurrentCultureIgnoreCase)).Name;
            }
            return "";
        }

        public static string GetLabel(this PropertyInfo propertyInfo)
        {
            var meta = ModelMetadataProviders.Current.GetMetadataForProperty(null, propertyInfo.DeclaringType, propertyInfo.Name);
            return meta.GetDisplayName();
        }

        public static string ToSeparatedWords(this string value)
        {
            return Regex.Replace(value, "([A-Z][a-z])", " $1").Trim();
        }

    }

    public static class PropertyInfoExtensions
    {
        public static bool AttributeExists<T>(this PropertyInfo propertyInfo) where T : class
        {
            var attribute = propertyInfo.GetCustomAttributes(typeof(T), false)
                                .FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверяет задан ли атрибут
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool АтрибутСуществует<T>(this Type type) where T : class
        {
            var attribute = type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
            if (attribute == null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Получает атрибут, если нет то ексепшин
        /// </summary>
        /// <typeparam name="А">Атрибут</typeparam>
        /// <param name="поле">Поле в котором надо искать</param>
        /// <param name="клас">Клас в котором поле, нужно так как метаданные через него</param>
        private static А ПолучитьАтрибутИзМетаданных<А>(PropertyInfo поле, Type клас)
        {
            //Получаем метаданные у класса.
            MetadataTypeAttribute[] metadataTypes = клас.GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().ToArray();
            //Метаданных нет.
            if (metadataTypes.Length == 0)
                throw new Exception();
            MetadataTypeAttribute metadata = metadataTypes.FirstOrDefault();
            //Теперь найдем свойство в метиаданных.
            PropertyInfo свойство = metadata.MetadataClassType.GetProperty(поле.Name);
            //Получаем атрибут.
            return (А)свойство.GetCustomAttributes(typeof(А), true)[0];
        }
        /// <summary>
        /// Получить значение атрибута
        /// </summary>
        /// <typeparam name="T">Класс атрибут</typeparam>
        /// <param name="type">Тип поля класса</param>
        /// <returns>Ссылка на атрибут</returns>
        public static T ПолучитьАтрибут<T>(this Type type) where T : class
        {
            return type.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }
        /// <summary>
        /// Получить значение атрибута из свойста класса
        /// </summary>
        /// <typeparam name="T">Класс атрибут</typeparam>
        /// <param name="type">Свойство класса</param>
        /// <returns>Ссылка на атрибут</returns>
        public static T ПолучитьАтрибут<T>(this PropertyInfo propertyInfo) where T : class
        {
            return propertyInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault() as T;
        }
		
        public static string LabelFromType(Type @type)
        {
            var att = ПолучитьАтрибут<DisplayNameAttribute>(@type);
            return att != null ? att.DisplayName 
                : @type.Name.ToSeparatedWords();
        }
		
        public static string GetLabel(this Object Model)
        {
            return LabelFromType(Model.GetType());
        }

        public static string GetLabel(this IEnumerable Model)
        {
            var elementType = Model.GetType().GetElementType();
            if (elementType == null)
            {
                elementType = Model.GetType().GetGenericArguments()[0];
            }
            return LabelFromType(elementType);
        }
    }

    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TryPartial(this HtmlHelper helper, string viewName, object model)
        {
            try
            {
                return helper.Partial(viewName, model);
            }
            catch (Exception)
            {
            }
            return MvcHtmlString.Empty;
        }
    }
}