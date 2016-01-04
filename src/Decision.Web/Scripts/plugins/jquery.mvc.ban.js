(function ($) {
    $.fn.banned = function (options) {
        var defaults = {
            postUrl: '/',
            loginUrl: '/Account/Login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null
        };
        options = $.extend(defaults, options);

        function addToken(data) {
            data.__RequestVerificationToken = $("input[name=__RequestVerificationToken]").val();
            return data;
        }
        
        var span = this;
        span.attr('disabled', 'disabled');
        var id = span.attr('id').replace('ban-', '');
        var refreshId = '#' + span.data('refreshBtn');
        return this.each(function () {
            //در اینجا می‌توان مثلا دکمه‌ای را غیرفعال کرد
            if (options.beforePostHandler)
                options.beforePostHandler(this);
            //اطلاعات نباید کش شوند
            $.ajaxSetup({ cache: false });
            span.removeAttr('disabled');
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
                    else if (data == "banned") {
                        infoNoty("حساب کاربر مورد نظر مسدود شد");
                        $(refreshId).trigger('click');
                    }
                    else if (data == "enabled") {
                        infoNoty("حساب کاربر مورد نظر فعال شد");
                        $(refreshId).trigger('click');
                    }
                }

            });

        });

    }
})(jQuery);


