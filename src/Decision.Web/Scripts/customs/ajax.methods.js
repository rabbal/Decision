
/*############### OnBegins ##############*/
function onBegin(xhr, a) {
    infoNoty("در حال ارسال اطلاعات");
    $(a).attr('disabled', 'disabled');
}


function modalOnBegin(xhr, formId) {
    AjaxForm.EnableAjaxFormvalidate(formId);
}

/*############### OnSuccesses ##############*/
function onSuccess(data, status, xhr) {

}

function onSuccessEnableValidation(data, status, xhr, formId) {
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnablePostbackValidation();
    AjaxForm.EnableAjaxFormvalidate(formId);
}
function onSuccessFormValidation(data, status, xhr, formId, modal) {
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnablePostbackValidation();
    AjaxForm.EnableAjaxFormvalidate(formId);
    if (data == "system") {
        warningNoty("رکورد مورد نظر از پیش فرض های سیستم میباشد و امکان ایجاد تغییرات برای آن وجود ندارد");
        return;
    }
    if (data == "in-refer") {
        warningNoty("متقاضی مورد نظر  قبلا به یک اپراتور ارجاع داده شده است.");
        return;
    }
    $('#' + modal).modal('show');
}
function onSuccessResetBtnWithValidation(data, status, xhr, btn, formId) {
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnableAjaxFormvalidate(formId);
    AjaxForm.EnablePostbackValidation();
    $('#' + btn).button('reset');
}

function onSuccessResetBtn(data, status, xhr, btn) {
    $('#' + btn).button('reset');
}

function modalOnSuccess(data, status, xhr, modal) {
    $('#' + modal).modal('show');
}


function inlineOnSuccess(data, status, xhr, btn, formId) {
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnableAjaxFormvalidate(formId);
    AjaxForm.EnablePostbackValidation();
    $('#' + btn).button('reset');

}

function inlineEditGetOnSuccess(data, status, xhr, formId) {
    AjaxForm.EnableStringMaxLength();
    AjaxForm.EnableAjaxFormvalidate(formId);
    AjaxForm.EnablePostbackValidation();

    makeFileUpload();
}
/*############### OnCompletes ##############*/
function onComplete(xhr, status) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (data == "system") {
        warningNoty("اطلاعات درخواستی شما جز پیش فرض سیستم است و امکان تغییر آن ممکن نیست.");
    }
    infoNoty("عملیات  با موفقیت انجام شد");
    $("[data-toggle='tooltip']").tooltip();
}

function modalOnComplete(xhr, status, refreshBtn, modal) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        $('#' + modal).modal('hide');
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data == "ok") {
        $('#' + modal).modal('hide');
        infoNoty("عملیات  با موفقیت انجام شد");
    }
    $("#" + refreshBtn).trigger('click');
};

function createOnComplete(xhr, status, containerId, modal, formId, btn) {
    var data = $.parseJSON(xhr.responseText);
    var $boxes = $(data.View);
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        $(modal).modal('hide');
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data.success) {
        $(modal).modal('hide');
        infoNoty("عملیات  با موفقیت انجام شد");
        $('#' + containerId).append($boxes);
    }
    else if (!data.success) {
        $(modal).html($boxes);
        AjaxForm.EnableAjaxFormvalidate(formId);
        AjaxForm.EnablePostbackValidation();
        AjaxForm.EnableStringMaxLength();
        $(btn).button('reset');
    }
};

function postOnComplete(xhr, status, containerId, modal, formId, btn) {
    var data = $.parseJSON(xhr.responseText);
    var $boxes = $(data.View);
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        $(modal).modal('hide');
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data.success) {
        $(modal).modal('hide');
        infoNoty("عملیات  با موفقیت انجام شد");
        $('#' + containerId).replaceWith($boxes);
    }
    else if (!data.success) {
        $(modal).html($boxes);
        AjaxForm.EnableAjaxFormvalidate(formId);
        AjaxForm.EnablePostbackValidation();
        AjaxForm.EnableStringMaxLength();
        $(btn).button('reset');
    }
};

function createAjaxFormOnComplete(xhr, status, containerId, modal, formId, btn) {
    var data = $.parseJSON(xhr.responseText);
    var $boxes = $(data.View);
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        $(modal).modal('hide');
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data.success) {
        $(modal).modal('hide');
        infoNoty("عملیات  با موفقیت انجام شد");
        $('#' + containerId).append($boxes);
    }
    else if (!data.success) {
        $(modal).html($boxes);
        AjaxForm.EnableAjaxFormvalidate(formId);
        AjaxForm.EnablePostbackValidation();
        AjaxForm.EnableStringMaxLength();
        $(btn).button('reset');
    }
};


function editOnComplete(xhr, status, formId, btn) {
    var data = $.parseJSON(xhr.responseText);
    var $boxes = $(data.View);
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data == "system") {
        warningNoty("اطلاعات درخواستی شما جز پیش فرض سیستم است و امکان تغییر آن ممکن نیست.");
    }
    else if (data.success) {
        infoNoty("عملیات  با موفقیت انجام شد");
        $('#' + $('#' + formId).data('ajaxUpdate')).replaceWith($boxes);
    }
    else if (!data.success) {
        $('#' + $('#' + formId).data('ajaxUpdate')).replaceWith($boxes);
        AjaxForm.EnableAjaxFormvalidate(formId);
        AjaxForm.EnablePostbackValidation();
        AjaxForm.EnableStringMaxLength();
        $(btn).button('reset');
    }
};

function unAuthorizeOnComplete(xhr, status) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
}

function editGetEditorOnComplete(xhr, status) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data == "system") {
        warningNoty("اطلاعات درخواستی شما جز پیش فرض سیستم است و امکان تغییر آن ممکن نیست.");
    }

};

function editGetOnComplete(xhr, status) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
    else if (data == "system") {
        warningNoty("اطلاعات درخواستی شما جز پیش فرض سیستم است و امکان تغییر آن ممکن نیست.");
    }
};

function cancelEditOnComplete(xhr, status) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (xhr.status == 400) {
        dangerNoty("داده ارسالی شما معتبر نیست");
    }
    else if (xhr.status == 404) {
        dangerNoty("اطلاعات درخواستی یافت نشد");
    }
    else if (status === 'error' || !data || data == "nok") {
        dangerNoty("خطایی در عملیات بوجود آمده است");
    }
};


function searchOnComplete(xhr, status, progressId, userPager, container) {
    var data = xhr.responseText;
    if (xhr.status == 403) {
        window.location = '/Account/Login';
    }
    else if (status === 'error' || !data) {
        dangerNoty('خطایی در بارگذاری بوجود آمده است');
    }
    else {
        if (data == "no-more-info") {
            infoNoty("اطلاعات بیشتری یافت نشد");
        }
        else {
            var $boxes = $(data);
            $(container).append($boxes);
            $(userPager).data("page", $(userPager).data("page") + 1);
            $(userPager).closest('.row').show();
        }

        $(progressId).css("display", "none");
        $("[data-toggle='tooltip']").tooltip();
    }
}



/*############### OnFailures ##############*/

function onFailure(xhr, status, error) {

}


/*################# searching and sorting ##################*/


function directSearchPaging(progressId, formId, pagerId, container) {
    var form = '#' + formId;
    $(pagerId).closest('.row').hide();
    $(progressId).css("display", "block");
    $(pagerId).data("page", 1);
    $(form).find('#PageIndex').val(1);
    $(container).html("");
    $(form).submit();
}

function doPaging(btn, progressId, formId) {
    var form = '#' + formId;
    $(btn).closest('.row').hide();
    $(progressId).css("display", "block");
    $(form).find('#PageIndex').val($(btn).data("page"));
    $(form).submit();
}

function paging(progressId, form, pagerId, sortId, sortOrderId, pageSizeId) {

    $(pagerId).closest('.row').hide();
    $(progressId).css("display", "block");
    $(form).find('#CurrentSort').val($(sortId).val());
    $(form).find('#SortDirection').val($(sortOrderId).val());
    $(form).find('#PageIndex').val($(pagerId).data("page"));
    $(form).find('#PageSize').val($(pageSizeId).val());

    $(form).submit();
}

function sorting(progressId, form, pagerId, sortId, sortOrderId, container, pageSizeId) {
    $('#'+pagerId).closest('.row').hide();
    $('#' + progressId).css("display", "block");
    $('#'+form).find('#PageIndex').val(1);
    $('#' + form).find('#CurrentSort').val($('#' + sortId).val());
    $('#' + form).find('#SortDirection').val($('#' + sortOrderId).val());
    $('#' + form).find('#PageSize').val($('#' + pageSizeId).val());
    $('#' + container).html("");
    $('#' + pagerId).data("page", 2);
    $('#' + form).submit();
}

function directSearchPagingSorting(progressId, form, pagerId, container, sortId, sortOrderId, pageSizeId) {
    $(pagerId).closest('.row').hide();
    $(progressId).css("display", "block");
    $(pagerId).data("page", 2);
    $(sortId).val($(sortId + " option:first").val());
    $(sortOrderId).val($(sortOrderId + " option:first").val());
    $(pageSizeId).val($(pageSizeId + " option:first").val());
    $(form).find('#PageIndex').val(1);
    $(form).find('#PageSize').val($(pageSizeId).val());
    $(form).find('#CurrentSort').val($(sortId).val());
    $(form).find('#SortDirection').val($(sortOrderId).val());
    $(container).html("");
    $(form).submit();
}


function resetSearch(progressId, form, pagerId, container, sortId, sortOrderId, pageSizeId) {
    $(pagerId).closest('.row').hide();
    $(progressId).css("display", "block");
    $(pagerId).data("page", 2);
    $(sortId).val($(sortId + " option:first").val());
    $(sortOrderId).val($(sortOrderId + " option:first").val());
    $(pageSizeId).val($(pageSizeId + " option:first").val());

    $(form).find("input[type=text], textarea").val("");
    $(form).find("select").each(function(key, val) {
        $(this).val($(this).find('option:first').val());
    });

    $(form).find('#PageIndex').val(1);
    $(form).find('#PageSize').val($(pageSizeId).val());
    $(form).find('#CurrentSort').val($(sortId).val());
    $(form).find('#SortDirection').val($(sortOrderId).val());
    $(form).find('#PageIndex').val(1);
    $(container).html("");
    $(form).submit();

}

function justPaging(btn) {
    var getData=function(data) {
        data.PageIndex = $(btn).data('page');
        return data;
    }
    var progress = $(btn).data('progress');
    $(btn).closest('.row').hide();
    $(progress).css("display", "block");
    $.ajax({
        type: "POST",
        url: $(btn).data('loadUrl'),
        data: JSON.stringify(getData($(btn).data('json'))),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function(xhr, status) {
            var data = xhr.responseText;
            if (xhr.status == 403) {
                window.location = '/Account/Login';
            } else if (status === 'error' || !data) {
                dangerNoty('خطایی در بارگذاری بوجود آمده است');
            } else {
                if (data == "no-more-info") {
                    infoNoty("اطلاعات بیشتری یافت نشد");
                } else {
                    var $boxes = $(data);
                    $($(btn).data('container')).append($boxes);
                    $(btn).data("page", $(btn).data("page") + 1);
                    $(btn).closest('.row').show();

                }
                $(progress).css("display", "none");
                $("[data-toggle='tooltip']").tooltip();
            }
        }
    });
}

function createEditor() {
    $('.ckeditor').each(function() {
        var instance = CKEDITOR.instances[this.attr('id')];
        if (instance) {
            CKEDITOR.remove(instance);
        }
        CKEDITOR.replace(id);
    });
}

function CKupdate() {
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
    CKEDITOR.instances[instance].setData('');
}