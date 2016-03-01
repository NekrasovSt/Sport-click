using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Данные.Интерфейсы;
using System.Web.WebPages;
using System.Linq.Expressions;
using System.Globalization;

namespace СпортКлик.Сервис
{
    public static class Помошники
    {
        public static MvcHtmlString ColorBoxImage(this HtmlHelper хтмл, ИЗображение изоб, string класс, string классМини)
        {
            //http://www.jacklmoore.com/colorbox/
            StringBuilder res = new StringBuilder();
            UrlHelper helper = new UrlHelper(хтмл.ViewContext.RequestContext);
            string src = helper.Content("~/" + Константы.ПапкаДляХраненияИзображений);
            string org = src + "/original_" + изоб.ИмяФайлаИзображения;
            string img = src + "/" + изоб.ИмяФайлаИзображения;
            string none = src + "/НетИзображения.png";
            if (!string.IsNullOrEmpty(изоб.ИмяФайлаИзображения))
            {
                res.AppendFormat("<a class='{4}' href='{1}' title='{2}'><img src='{3}' class='{0}'/></a>", классМини, org, изоб.Имя, img, класс);
            }
            else
            {
                res.AppendFormat("<img src='{0}' class='{1}' alt='{2}'/>", none, классМини, изоб.Имя);
            }
            return new MvcHtmlString(res.ToString());
        }

        private static StringBuilder МодальноеИзображение(HtmlHelper хтмл, ИЗображение изоб, bool модальное = true)
        {
            string модИд = "#m_" + изоб.Ид;
            string мод = "m_" + изоб.Ид;
            StringBuilder res = new StringBuilder();
            res.Append("<div style='height: 130px;'>");
            res.AppendFormat("<a href='{0}' data-toggle='modal'>", модИд);
            UrlHelper helper = new UrlHelper(хтмл.ViewContext.RequestContext);
            string src = helper.Content("~/" + Константы.ПапкаДляХраненияИзображений);
            if (!string.IsNullOrEmpty(изоб.ИмяФайлаИзображения))
            {
                res.AppendFormat("<img src='{0}/{1}' alt='{2}'  class='img-polaroid'>", src, изоб.ИмяФайлаИзображения, изоб.Имя);
            }
            else
            {
                модальное = false;
                res.AppendFormat("<img src='{0}/{1}' alt='{2}'  class='img-polaroid'>", src, "НетИзображения.png", изоб.Имя);
            }
            res.Append("</a>");
            res.Append("</div>");
            if (модальное)
            {
                res.AppendFormat("<div class='modal hide fade' id='{0}' >", мод);
                res.Append("<div class='modal-header'>");
                res.Append("<button class='close' data-dismiss='modal'>×</button>");
                res.AppendFormat("<h3>{0}</h3>", изоб.Имя);
                res.Append("</div>");
                res.Append("<div class='modal-body'>");
                res.AppendFormat("<img src='{0}/original_{1}' alt='{2}'>", src, изоб.ИмяФайлаИзображения, изоб.Имя);
                res.Append("</div>");
                res.Append("</div>");
            }
            return res;
        }
        public static MvcHtmlString ИзображениеДляГлавнойСтраницы(this HtmlHelper хтмл, ИЗображение изоб, bool модальное = true)
        {
            return new MvcHtmlString(МодальноеИзображение(хтмл, изоб, модальное).ToString());
        }
        static MvcHtmlString ImageFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            string alternateText,
            object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            string name = ExpressionHelper.GetExpressionText(expression);

            TagBuilder tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", metadata.Model.ToString());
            tagBuilder.MergeAttribute("alt", alternateText);
            tagBuilder.MergeAttribute("name", name);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// Наименование поля, если нет атрибута DisplayAttribute то название в классе
        /// </summary>
        /// <param name="поле">Поле для проверки</param>
        /// <returns>Наименование</returns>
        private static string Наименование(PropertyInfo поле)
        {
            //Если атрибут не установлен используем имя.
            Object[] ИмяАтр = поле.GetCustomAttributes(typeof(DisplayAttribute), false);
            string имя = поле.Name;
            if (ИмяАтр != null && ИмяАтр.Length != 0)
                имя = ((DisplayAttribute)ИмяАтр[0]).Name;
            return имя;
        }
        public static MvcHtmlString PageLinks(this HtmlHelper html, int currentPage, int totalPages, Func<int, string> pageUrl)
        {
            StringBuilder builder = new StringBuilder();

            //Prev
            var prevBuilder = new TagBuilder("a");
            prevBuilder.InnerHtml = "«";
            //AJAX
            //data-ajax="true" data-ajax-mode="replace" data-ajax-update="#target"
            prevBuilder.MergeAttribute("data-ajax", "true");
            prevBuilder.MergeAttribute("data-ajax-mode", "replace");
            prevBuilder.MergeAttribute("data-ajax-update", "#target");
            if (currentPage == 1)
            {
                prevBuilder.MergeAttribute("href", "#");
                builder.AppendLine("<li class=\"active\">" + prevBuilder.ToString() + "</li>");
            }
            else
            {
                prevBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage - 1));
                builder.AppendLine("<li>" + prevBuilder.ToString() + "</li>");
            }
            //По порядку
            for (int i = 1; i <= totalPages; i++)
            {
                //Условие что выводим только необходимые номера
                if (((i <= 3) || (i > (totalPages - 3))) || ((i > (currentPage - 2)) && (i < (currentPage + 2))))
                {
                    var subBuilder = new TagBuilder("a");
                    //AJAX
                    //data-ajax="true" data-ajax-mode="replace" data-ajax-update="#target"
                    subBuilder.MergeAttribute("data-ajax", "true");
                    subBuilder.MergeAttribute("data-ajax-mode", "replace");
                    subBuilder.MergeAttribute("data-ajax-update", "#target");
                    subBuilder.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                    if (i == currentPage)
                    {
                        subBuilder.MergeAttribute("href", "#");
                        builder.AppendLine("<li class=\"active\">" + subBuilder.ToString() + "</li>");
                    }
                    else
                    {
                        subBuilder.MergeAttribute("href", pageUrl.Invoke(i));
                        builder.AppendLine("<li>" + subBuilder.ToString() + "</li>");
                    }
                }
                else if ((i == 4) && (currentPage > 5))
                {
                    //Троеточие первое
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
                else if ((i == (totalPages - 3)) && (currentPage < (totalPages - 4)))
                {
                    //Троеточие второе
                    builder.AppendLine("<li class=\"disabled\"> <a href=\"#\">...</a> </li>");
                }
            }
            //Next
            var nextBuilder = new TagBuilder("a");
            //AJAX
            //data-ajax="true" data-ajax-mode="replace" data-ajax-update="#target"
            nextBuilder.MergeAttribute("data-ajax", "true");
            nextBuilder.MergeAttribute("data-ajax-mode", "replace");
            nextBuilder.MergeAttribute("data-ajax-update", "#target");
            nextBuilder.InnerHtml = "»";
            if (currentPage == totalPages)
            {
                nextBuilder.MergeAttribute("href", "#");
                builder.AppendLine("<li class=\"active\">" + nextBuilder.ToString() + "</li>");
            }
            else
            {
                nextBuilder.MergeAttribute("href", pageUrl.Invoke(currentPage + 1));
                builder.AppendLine("<li>" + nextBuilder.ToString() + "</li>");
            }
            return new MvcHtmlString("<ul>" + builder.ToString() + "</ul>");
        }
        public static MvcHtmlString Ссылка(this HtmlHelper html, params  string[] значения)
        {
            UrlHelper helper = new UrlHelper(html.ViewContext.RequestContext);
            string result = "~";
            foreach (string значение in значения)
            {
                result += "/" + значение;
            }
            return new MvcHtmlString(helper.Content(result));
        }
    }
}