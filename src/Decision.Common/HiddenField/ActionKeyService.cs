using System.Collections.Generic;
using System.Linq;

namespace Decision.Common.HiddenField
{
    public class ActionKeyService : IActionKeyService
    {

        private static readonly IList<ActionKey> ActionKeys;

        static ActionKeyService()
        {
            ActionKeys = new List<ActionKey>();

        }

        /// <summary>
        /// پیدا کردن کلید متناظر هر ویو.ایجاد کلید جدید در صورت عدم وجود کلید در سیستم
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public string GetActionKey(string token, string controller, string area = "")
        {
            area = area ?? "";
            var actionKey = ActionKeys.FirstOrDefault(a =>
                 a.Controller.ToLower() == controller.ToLower() &&
                a.ActionKeyValue == token &&
                 a.Area.ToLower() == area.ToLower());
            return actionKey != null ? actionKey.ActionKeyValue : AddActionKey(token, controller, area);
        }

        /// <summary>
        /// اضافه کردن کلید جدید به سیستم
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        private string AddActionKey(string token, string controller, string area = "")
        {
            var actionKey = new ActionKey
            {
                Controller = controller,
                Area = area,
                ActionKeyValue = token
            };
            ActionKeys.Add(actionKey);
            return actionKey.ActionKeyValue;
        }

    }
}