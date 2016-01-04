var urlParams;
(window.onpopstate = function () {
    var match,
        pl = /\+/g,  // Regex for replacing addition symbol with a space
        search = /([^&=]+)=?([^&]*)/g,
        decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); },
        query = window.location.search.substring(1);

    urlParams = {};
    while (match === search.exec(query))
        urlParams[decode(match[1])] = decode(match[2]);
})();


function jsonConcat(defaults, options) {
    /* merge defaults and options, without modifying defaults */
    return $.extend({}, defaults, options);
}


var ajaxData = { items: items, surveyId: surveyId };
$.ajax({
    type: "POST",
    url: "@sortUrl",
    data: JSON.stringify(jsonConcat(ajaxData, urlParams))
});