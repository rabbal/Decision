
$(function () {
    // حذف تمام تگ‌های یک قطعه متن
    function removeAllTagsAndTrim(html) {
        return !html ? "" : jQuery.trim(html.replace(/(<([^>]+)>)/ig, ""));
    }

    // متد اصلی اعتبارسنجی را ابتدا ذخیره می‌کنیم
    jQuery.validator.methods.originalRequired = jQuery.validator.methods.required;
    // نحوه بازنویسی متد توکار اعتبار سنجی جهت استفاده از یک متد سفارشی
    jQuery.validator.addMethod("required", function (value, element, param) {
        value = removeAllTagsAndTrim(value);
        if (!value) {
            return false;

        }
        //  فراخوانی متد اصلی اعتبار سنجی در صورت شکست تابع سفارشی
        return jQuery.validator.methods.originalRequired.call(this, value, element, param);
    }, jQuery.validator.messages.required);
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnablePostbackValidation();
});

var AjaxForm = new Object();

AjaxForm.EnableAjaxFormvalidate = function (formId) {
    $.validator.unobtrusive.parse('#'+ formId);
};


AjaxForm.EnableStringMaxLength = function () {
    $("input[data-val-length-max]").each(function (i, e) {
        var input = $(e);
        var maxlength = input.attr("data-val-length-max");
        input.attr("maxlength", maxlength);
    });
}

AjaxForm.ValidateForm = function (formId) {
    var val = $('#' + formId).validate();
    val.form();
    return val.valid();
};


AjaxForm.CustomSubmit = function (element, formId) {
    
    if (!AjaxForm.ValidateForm(formId)) return;
    $(element).button('loading');
    $('#' + formId).submit();
};
AjaxForm.CustomSubmitWithEditor = function (element, formId) {

    for(instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }

    if (!AjaxForm.ValidateForm(formId)) return;
    $(element).button('loading');
    $('#' + formId).submit();
};

AjaxForm.ResetButton = function (id) {
    $('#' + id).button('reset');
};

AjaxForm.EnablePostbackValidation = function () {
    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-error');
            }
        });
    });
};

//AjaxForm.EnableBootstrapStyleValidation = function () {
//    $.validator.setDefaults({
//        ignore: "",
//        highlight: function (element, errorClass, validClass) {
//            if (element.type === 'radio') {
//                this.findByName(element.name).addClass(errorClass).removeClass(validClass);
//            } else {
//                $(element).addClass(errorClass).removeClass(validClass);
//                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
//            }
//            $(element).trigger('highlited');
//        },
//        unhighlight: function (element, errorClass, validClass) {
//            if (element.type === 'radio') {
//                this.findByName(element.name).removeClass(errorClass).addClass(validClass);
//            } else {
//                $(element).removeClass(errorClass).addClass(validClass);
//                $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
//            }
//            $(element).trigger('unhighlited');
//        }
//    });
//};
