using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Decision.Common.MvcToolkit.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ClientIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        }
    }
}