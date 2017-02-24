using System;
using System.Globalization;
using System.Web.Mvc;

namespace NTierMvcFramework.Common.MvcToolkit.Binders
{
    public class PersianDateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var modelState = new ModelState { Value = valueResult };
            object actualValue = new DateTime(1900, 1, 1); //todo: توصيه شده تاريخ تولد خودتان را در اينجا قرار دهيد
            try
            {
                var minute = int.Parse(bindingContext.ValueProvider.GetValue("Minute").AttemptedValue);
                var hour = int.Parse(bindingContext.ValueProvider.GetValue("Hour").AttemptedValue);
                var parts = valueResult.AttemptedValue.Split('/'); //ex. 1391/1/19
                if (parts.Length != 3) return actualValue;
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);
                actualValue = new DateTime(year, month, day, hour, minute, 0, new PersianCalendar());
            }
            catch (FormatException e)
            {
                modelState.Errors.Add("تاریخ را به شکل صحیح [ به عنوان مثال 1371/9/28] وارد کنید");
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }

}