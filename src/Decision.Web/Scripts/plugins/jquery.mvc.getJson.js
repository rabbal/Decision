
$(function () {
    $(document).on('change', 'select.cascade', function () {
        var $select = $(this);
        jQuery.each($select.data('selects').split(','), function () {
            $('#' + this).empty().append('<option value>' + $select.data('lable') + '</option>');
        });
        var tofill = $select.data('tofill');
        if ($select.val() == '') return;
        $.ajaxSetup({ cache: false });
        $.getJSON($select.data('url'), { id: $select.val() }, function (data) {
            jQuery.each(data, function (i) {
                var option = $('<option></option>').attr("value", data[i].Value).text(data[i].Text);
                $('#' + tofill).append(option);
            });
        });
    });

});






