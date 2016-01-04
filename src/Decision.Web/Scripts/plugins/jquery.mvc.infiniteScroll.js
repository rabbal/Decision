// <![CDATA[
(function ($) {
    $.fn.infiniteScroll = function (options) {
        var defaults = {
            loginUrl: '/Account/Login',
            pageName: "صفحه"
        };
        var options = $.extend(defaults, options);
        var button = $(this);

        var moreInfoDiv = '#' + button.data('moreInfoDiv');
        var progressDiv = '#' + button.data('progressDiv');
        var loadUrl = '#' + button.data('loadUrl');
        var sortArea = '#' + button.data('sortArea');
        var searchArea = '#' + button.data('searchArea');
        var mainNonAjaxContentDiv = '#' + button.data('mainNonAjaxContentDiv');
        var clearSearchBtn = '#' + button.data('clearBtn');
        var searchBtn = '#' + button.data('searchBtn');
        var pageIndex = button.data('pageIndex');

        var showProgress = function () {
            $(progressDiv).css("display", "block");
        }

        var hideProgress = function () {
            $(progressDiv).css("display", "none");
        }

        var serializeAreas=function(page) {
            return $(sortArea + ',' + searchArea).serialize({ checkboxesAsBools: true })+"&pageIndex="+page;
        }

        var clearArea = function () {
            $(moreInfoDiv).html("");
            $(mainNonAjaxContentDiv).html("");
            window.slow(400).scrollTo(0, 0);
        }

        var updatePageIndex = function (page) {
            button.data('pageIndex', page);
        }

        var increasePageIndex = function () {
           var  page = button.data('pageIndex') + 1;
            button.data('pageIndex', page);
        }
        return this.each(function () {
            var moreInfoButton = $(this);
            var doAction = function (serializedData) {
                showProgress();
                $.ajax({
                    type: "POST",
                    url: loadUrl,
                    data: serializedData,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    complete: function (xhr, status) {
                        var data = xhr.responseText;
                        if (xhr.status == 403) {
                            window.location = options.loginUrl;
                        }
                        else if (status === 'error' || !data) {
                            dangerNoty('خطایی در بارگذاری به وجود آمده است');
                        }
                        else {
                            if (data == "no-more-info") {
                                hideProgress();
                                moreInfoButton.parent().hide();
                            }
                            else {
                                var $boxes = $(data);
                                $(moreInfoDiv).append($boxes);
                                hideProgress();
                                moreInfoButton.parent().show();
                            }
                            increasePageIndex();
                        }
                    }
                });
            }

            var searchOrClear = function () {
                $(sortArea + '> select').each(function () {
                    this.val('#' + $(this.attr('id') + " option:first").val());
                });
                doAction(serializeAreas());
            }

            $(searchBtn).click(function () {
                searchOrClear();
            });

            $(clearSearchBtn).click(function () {
                searchOrClear();
            });

            $(sortArea + "> select").change(function () {
                updatePageIndex(1);
                clearArea();
                doAction(serializeAreas());
            });

            $(moreInfoButton).click(function (event) {
                if (event.originalEvent === undefined) {
                    // triggered by code
                    updatePageIndex(1);
                }
                doAction(serializeAreas());
            });
        });
    };
})(jQuery);
// ]]>