//using Decision.DomainClasses.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace Web.Helpers
//{
//    public class DropDown
//    {
//        public static SelectList GetUserSearchByList(UserSearchBy userSearchBy)
//        {
//            var selectedUserSearchBy = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "نام کاربری", Value = "UserName"},
//                new SelectListItem {Text = "شماره همراه", Value = "PhoneNumber"},
//                new SelectListItem {Text = "آی پی", Value = "Ip"},
//                new SelectListItem {Text = "نقش", Value = "RoleDescription"}
//            };

//            return new SelectList(selectedUserSearchBy, "Value", "Text", userSearchBy);
//        }

//        public static SelectList GetUserOrderByList(UserOrderBy usersOrderBy)
//        {
//            var selectedOrder = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "نام کاربری", Value = "UserName"},
//                new SelectListItem {Text = "تاریخ عضویت", Value = "RegisterDate"},
//                new SelectListItem {Text = "تعداد خرید", Value = "OrderCount"}
//            };
//            return new SelectList(selectedOrder, "Value", "Text", usersOrderBy);
//        }
//        public static SelectList GetCountList(int selected)
//        {
//            var selectedCount = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "10", Value = "10"},
//                new SelectListItem {Text = "30", Value = "30"},
//                new SelectListItem {Text = "50", Value = "50"}
//            };
//            return new SelectList(selectedCount, "Value", "Text", selected);
//        }
//        public static SelectList GetOrderList(Order order)
//        {
//            var selectedUserOrderBy = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "نزولی", Value = "Descendign"},
//                new SelectListItem {Text = "صعودی", Value = "Asscending"}
//            };
//            return new SelectList(selectedUserOrderBy, "Value", "Text", order);
//        }
//        public static SelectList GetUserOperationList(UserOperations userOperations)
//        {
//            var selectedOperation = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "کارهای دسته جمعی", Value = "AllWorks"},
//                new SelectListItem {Text = "حذف", Value = "DeleteSelectedUses"},
//                new SelectListItem {Text = "قفل کردن", Value = "BanedSelectedUsers"},
//                new SelectListItem {Text = "باز کردن قفل", Value = "UnBanedSelectedUsers"}
//            };

//            return new SelectList(selectedOperation, "Value", "Text", userOperations);
//        }


//        public static SelectList GetProductOrderByList(ProductOrderBy productOrderBy)
//        {

//            var list = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "نام", Value = "Name"},
//                new SelectListItem {Text = "تعداد در انبار", Value = "StockCount"},
//                new SelectListItem {Text = "تعداد فروش", Value = "SellCount"},
//                new SelectListItem {Text = "تعداد مشاهده", Value = "ViewCount"},
//                new SelectListItem {Text = "تعداد رزرو شده در کارت", Value = "ReserveCount"},
//                new SelectListItem {Text = "قیمت", Value = "Price"},
//                new SelectListItem {Text = "درصد تخفیف", Value = "DiscountPercent"},
//                new SelectListItem {Text = "مینیمم مقدار هشدار", Value = "NotificationStockMinimun"}

//            };

//            return new SelectList(list, "Value", "Text", productOrderBy);
//        }


//        public static SelectList GetShowTranstionSlide(string showTranstion)
//        {

//            var list = new List<SelectListItem>
//            {
//                 new SelectListItem {Text = "--انتخاب--", Value = ""},
//                new SelectListItem {Text = "بالا", Value = "up"},
//                new SelectListItem {Text = "پایین", Value = "down"},
//                new SelectListItem {Text = "چپ", Value = "left"},
//                new SelectListItem {Text = "راست", Value = "right"}
//            };

//            return new SelectList(list, "Value", "Text", showTranstion);
//        }
//        public static SelectList GetHideTranstionSlide(string hideTranstion)
//        {

//            var list = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "--انتخاب--", Value = ""},
//                new SelectListItem {Text = "بالا", Value = "up"},
//                new SelectListItem {Text = "پایین", Value = "down"},
//                new SelectListItem {Text = "چپ", Value = "left"},
//                new SelectListItem {Text = "راست", Value = "right"}
//            };

//            return new SelectList(list, "Value", "Text", hideTranstion);
//        }
//        public static SelectList GetPositonSlide(string position)
//        {
//            var list = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "--انتخاب--", Value = ""},
//                new SelectListItem {Text = "پایین سمت جپ", Value = "bottomLeft"},
//                new SelectListItem {Text = "پایین سمت راست", Value = "bottomRight"},
//                new SelectListItem {Text = "بالا سمت چپ", Value = "topLeft"},
//                new SelectListItem {Text = "بالا سمت راست", Value = "topRight"},
//                new SelectListItem {Text = "وسط وسط", Value = "centerCenter"},
//                new SelectListItem {Text = "وسط بالا ", Value = "centerTop"},
//                new SelectListItem {Text = "وسط پایین", Value = "centerBottom"}
//            };

//            return new SelectList(list, "Value", "Text", position);
//        }

//        public static SelectList GetSearchPageCount(int seleted)
//        {

//            var list = new List<SelectListItem>
//            {
//                 new SelectListItem {Text = "12", Value = "12"},
//                new SelectListItem {Text = "24", Value = "24"},
//                new SelectListItem {Text = "36", Value = "36"},
//                new SelectListItem {Text = "48", Value = "48"}
//            };

//            return new SelectList(list, "Value", "Text", seleted);
//        }

//        public static SelectList GetSearchFilters(PSFilter seleted)
//        {

//            var list = new List<SelectListItem>
//            {
//                new SelectListItem {Text = "انتخاب کنید", Value = "All"},
//                 new SelectListItem {Text = "جدیدترین ها", Value = "New"},
//                new SelectListItem {Text = "پرفروش ترین ها", Value = "MoreSell"},
//                new SelectListItem {Text = "محبوب ترین ها", Value = "Beloved"},
//                new SelectListItem {Text = "پربازدید ترین ها", Value = "MoreView"},
//                new SelectListItem {Text = "تخفیف دار ها", Value = "HasDiscount"},
//                new SelectListItem {Text = "محصولات موجود", Value = "IsInStock"},
//                new SelectListItem {Text = "محصولات با ارسال رایگان", Value = "FreeSend"}
//            };

//            return new SelectList(list, "Value", "Text", seleted);
//        }
//    }
//}