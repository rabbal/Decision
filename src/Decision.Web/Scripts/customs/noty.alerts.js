/*##############       success alerts   ############*/
var successNoty = function (text) {
    var customDefaults = {
        layout: 'center',
        theme:'relax',
        type: 'success',
        text: text,
        dismissQueue: false,
        animation: {
            open: 'animated zoomIn',
            close: 'animated bounceOut',// or Animate.css class names like: 'animated bounceOutLeft'
            easing: 'swing',
            speed: 2000 // opening & closing animation speed
        },
        timeout: true, // delay for closing event. Set false for sticky notifications
        force: true, // adds notification to the beginning of queue when set to true
        killer: false, // for close all notifications before show
        closeWith: ['backdrop'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications

        buttons: false // an array of buttons
    };
    noty(customDefaults);
}

/*################# Info alerts ##################*/

var infoNotyModal= function (text) {
    var customDefaults = {
        layout: 'center',
        theme: 'relax',
        dismissQueue: false,
        type: 'information',
        modal: true,
        text: text,
        timeout: true, // delay for closing event. Set false for sticky notifications
        force: true, // adds notification to the beginning of queue when set to true
        killer: true, // for close all notifications before show
        closeWith: ['backdrop'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications

        buttons: false // an array of buttons
    };
    noty(customDefaults);
}
var infoNoty = function (text) {
    var customDefaults = {
        layout: 'center',
        theme: 'relax',
        dismissQueue: false,
        type: 'information',
        text: text,
        timeout: 2000, // delay for closing event. Set false for sticky notifications
        force: true, // adds notification to the beginning of queue when set to true
        killer: true, // for close all notifications before show
        closeWith: ['backdrop'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications

        buttons: false // an array of buttons
    };
    noty(customDefaults);
}

/*###############    error alerts    #################*/
var dangerNoty = function (text) {
    var customDefaults = {
        layout: 'center',
        type: 'error',
        dismissQueue: false,
        theme:'relax',
        text: text,
        timeout: 2000, // delay for closing event. Set false for sticky notifications
        force: true, // adds notification to the beginning of queue when set to true
        killer: true, // for close all notifications before show
        closeWith: ['backdrop'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications

        buttons: false // an array of buttons
    };
    noty(customDefaults);
}

/*#################     confirm alerts      ################*/

function confirmNoty(text) {
    noty({
        text: text,
        type: 'error',
        theme: 'relax',
        modal: false,
        dismissQueue: false,
        buttons: [
            {
                addClass: 'btn btn-success',
                text: 'بله',
                onClick: function($noty) {
                    $noty.close();
                    noty({
                        force: true, closeWith: ['backdrop'], layout: 'center', theme: 'relax', timeout: 2000, text: 'عملیات با موفقیت انجام شد', type: 'information'
                    });
                }
            },
            {
                addClass: 'btn btn-default',
                text: 'انصراف',
                onClick: function($noty) {
                    $noty.close();
                }
            }
        ]
    });
}


/*###################       warning alerts      ##########################*/


var warningNoty = function (text) {
    var customDefaults = {
        layout: 'center',
        type: 'warning',
        theme: 'relax',
        text: text,
        dismissQueue: false,
        timeout: 2000, // delay for closing event. Set false for sticky notifications
        force: true, // adds notification to the beginning of queue when set to true
        killer: false, // for close all notifications before show
        closeWith: ['backdrop'], // ['click', 'button', 'hover', 'backdrop'] // backdrop click will close all notifications

        buttons: false // an array of buttons
    };
    noty(customDefaults);
}