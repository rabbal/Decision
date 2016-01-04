using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Decision.Common.Security.HiddenField;

namespace Decision.Common.Helpers
{
    public static class MvcHtmlHelperExtentions
    {
        #region Input FormControl
        public static MvcHtmlString NoAutoCompleteTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off" });
        }
        public static MvcHtmlString NoAutoCompleteTextBoxForLtr<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr" });
        }
        public static MvcHtmlString NoAutoCompleteTextBoxForNumber<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr",data_val_number="لطفا مقدار عددی وارد کنید" });
        }
        public static MvcHtmlString FormControlTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.TextBoxFor(expression, new { @class = "form-control" });
        }
        public static MvcHtmlString FormControlPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.PasswordFor(expression, new { @class = "form-control" });
        }
        #endregion


    }
}
