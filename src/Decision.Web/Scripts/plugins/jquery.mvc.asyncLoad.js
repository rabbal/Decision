
(function ($) {
    $.fn.asyncLoad = function (options) {
        var defaults = {
            loginUrl: '/Account/Login',
            beforePostHandler: null,
            completeHandler: null,
            errorHandler: null,
        };
        var options = $.extend(defaults, options);
        var sectionId = '#' + $(this).attr('id');
        var postUrl = $(this).data('loadUrl');
        var progressDiv ='#' +$(this).data('progressDiv');
        var showProgress = function () {
            $(progressDiv).css("display", "block");
        };

        var hideProgress = function () {
            $(progressDiv).css("display", "none");
        };

        var row = this;
        return this.each(function () {
            //در اینجا می‌توان مثلا دکمه‌ای را غیرفعال کرد
            if (options.beforePostHandler) {
                options.beforePostHandler(this);
            }
            showProgress();
            //اطلاعات نباید کش شوند
            $.ajaxSetup({ cache: false });
            $.ajax({
                type: "POST",
                url: postUrl,
                complete: function (xhr, status) {
                    var data = xhr.responseText;
                    if (xhr.status == 403) {
                        window.location = options.loginUrl;
                    } else if (status === 'error' || !data || data == "nok") {
                        dangerNoty('خطایی در بارگذاری به وجود آمده است . لطفا با بخش فنی در میان بگذارید');
                    } else {
                        hideProgress();
                        $(sectionId).html(data);
                    }
                }
            });
        });

    }
})(jQuery);