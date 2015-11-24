function addLoadEvent(func) {
    var oldonload = window.onload;
    if (typeof oldonload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload;
            func();
        }
    }
};
function hideAlert() {
    var a = document.getElementsByClassName('alert')[0];
    if (a.innerText != "")
        a.style.display = "block";
}
hideAlert();