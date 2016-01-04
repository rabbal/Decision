using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Decision.ViewModel.Role
{
    /// <summary>
    /// ویو مدل نمایش گروه کاربری
    /// </summary>
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            PermissionsList=new List<string>();
        }
        /// <summary>
        /// آی دی
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// نام
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// آیا کاربران این گروه قفل شوند؟
        /// </summary>
        public  bool IsBanned { get; set; }
        /// <summary>
        /// آیا گروه سیستمی است؟
        /// </summary>
        public bool IsSystemRole { get; set; }
        /// <summary>
        /// دسترسی ها
        /// </summary>
        public IEnumerable<string> PermissionsList { get; set; }
        /// <summary>
        /// لیست دسترسی های گروه کاربری
        /// </summary>
        public  string Permissions { get; set; }
        /// <summary>
        ///ساختار ایکس ام ال لیست دسترسی های گروه کاربری
        /// </summary>
        public XElement XmlPermission
        {
            get { return XElement.Parse(Permissions); }
        }

    }
}
