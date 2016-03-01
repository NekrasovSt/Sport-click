using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace СпортКлик.Areas.Admin
{
    public static  class AdminHelpers
    {
        public static MvcHtmlString AdminTextBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expr)
        {
            StringBuilder sb = new StringBuilder();

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expr, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expr);
           
            sb.Append("<div class=\"control-group\">");
            sb.AppendFormat("<label class=\"control-label\">{0}</label>", metadata.DisplayName);
            //sb.Append(htmlHelper.LabelFor(expr, new { @class = "control-label" }).ToString());
            sb.Append("<div class='controls'>");
            sb.Append(htmlHelper.TextBoxFor(expr).ToString());
            sb.Append(htmlHelper.ValidationMessageFor(expr, null, new { @class = "help-inline" }).ToString());
            sb.Append("</div></div>");
            return new MvcHtmlString(sb.ToString());
        }
        public static MvcHtmlString AdminPasswordBox<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expr)
        {
            StringBuilder sb = new StringBuilder();

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expr, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expr);

            sb.Append("<div class=\"control-group\">");
            sb.AppendFormat("<label class=\"control-label\">{0}</label>", metadata.DisplayName);
            //sb.Append(htmlHelper.LabelFor(expr, new { @class = "control-label" }).ToString());
            sb.Append("<div class='controls'>");
            sb.Append(htmlHelper.PasswordFor(expr).ToString());
            sb.Append(htmlHelper.ValidationMessageFor(expr, null, new { @class = "help-inline" }).ToString());
            sb.Append("</div></div>");
            return new MvcHtmlString(sb.ToString());
        }
        public static MvcHtmlString AdminTextArea<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expr)
        {
            StringBuilder sb = new StringBuilder();

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expr, htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expr);

            sb.Append("<div class=\"control-group\">");
            sb.AppendFormat("<label class=\"control-label\">{0}</label>", metadata.DisplayName);
            sb.Append("<div class='controls'>");
            sb.Append(htmlHelper.TextAreaFor(expr, new { @class = "input-xlarge" }).ToString());
            sb.Append(htmlHelper.ValidationMessageFor(expr, null, new { @class = "help-inline" }).ToString());
            sb.Append("</div></div>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}
//<div class="control-group">
//                <label class="control-label">Название</label>
//                <div class="controls">
//                    @Html.TextBox("Имя", Model.Имя, new { @class = "input-xlarge" })
//                    @Html.ValidationMessage("Имя", null, new { @class = "help-inline" })
//                </div>
//            </div>