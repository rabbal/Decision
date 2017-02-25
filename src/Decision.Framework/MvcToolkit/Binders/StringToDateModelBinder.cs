using System;
using System.Web.Mvc;

namespace Decision.Framework.MvcToolkit.Binders
{
    public class StringToDateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = PersianDateTime.Parse(valueResult.AttemptedValue).ToDateTime();
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("تاریخ را به شکل صحیح [ به عنوان مثال 1399/02/01] وارد کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
