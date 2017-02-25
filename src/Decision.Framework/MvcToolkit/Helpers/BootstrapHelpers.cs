using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Decision.Framework.MvcToolkit.Helpers
{
    public static class BootstrapHelpers
    {
        #region Labels (2)
        public static IHtmlString BootstrapLabelFor<TModel, TProp>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TProp>> property)
        {
           
            return helper.LabelFor(property, new
            {
                @class = "col-md-2 control-label"
            });
        }
        public static IHtmlString BootstrapLabel(
            this HtmlHelper helper,
            string propertyName)
        {
            return helper.Label(propertyName, new
            {
                @class = "col-md-2 control-label"
            });
        }
        #endregion

        #region Inputs (4)

        //public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off" });
        //}

        //public static MvcHtmlString BootstrapLtrTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr" });
        //}

        //public static MvcHtmlString BootstrapTextBoxForNumber<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    return html.TextBoxFor(expression, new { @class = "form-control", autocomplete = "off", dir = "ltr", data_val_number = " مقدار عددی وارد کنید" });
        //}

        //public static MvcHtmlString FormControlTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    return html.TextBoxFor(expression, new { @class = "form-control" });
        //}

        //public static MvcHtmlString FormControlPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        //{
        //    return html.PasswordFor(expression, new { @class = "form-control" });
        //}

        #endregion
            
        #region Html5 Input Types Enum
        public enum Html5InputTypes
        {
            text,
            color,
            date,
            datetime,
            email,
            month,
            number,
            password,
            range,
            search,
            tel,
            time,
            url,
            week
        }
        #endregion

        #region Bootstrap Submit Button Helpers
        /// <summary>
        /// Bootstrap Submit Button Helper
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="buttonText">The text for the button</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input type='submit' with the appropriate properties set.</returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper,
          string buttonText,
          object htmlAttributes = null)
        {
            return SubmitButton(htmlHelper, buttonText, null, false, null, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap Submit Button Helper
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="buttonText">The text for the button</param>
        /// <param name="id">The id for the button</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input type='submit' with the appropriate properties set.</returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper,
          string buttonText, string id,
          object htmlAttributes = null)
        {
            return SubmitButton(htmlHelper, buttonText, id, false, null, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap Submit Button Helper
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="buttonText">The text for the button</param>
        /// <param name="id">The id for the button</param>
        /// <param name="isDisabled">Set to true if you want the button disabled</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input type='submit' with the appropriate properties set.</returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper,
          string buttonText, string id, bool isDisabled,
          object htmlAttributes = null)
        {
            return SubmitButton(htmlHelper, buttonText, id, isDisabled, null, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap Submit Button Helper
        /// </summary>
        /// <param name="htmlHelper">The helper</param>
        /// <param name="buttonText">The text for the button</param>
        /// <param name="id">The id for the button</param>
        /// <param name="isDisabled">Set to true if you want the button disabled</param>
        /// <param name="btnClass">The bootstrap 'btn-' class to use for this button</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input type='submit' with the appropriate properties set.</returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper,
          string buttonText, string id, bool isDisabled, string btnClass,
          object htmlAttributes = null)
        {
            var disable = string.Empty;

            if (string.IsNullOrEmpty(id))
                id = buttonText;
            if (string.IsNullOrEmpty(btnClass))
                btnClass = "btn-primary";

            // Ensure ID is a valid identifier
            id = id.Replace(" ", "").Replace("-", "_");

            var html = "<input type='submit' class='btn {3}{1}' title='{0}' value='{0}' id='{2}' {4} />";
            if (isDisabled)
                disable = " disabled";

            html = string.Format(html, buttonText, disable,
                                id, btnClass,
                                GetHtmlAttributes(htmlAttributes));
            html = html.Replace("'", "\"");

            return new MvcHtmlString(html);
        }
        #endregion

        #region Bootstrap/HTML 5 Check Box Helpers
        /// <summary>
        /// Bootstrap and HTML 5 Check Box.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="text">The text to display next to this check box.</param>
        /// <param name="htmlAttributes">custom attributes</param>
        /// <returns>An HTML checkbox with the appropriate type set.</returns>
        public static MvcHtmlString CheckBoxFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string text,
          object htmlAttributes = null)
        {
            return CheckBoxFor(htmlHelper, expression, id, text, false, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Check Box in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this check box.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="text">The text to display next to this check box.</param>
        /// <param name="isChecked">Whether or not to set the 'checked' attribute on this check box.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this check box.</param>
        /// <param name="useInline">Whether or not to use 'checkbox-inline' for the Bootstrap class.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML checkbox with the appropriate type set.</returns>
        public static MvcHtmlString CheckBoxFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string text,
          bool isChecked,
          bool isAutoFocus,
          bool useInline = false,
          object htmlAttributes = null)
        {
            var sb = new StringBuilder(512);
            var htmlChecked = string.Empty;
            var htmlAutoFocus = string.Empty;

            if (isChecked)
            {
                htmlChecked = "checked='checked'";
            }
            if (isAutoFocus)
            {
                htmlAutoFocus = "autofocus='autofocus'";
            }

            // Build the CheckBox
            if (useInline)
            {
                sb.Append("<label class='checkbox-inline'>");
            }
            else
            {
                sb.Append("<div class='checkbox'>");
                sb.Append("  <label>");
            }
            sb.AppendFormat("    <input id='{0}' name='{0}' type='checkbox' value='true' {1} {2}/><input name='{0}' type='hidden' value='false' {3} />",
                            id, htmlChecked, htmlAutoFocus,
                            GetHtmlAttributes(htmlAttributes));
            sb.Append($"    {text}");
            if (useInline)
            {
                sb.Append("</label>");
            }
            else
            {
                sb.Append("  </label>");
                sb.Append("</div>");
            }

            // Return an MVC HTML String
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        #region Bootstrap/HTML 5 Check Box in a Button Helpers

        /// <summary>
        /// Bootstrap and HTML 5 Check Box in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this check box.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="text">The text to display next to this check box.</param>
        /// <param name="htmlAttributes">custom attributes</param>
        /// <returns>An HTML checkbox with the appropriate type set.</returns>
        public static MvcHtmlString CheckBoxButtonFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string text,
          object htmlAttributes = null)
        {
            return CheckBoxButtonFor(htmlHelper, expression, id, text, null, false, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Check Box in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this check box.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="text">The text to display next to this check box.</param>
        /// <param name="btnClass">The Bootstrap 'btn-' class to add to this check box.</param>
        /// <param name="htmlAttributes">custom attributes</param>
        /// <returns>An HTML checkbox with the appropriate type set.</returns>
        public static MvcHtmlString CheckBoxButtonFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string text,
          string btnClass,
          object htmlAttributes = null)
        {
            return CheckBoxButtonFor(htmlHelper, expression, id, text, btnClass, false, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Check Box in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this check box.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="text">The text to display next to this check box.</param>
        /// <param name="btnClass">The Bootstrap 'btn-' class to add to this check box.</param>
        /// <param name="isChecked">Whether or not to set the 'checked' attribute on this check box.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this check box.</param>
        /// <param name="useInline">Whether or not to use 'checkbox-inline' for the Bootstrap class.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML checkbox with the appropriate type set.</returns>
        public static MvcHtmlString CheckBoxButtonFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string text,
          string btnClass,
          bool isChecked,
          bool isAutoFocus,
          bool useInline = false,
          object htmlAttributes = null)
        {
            var sb = new StringBuilder(512);
            var htmlChecked = string.Empty;
            var htmlAutoFocus = string.Empty;
            var chkClass = "checkbox";

            if (string.IsNullOrEmpty(btnClass))
            {
                btnClass = "btn-default";
            }
            if (isChecked)
            {
                htmlChecked = "checked='checked'";
            }
            if (isAutoFocus)
            {
                htmlChecked = "autofocus='autofocus'";
            }
            if (useInline)
            {
                chkClass = "checkbox-inline";
            }

            // Build the CheckBox
            sb.Append($"<div class='{chkClass}'>");
            sb.Append($"  <label class='btn {btnClass}'>");
            sb.AppendFormat("    <input id='{0}' name='{0}' type='checkbox' value='true' {1} {2}/><input name='{0}' type='hidden' value='false' {3} />",
                            id, htmlChecked, htmlAutoFocus,
                            GetHtmlAttributes(htmlAttributes));
            sb.Append($"    {text}");
            sb.Append("  </label>");
            sb.Append("</div>");

            // Return an MVC HTML String
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        #region Bootstrap/HTML 5 Radio Button
        /// <summary>
        /// Bootstrap and HTML 5 Radio Button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="name">The 'name' attribute to set.</param>
        /// <param name="text">The text to display next to this radio button.</param>
        /// <param name="value">The 'value' attribute to set.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML radio button with the appropriate type set.</returns>
        public static MvcHtmlString RadioButtonFor<TModel, TValue>(
              this HtmlHelper<TModel> htmlHelper,
              Expression<Func<TModel, TValue>> expression,
              string id,
              string name,
              string text,
              string value,
              object htmlAttributes = null)
        {
            return RadioButtonFor(htmlHelper, expression, id, name, text, value, false, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Radio Button in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this radio button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="name">The 'name' attribute to set.</param>
        /// <param name="text">The text to display next to this radio button.</param>
        /// <param name="value">The 'value' attribute to set.</param>
        /// <param name="isChecked">Whether or not to set the 'checked' attribute on this radio button.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this radio button.</param>
        /// <param name="useInline">Whether or not to use 'radio-inline' for the Bootstrap class.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML radio button with the appropriate type set.</returns>
        public static MvcHtmlString RadioButtonFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string name,
          string text,
          string value,
          bool isChecked,
          bool isAutoFocus,
          bool useInline = false,
          object htmlAttributes = null)
        {
            var sb = new StringBuilder(512);
            var htmlChecked = string.Empty;
            var htmlAutoFocus = string.Empty;
            var rdoClass = "radio";

            if (isChecked)
            {
                htmlChecked = "checked='checked'";
            }
            if (isAutoFocus)
            {
                htmlAutoFocus = "autofocus='autofocus'";
            }
            if (useInline)
            {
                rdoClass = "radio-inline";
            }

            // Build the Radio Button
            sb.Append($"<div class='{rdoClass}'>");
            sb.Append("  <label>");
            sb.Append(
                $"    <input id='{id}' name='{name}' type='radio' value='{value}' {htmlChecked} {htmlAutoFocus} {GetHtmlAttributes(htmlAttributes)} />");
            sb.Append($"    {text}");
            sb.Append("  </label>");
            sb.Append("</div>");

            // Return an MVC HTML String
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        #region Bootstrap/HTML 5 Radio Button in a Button Helpers
        /// <summary>
        /// Bootstrap and HTML 5 Radio Button in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this radio button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="name">The 'name' attribute to set.</param>
        /// <param name="text">The text to display next to this radio button.</param>
        /// <param name="value">The 'value' attribute to set.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML radio button with the appropriate type set.</returns>
        public static MvcHtmlString RadioButtonInButtonFor<TModel, TValue>(
              this HtmlHelper<TModel> htmlHelper,
              Expression<Func<TModel, TValue>> expression,
              string id,
              string name,
              string text,
              string value,
              object htmlAttributes = null)
        {
            return RadioButtonInButtonFor(htmlHelper, expression, id, name, text, value, null, false, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Radio Button in a Button Helper.
        /// This helper assumes you have added the appropriate CSS to style this radio button.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="id">The 'id' attribute name to set.</param>
        /// <param name="name">The 'name' attribute to set.</param>
        /// <param name="text">The text to display next to this radio button.</param>
        /// <param name="value">The 'value' attribute to set.</param>
        /// <param name="btnClass">The Bootstrap 'btn-' class to add to this check box.</param>
        /// <param name="isChecked">Whether or not to set the 'checked' attribute on this radio button.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this radio button.</param>
        /// <param name="useInline">Whether or not to use 'radio-inline' for the Bootstrap class.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML radio button with the appropriate type set.</returns>
        public static MvcHtmlString RadioButtonInButtonFor<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string id,
          string name,
          string text,
          string value,
          string btnClass,
          bool isChecked,
          bool isAutoFocus,
          bool useInline = false,
          object htmlAttributes = null)
        {
            var sb = new StringBuilder(512);
            var htmlChecked = string.Empty;
            var htmlAutoFocus = string.Empty;
            var rdoClass = "radio";

            if (string.IsNullOrEmpty(btnClass))
            {
                btnClass = "btn-default";
            }
            if (isChecked)
            {
                htmlChecked = "checked='checked'";
            }
            if (isAutoFocus)
            {
                htmlAutoFocus = "autofocus='autofocus'";
            }
            if (useInline)
            {
                rdoClass = "radio-inline";
            }

            // Build the Radio Button
            sb.Append($"<div class='{rdoClass}'>");
            sb.Append($"  <label class='btn {btnClass}'>");
            sb.Append(
                $"    <input id='{id}' name='{name}' type='radio' value='{value}' {htmlChecked} {htmlAutoFocus} {GetHtmlAttributes(htmlAttributes)} />");
            sb.Append($"    {text}");
            sb.Append("  </label>");
            sb.Append("</div>");

            // Return an MVC HTML String
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion

        #region Bootstrap/HTML 5 TextBox Helpers
        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          object htmlAttributes = null)
        {
            return TextBox5For(htmlHelper, expression, Html5InputTypes.text, string.Empty, string.Empty, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="type">The text type to set (text, password, date, tel, email, etc.).</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          Html5InputTypes type,
          object htmlAttributes = null)
        {
            return TextBox5For(htmlHelper, expression, type, string.Empty, string.Empty, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="title">The HTML 5 'title' attribute to set.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string title,
          object htmlAttributes = null)
        {
            return TextBox5For(htmlHelper, expression, Html5InputTypes.text, title, title, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="type">The text type to set (text, password, date, tel, email, etc.).</param>
        /// <param name="title">The HTML 5 'title' attribute to set.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          Html5InputTypes type,
          string title,
          object htmlAttributes = null)
        {
            return TextBox5For(htmlHelper, expression, type, title, title, false, false, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="title">The HTML 5 'title' attribute to set.</param>
        /// <param name="isRequired">Whether or not to set the 'required' attribute on this text box.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this text box.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          string title,
          bool isRequired,
          bool isAutoFocus,
          object htmlAttributes = null)
        {
            return TextBox5For(htmlHelper, expression, Html5InputTypes.text, title, title, isRequired, isAutoFocus, htmlAttributes);
        }

        /// <summary>
        /// Bootstrap and HTML 5 Text Box Helper
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to render.</param>
        /// <param name="type">The text type to set (text, password, date, tel, email, etc.).</param>
        /// <param name="title">The HTML 5 'title' attribute to set.</param>
        /// <param name="placeholder">The HTML 5 'placeholder' attribute to set.</param>
        /// <param name="isRequired">Whether or not to set the 'required' attribute on this text box.</param>
        /// <param name="isAutoFocus">Whether or not to set the 'autofocus' attribute on this text box.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML input element with the appropriate type set.</returns>
        public static MvcHtmlString TextBox5For<TModel, TValue>(
          this HtmlHelper<TModel> htmlHelper,
          Expression<Func<TModel, TValue>> expression,
          Html5InputTypes type,
          string title,
          string placeholder,
          bool isRequired,
          bool isAutoFocus,
          object htmlAttributes = null)
        {
            var html = default(MvcHtmlString);
            var attr = new Dictionary<string, object>();

            if (htmlAttributes != null)
            {
                var attributes =
                  HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    attr.Add(item.Key, item.Value);
                }
            }

            attr.Add("type", type.ToString());
            attr.Add("class", "form-control");
            if (!string.IsNullOrEmpty(title))
            {
                attr.Add("title", title);
            }
            if (!string.IsNullOrEmpty(placeholder))
            {
                attr.Add("placeholder", placeholder);
            }
            if (isAutoFocus)
            {
                attr.Add("autofocus", "autofocus");
            }
            if (isRequired)
            {
                attr.Add("required", "required");
            }

            html = InputExtensions.TextBoxFor(htmlHelper,
                                              expression,
                                              attr);

            return html;
        }
        #endregion

        #region GetHtmlAttributes Method
        /// <summary>
        /// Break the HTML Attributes apart and put into key='value' pairs for adding to an HTML element.
        /// </summary>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>A string with the key='value' pairs</returns>
        private static string GetHtmlAttributes(object htmlAttributes)
        {
            var ret = string.Empty;

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }
        #endregion
    }
}