(function ($) {
    $.fn.postFormAjax = function (options) {
        var defaults = {
            postUrl: '/',
            loginUrl: '/Account/Login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null
        };
        var options = $.extend(defaults, options);

        var validateForm = function (form) {
            //فعال سازی دستی اعتبار سنجی جی‌کوئری
            var val = form.validate();
            val.form();
            return val.valid();
        };

        return this.each(function () {
            var form = $(this);
            //اگر فرم اعتبار سنجی نشده، اطلاعات آن ارسال نشود
            if (!validateForm(form)) return;

            //در اینجا می‌توان مثلا دکمه‌ای را غیرفعال کرد
            if (options.beforePostHandler)
                options.beforePostHandler(this);

           

            //اطلاعات نباید کش شوند
            $.ajaxSetup({ cache: false });

            $.ajax({
                type: "POST",
                url: options.postUrl,
                data: form.serialize(), //تمام فیلدهای فرم منجمله آنتی فرجری توکن آن‌را ارسال می‌کند
                complete: function (xhr, status) {
                    var data = xhr.responseText;
                    if (xhr.status == 403) {
                        window.location = options.loginUrl; //در حالت لاگین نبودن شخص اجرا می‌شود
                    }
                    else if (status === 'error' || !data) {
                        if (options.errorHandler)
                            options.errorHandler(this);
                    }
                    else {
                        if (options.completeHandler)
                            options.completeHandler(this);
                    }
                }

            });
        });
    };
})(jQuery);