var scanUrl = "http://localhost:8000/Api/Scanner/GetScan";
$(function () {
    $(document).on('click', '.scan', function () {
        //alert noty info
        infoNotyModal("در حال ارسال اطلاعات برای اسکن");
        var button = $(this);
        var container = '#' + $(this).data('container');
        $.ajaxSetup({ cache: false });
        $.get(scanUrl, { type: button.data('type') }, function(data) {
            $(container).val(data);
            button.removeClass('btn-prtimary scan').addClass('btn-danger  scan-remove').html(" <i class=\"fa fa-remove\"></i> حذف فایل اسکن شده ");
            return;
        });
        warningNoty("امکان اتصال به اسکنر وجود ندارد");
    });

    $(document).on('click', '.scan-remove', function () {
        var button = $(this);
        $('#' + $(this).data('container')).val('');
        button.removeClass('btn-danger scan-remove').addClass('btn-default scan').html(" <i class=\"fa fa-file-photo-o\"></i> افزودن اسکن");
    });
});
