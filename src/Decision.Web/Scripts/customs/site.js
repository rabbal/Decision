
//$('.masonry').masonry({
// itemSelector : '.data-item',
// isRTL : true,
// isAnimated : true
//});
var CKEDITOR_BASEPATH = '/Scripts/ckeditor/';
$(function () {

    highLightMenu();
    makeFileUpload();
    $(document).on('focus', 'input.datepicker', function () {
        $(this).datepicker({
            changeMonth: true, //
            changeYear: true, // T
            yearRange: 'c-100:c+0'
        });
    });

    $(document).on('click', '.clearFile', function () { $($(this).data('file')).val(''); });

    $('form.search').keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    $("[data-toggle='tooltip']").tooltip();
    $('#dl-menu').dlmenu({
        animationClasses: {
            classin: 'dl-animate-in-1',
            classout: 'dl-animate-out-1'
        }
    });


    //highLightMenu();

    $(document).on("click", "button[id^='remove']", function (e) {
        var button = $(this);
        button.deleteItem({
            'postUrl': button.data('deleteUrl')
        });
    });

    $(document).on("click", "span[id^='ban']", function (e) {
        var span = $(this);
        span.banned({
            'postUrl': span.data('bannedUrl')
        });
    });

    $(".asyncLoad").each(function () {
        $(this).asyncLoad();
    });
});


/*####################  Prevent Navigation ###############*/


var warningBeforeLoad = function () {
    var msg = "اطلاعات دخیره نشده ای در این صفحه دارید و با" +
        " هدایت به صفحه بعد این اطلاعات را از دست خواهید داد";
    $('button:button').click(function () {
        msg = null;
    });
    $('input:not(:button,:submit),textarea,select').change(function () {
        window.onbeforeunload = function () {
            if (msg != null)
                return msg;
        };
    });
    $('input:checkbox,input:radio').click(function () {
        window.onbeforeunload = function () {
            if (msg != null)
                return msg;
        };
    });
}


/*######################     FileUpload       ###############*/


/*##################### site faveicon  ################*/
function sitesFavicon() {
    $("a").each(function () {
        var $a = $(this);
        var href = $a.attr("href");
        // see if the link is external 
        if (href && href.match(/^http/))
            if (!href.match(document.domain)) {
                var domain = href.replace(/<\S[^><]*>/g, "").split('/')[2];
                var image = '<img src="http://' + domain +
                '/favicon.ico" width="0" ' +
                ' onload="this.width=16;this.height=16;this.style.paddingLeft=\'3px\';this.style.paddingRight=\'1px\';" ' +
                ' style="border:0" ' +
                ' onerror="this.src=\'alternative url\';" />';
                $(this).prepend(image);
            }
    });
}

/*#######################  notify admin for error on load image ####################*/
function errorReplace(arg) {
    //ارسال پیغام خطا
    $.ajax({
        type: "POST",
        url: "api/url",
        data: "{'image': '" + arg.src + "','page':'" + location.href + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
    //نمایش تصویری دلخواه بجای نمونه مفقود
    $(arg).attr('src', 'alternativeImage');
}

/*############### برجسته سازی آدرس ها جاری  ############*/

var highLightMenu = function () {

    $(document).ready(function () {
        $("#navbar-main  a").each(function () {
            var $a = $(this);
            var href = $a.attr("href");
            if (href && (location.pathname.toLowerCase().split('/')[1] === href.toLowerCase().split('/')[1])) {
                //صفحه جاری را یافتیم
                $a.closest('li').addClass("active");
            }
            if (href && (location.pathname.toLowerCase() === href.toLowerCase())) {
                //صفحه جاری را یافتیم
                $a.closest('li').addClass("active");
            }
        });

         $("#sidebar  a.second-split").each(function () {
            var $a = $(this);
            var href = $a.attr("href");
            var check = false;
           
             if ( href && (location.pathname.toLowerCase().split('/')[2] === href.toLowerCase().split('/')[2])){
                //صفحه جاری را یافتیم
                 $a.addClass("active");
                 check = true;
             }
            if (!check && href && (location.pathname.toLowerCase() === href.toLowerCase())) {
                //صفحه جاری را یافتیم
                $a.addClass("active");
            }
         });

         $("#sidebar  a.thrid-split").each(function () {
             var $a = $(this);
             var href = $a.attr("href");
             var check = false;
             if ( href && (location.pathname.toLowerCase().split('/')[3] === href.toLowerCase().split('/')[3])) {
                 //صفحه جاری را یافتیم
                 $a.addClass("active");
                 check = true;
             }
             if (!check && href && (location.pathname.toLowerCase() === href.toLowerCase())) {
                 //صفحه جاری را یافتیم
                 $a.addClass("active");
             }
         });
         
    });
};

function preventEnterSubmit(e) {
    if (e.which == 13) {
        var $targ = $(e.target);

        if (!$targ.is("textarea") && !$targ.is(":button,:submit")) {
            var focusNext = false;
            $(this).find(":input:visible:not([disabled],[readonly]), a").each(function () {
                if (this === e.target) {
                    focusNext = true;
                }
                else if (focusNext) {
                    $(this).focus();
                    return false;
                }
            });

            return false;
        }
    }
}


//function createEditor() {
//    CKEDITOR.editorConfig = function (config) {
//        config.language = 'fa';
//        config.toolbarGroups = [
//            { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
//            { name: 'clipboard', groups: ['clipboard', 'undo'] },
//            { name: 'links', groups: ['links'] },
//            { name: 'insert', groups: ['insert'] },
//            { name: 'forms', groups: ['forms'] },
//            { name: 'tools', groups: ['tools'] },
//            { name: 'document', groups: ['mode', 'document', 'doctools'] },
//            { name: 'others', groups: ['others'] },
//            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
//            { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
//            { name: 'styles', groups: ['styles'] },
//            { name: 'colors', groups: ['colors'] },
//            { name: 'about', groups: ['about'] }
//        ];

//        config.removeButtons = 'Underline,Subscript,Superscript,Scayt,Link,Unlink,Anchor,Image,Source,About';
//    };
//    $('.editor').ckeditor();
//}

function makeFileUpload() {

    $("input[type=file]").fileinput({
        showUpload: false,
        msgInvalidFileType: "از فایل معتبر استفاده کنید",
        browseClass: "btn btn-success btn-md",
        showPreview: false,
        browseLabel: "انتخاب",
        browseIcon: '<i class="fa fa-file"></i>',
        removeClass: "btn btn-danger btn-md",
        removeLabel: "حذف",
        removeIcon: '<i class="fa fa-trash"></i>'
    });
}

var $index=0;
var generateOption = function (btn, index, container) {
    
    if (index != 0)
        $index = index+1;
    var optionIndex = { 'index': $index };
    var template = $('#answerOption').html();
    var option = Mustache.render(template, optionIndex);
    $(option).insertBefore(container);
    $index++;
}


function onquestiontype(select) {
    $index = 0;
    var selectId = '#' + select;
    if ($(selectId).val() == 3 || $(selectId).val() == 4) {
        $('button.option').show();
    }
    else
    {
        $('button.option').hide();
        $('div.option').remove();
    }
}