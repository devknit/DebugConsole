var WebWindow = {
    WebWindowPlugin_OnFocus: function (cb) {
        window.addEventListener('focus', function () {
            Runtime.dynCall("v", cb, []);
        });
    },
    WebWindowPlugin_OnBlur: function (cb) {
        window.addEventListener('blur', function () {
            Runtime.dynCall("v", cb, []);
        });
    },
}

mergeInto(LibraryManager.library, WebWindow);