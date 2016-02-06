﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using Decision.Common.MVC;
    using Decision.Utility;
    using Decision.Web.HtmlHelpers;
    using MvcSiteMapProvider.Web.Html;
    using MvcSiteMapProvider.Web.Html.Models;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/EducationalBackground/List.cshtml")]
    public partial class _Views_EducationalBackground_List_cshtml : System.Web.Mvc.WebViewPage<Decision.ViewModel.EducationalBackground.EducationalBackgroundListViewModel>
    {
        public _Views_EducationalBackground_List_cshtml()
        {
        }
        public override void Execute()
        {
            
            #line 2 "..\..\Views\EducationalBackground\List.cshtml"
  
    ViewBag.Title = "لیست سوابق تحصیلی متقاضی";

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 5 "..\..\Views\EducationalBackground\List.cshtml"
Write(Html.AntiForgeryToken());

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<div");

WriteLiteral(" class=\"panel panel-default\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"panel-heading\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 11 "..\..\Views\EducationalBackground\List.cshtml"
           Write(Html.HelpAlert(Url.Content("~/Content/images/lightbulb.png"), "برای درج سابقه تحصیلی جدید از دکمه (افزودن سابقه تحصیلی جدید) استفاده کنید."
                    , "در صورت استفاده از امکان آپلود فایل ضمیمه ، از تصویر استفاده کنید."));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n        <hr");

WriteLiteral(" class=\"margin-bottom-5 margin-top-5\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-12\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" aria-controls=\"create-educationalBackground\"");

WriteLiteral(" aria-expanded=\"false\"");

WriteLiteral(" class=\"btn btn-default btn-sm btn-block\"");

WriteLiteral(" data-target=\"#create-educationalBackground\"");

WriteLiteral(" data-toggle=\"collapse\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(">\r\n                    <i");

WriteLiteral(" class=\"fa fa-plus\"");

WriteLiteral("></i>افزودن سابقه  جدید\r\n                </button>\r\n            </div>\r\n        <" +
"/div>\r\n        <hr");

WriteLiteral(" class=\"margin-bottom-5 margin-top-5\"");

WriteLiteral(" />\r\n        <div");

WriteLiteral(" class=\"row collapse\"");

WriteLiteral(" id=\"create-educationalBackground\"");

WriteLiteral(">\r\n");

            
            #line 25 "..\..\Views\EducationalBackground\List.cshtml"
            
            
            #line default
            #line hidden
            
            #line 25 "..\..\Views\EducationalBackground\List.cshtml"
               Html.RenderAction(MVC.EducationalBackground.Create(Model.SearchRequest.ApplicantId));
            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"panel-body\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(" id=\"educationalBackgroundList\"");

WriteLiteral(">\r\n");

            
            #line 30 "..\..\Views\EducationalBackground\List.cshtml"
            
            
            #line default
            #line hidden
            
            #line 30 "..\..\Views\EducationalBackground\List.cshtml"
               Html.RenderPartial(MVC.EducationalBackground.Views.ViewNames._ListAjax, Model);
            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"panel-footer padding-5-5\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"col-md-offset-2 col-md-8\"");

WriteLiteral(">\r\n                <button");

WriteLiteral(" data-page=\"2\"");

WriteLiteral(" data-json=\'{\"ApplicantId\":\"");

            
            #line 36 "..\..\Views\EducationalBackground\List.cshtml"
                                                          Write(Model.SearchRequest.ApplicantId);

            
            #line default
            #line hidden
WriteLiteral("\"}\'");

WriteLiteral(" data-container=\"#educationalBackgroundList\"");

WriteLiteral("\r\n                        data-progress=\"#progress\"");

WriteLiteral("\r\n                        data-load-url=\"");

            
            #line 38 "..\..\Views\EducationalBackground\List.cshtml"
                                  Write(Url.Action(MVC.EducationalBackground.ListAjax()));

            
            #line default
            #line hidden
WriteLiteral("\"");

WriteLiteral("\r\n                        onclick=\"justPaging(this)\"");

WriteLiteral(" class=\"btn btn-info btn-block btn-sm\"");

WriteLiteral(" type=\"button\"");

WriteLiteral(">\r\n                    بیشتر\r\n                </button>\r\n            </div>\r\n    " +
"    </div>\r\n        <div");

WriteLiteral(" id=\"progress\"");

WriteLiteral(" align=\"center\"");

WriteLiteral(" style=\"display: none\"");

WriteLiteral(">\r\n            <img");

WriteAttribute("src", Tuple.Create(" src=\"", 2196), Tuple.Create("\"", 2246)
            
            #line 45 "..\..\Views\EducationalBackground\List.cshtml"
, Tuple.Create(Tuple.Create("", 2202), Tuple.Create<System.Object, System.Int32>(Url.Content("~/Content/images/loading.gif")
            
            #line default
            #line hidden
, 2202), false)
);

WriteLiteral(" alt=\"loading...\"");

WriteLiteral(" />\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");

DefineSection("Menu", () => {

WriteLiteral("\r\n");

            
            #line 53 "..\..\Views\EducationalBackground\List.cshtml"
    
            
            #line default
            #line hidden
            
            #line 53 "..\..\Views\EducationalBackground\List.cshtml"
      Html.RenderPartial(MVC.Applicant.Views._ApplicantRelatedLinksBuilder, Model.SearchRequest.ApplicantId);
            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

DefineSection("Scripts", () => {

WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 56 "..\..\Views\EducationalBackground\List.cshtml"
Write(Scripts.Render("~/bundles/jqueryval"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 57 "..\..\Views\EducationalBackground\List.cshtml"
Write(Scripts.Render("~/bundles/datePicker"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("    ");

            
            #line 58 "..\..\Views\EducationalBackground\List.cshtml"
Write(Scripts.Render("~/bundles/formData"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

});

WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
