(function ($) {
    $.fn.deleteItem = function (options) {
        var defaults = {
            postUrl: '/',
            loginUrl: '/Account/Login'
        };
        options = $.extend(defaults, options);

        var button = this;
        var type = "error";
        var doneMsg = "اطلاعات با موفقیت از پایگاه داده حذف شد";
        var msg = "آیا از حذف رکورد مورد نظر مطمئن هستید؟";
        if (button.data('message') !== undefined)
            msg = button.data('message');
        if (button.data('type') !== undefined)
            type = button.data('type');
        if (button.data('doneMsg') !== undefined)
            doneMsg = button.data('doneMsg');
        function addToken(data) {
            if ($(button).data("applicant") !== undefined)
                data.applicantId = $(button).data("applicant");
            data.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
            return data;
        }

        var item = $(button).data('removalElement');
        var id = button.attr('id').replace('remove-', '');

        return this.each(function () {
                noty({
                    text: msg,
                    type: type,
                    theme: 'relax',
                    layout: 'center',
                    killer:true,
                    modal: true,
                    dismissQueue: false,
                    buttons: [
                        {
                            addClass: 'btn btn-primary',
                            text: 'بله موافقم',
                            onClick: function ($noty) {
                                //اطلاعات نباید کش شوند
                                $.ajaxSetup({ cache: false });

                                $.ajax({
                                    type: "POST",
                                    url: options.postUrl,
                                    data: addToken({ id: id }), // اضافه کردن توکن
                                    dataType: "html", // نوع داده مهم است
                                    complete: function (xhr, status) {
                                        var data = xhr.responseText;
                                        if (xhr.status == 403) {
                                            window.location = options.loginUrl;
                                        }
                                        else if (xhr.status == 400) {
                                            dangerNoty("داده ارسالی شما معتبر نیست");
                                        }
                                        else if (data == "system") {
                                            warningNoty("رکورد مورد نظر از پیش فرض های سیستم میباشد و امکان ایجاد تغییرات برای آن وجود ندارد");
                                        }
                                        else if (status === 'error' || !data || data == "nok") {
                                            dangerNoty("مشکلی در انجام عملیات به وجود آمده است");
                                        }
                                        else if (data == "ok") {
                                            $noty.close();
                                            $(item).slideUp('slow').remove();
                                            infoNoty(doneMsg);
                                        }
                                    }

                                });
                                $noty.close();
                            }
                        },
                        {
                            addClass: 'btn btn-default',
                            text: 'انصراف',
                            onClick: function ($noty) {
                                $noty.close();
                            }
                        }
                    ]
                });
     

        });

    }
})(jQuery);


