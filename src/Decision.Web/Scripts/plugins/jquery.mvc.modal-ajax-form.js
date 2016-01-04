// <![CDATA[
(function ($) {
    $.bootstrapModalAjaxForm = function (options) {
        var defaults = {
            renderModalPartialViewUrl: null,
            renderModalPartialViewData: null,
            postUrl: '/',
            loginUrl: '/login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null
        };
        var options = $.extend(defaults, options);

        var validateForm = function (form) {
            //فعال سازي دستي اعتبار سنجي جي‌كوئري
            var val = form.validate();
            val.form();
            return val.valid();
        };

        var enableBootstrapStyleValidation = function () {
            $.validator.setDefaults({
                highlight: function (element, errorClass, validClass) {
                    if (element.type === 'radio') {
                        this.findByName(element.name).addClass(errorClass).removeClass(validClass);
                    } else {
                        $(element).addClass(errorClass).removeClass(validClass);
                        $(element).closest('.control-group').removeClass('success').addClass('error');
                    }
                    $(element).trigger('highlited');
                },
                unhighlight: function (element, errorClass, validClass) {
                    if (element.type === 'radio') {
                        this.findByName(element.name).removeClass(errorClass).addClass(validClass);
                    } else {
                        $(element).removeClass(errorClass).addClass(validClass);
                        $(element).closest('.control-group').removeClass('error').addClass('success');
                    }
                    $(element).trigger('unhighlited');
                }
            });
        }

        var enablePostbackValidation = function () {
            $('form').each(function () {
                $(this).find('div.control-group').each(function () {
                    if ($(this).find('span.field-validation-error').length > 0) {
                        $(this).addClass('error');
                    }
                });
            });
        }

        var processAjaxForm = function (dialog) {
            $('form', dialog).submit(function (e) {
                e.preventDefault();

                if (!validateForm($(this))) {
                    //اگر فرم اعتبار سنجي نشده، اطلاعات آن ارسال نشود
                    return false;
                }

                //در اينجا مي‌توان مثلا دكمه‌اي را غيرفعال كرد
                if (options.beforePostHandler)
                    options.beforePostHandler();

                //اطلاعات نبايد كش شوند
                $.ajaxSetup({ cache: false });
                $.ajax({
                    url: options.postUrl,
                    type: "POST",
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#dialogDiv').modal('hide');
                            if (options.completeHandler)
                                options.completeHandler();
                        } else {
                            $('#dialogContent').html(result);
                            if (options.errorHandler)
                                options.errorHandler();
                        }
                    }
                });
                return false;
            });
        };

        var mainContainer = "<div id='dialogDiv' class='modal hide fade in'><div id='dialogContent'></div></div>";
        enableBootstrapStyleValidation(); //اعمال نكات خاص بوت استرپ جهت اعتبارسنجي يكپارچه با آن
        $.ajaxSetup({ cache: false });
        $.ajax({
            type: "POST",
            url: options.renderModalPartialViewUrl,
            data: options.renderModalPartialViewData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function (xhr, status) {
                var data = xhr.responseText;
                var data = xhr.responseText;
                if (xhr.status == 403) {
                    window.location = options.loginUrl; //در حالت لاگين نبودن شخص اجرا مي‌شود
                }
                else if (status === 'error' || !data) {
                    if (options.errorHandler)
                        options.errorHandler();
                }
                else {
                    var dialogContainer = "#dialogDiv";
                    $(dialogContainer).remove();
                    $(mainContainer).appendTo('body');

                    $('#dialogContent').html(data); // دريافت پوياي اطلاعات مودال ديالوگ
                    $.validator.unobtrusive.parse("#dialogContent"); // فعال سازي اعتبارسنجي فرمي كه با ايجكس بارگذاري شده                            
                    enablePostbackValidation();
                    // و سپس نمايش آن به صورت مودال
                    $('#dialogDiv').modal({
                        backdrop: 'static', //با كليك كاربر روي صفحه، صفحه مودال بسته نمي‌شود
                        keyboard: true
                    }, 'show');
                    // تحت نظر قرار دادن اين فرم اضافه شده
                    processAjaxForm('#dialogContent');
                }
            }
        });
    };
})(jQuery);
// ]]>