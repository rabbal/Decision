function formDataSubmit(btn, form, containerId, section) {

    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }

    var formId = '#' + form;
    if (!AjaxForm.ValidateForm(form)) return;
    $(btn).button('loading');
    var formObj = $(formId);

    formObj.ajaxForm({
        cache: false,
        complete: function (xhr, status) {
            var data = $.parseJSON(xhr.responseText);
            var $boxes = $(data.View);
            if (xhr.status == 403) {
                window.location = '/Account/Login';
            } else if (xhr.status == 400) {
                dangerNoty("داده ارسالی شما معتبر نیست");
            } else if (xhr.status == 404) {
                dangerNoty("اطلاعات درخواستی یافت نشد");
            } else if (status === 'error' || !data || data == "nok") {
                dangerNoty("خطایی در عملیات بوجود آمده است");
            } else if (data.success) {
                infoNoty("عملیات  با موفقیت انجام شد");
                $(containerId).append($boxes);
                $(btn).button('reset');
                formObj.resetForm();
                for (instance in CKEDITOR.instances) {
                    CKEDITOR.instances[instance].setData('');
                }
            } else if (!data.success) {
                $(section).html($boxes);
                AjaxForm.EnableAjaxFormvalidate(formId);
                AjaxForm.EnablePostbackValidation();
                AjaxForm.EnableStringMaxLength();
                $(btn).button('reset');
            }
        }
    });
    formObj.submit();
}


function formDataSubmitWidOutEditor(btn, form, containerId, section) {
    
    var formId = '#' + form;
    $(formId).removeData('validator');
    $(formId).removeData('unobtrusiveValidation');
    AjaxForm.EnableAjaxFormvalidate(form);
    if (!AjaxForm.ValidateForm(form)) return;
    $(btn).button('loading');
    var formObj = $(formId);

    formObj.ajaxForm({
        cache: false,
        complete: function (xhr, status) {
            var data = $.parseJSON(xhr.responseText);
            var $boxes = $(data.View);
            if (xhr.status == 403) {
                window.location = '/Account/Login';
            } else if (xhr.status == 400) {
                dangerNoty("داده ارسالی شما معتبر نیست");
            } else if (xhr.status == 404) {
                dangerNoty("اطلاعات درخواستی یافت نشد");
            } else if (status === 'error' || !data || data == "nok") {
                dangerNoty("خطایی در عملیات بوجود آمده است");
            } else if (data.success) {
                infoNoty("عملیات  با موفقیت انجام شد");
                $(containerId).append($boxes);
                $(btn).button('reset');
                formObj.resetForm();
                onquestiontype('Type');
            } else if (!data.success) {
                $(section).html($boxes);
                AjaxForm.EnableAjaxFormvalidate(form);
                AjaxForm.EnablePostbackValidation();
                AjaxForm.EnableStringMaxLength();
                $(btn).button('reset');
            }
        }
    });
    formObj.submit();
}
