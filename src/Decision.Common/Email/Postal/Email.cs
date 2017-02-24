using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mail;
using System.Web.Mvc;

namespace Decision.Common.Email.Postal
{
   public class Email : DynamicObject, IViewDataContainer
    {
        public Email(string viewName)
        {
            if (viewName == null) throw new ArgumentNullException(nameof(viewName));
            if (string.IsNullOrWhiteSpace(viewName)) throw new ArgumentException("View name cannot be empty.", nameof(viewName));

            Attachments = new List<Attachment>();
            ViewName = viewName;
            ViewData = new ViewDataDictionary(this);
            ImageEmbedder = new ImageEmbedder();
        }
        protected Email()
        {
            Attachments = new List<Attachment>();
            ViewName = DeriveViewNameFromClassName();
            ViewData = new ViewDataDictionary(this);
            ImageEmbedder = new ImageEmbedder();
        }

        public string ViewName { get; set; }
        public ViewDataDictionary ViewData { get; set; }
        public List<Attachment> Attachments { get; set; }
        internal ImageEmbedder ImageEmbedder { get; private set; }
        public void Attach(Attachment attachment)
        {
            Attachments.Add(attachment);
        }


        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            ViewData[binder.Name] = value;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return ViewData.TryGetValue(binder.Name, out result);
        }

        private string DeriveViewNameFromClassName()
        {
            var viewName = GetType().Name;
            if (viewName.EndsWith(nameof(Email))) viewName = viewName.Substring(0, viewName.Length - nameof(Email).Length);
            return viewName;
        }
    }
}