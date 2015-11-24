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
function showAlert() {
    var a = document.getElementsByClassName('alert')[0];
    if (a.innerText != "")
        a.style.display = "block";
}
function showSideBarActive() {
    var a = document.getElementsByClassName('nav-sidebar')[0];
    var currentIndex = a.lastElementChild.innerText[0] - '0';
    a.children[currentIndex].className += ' active';
};
showSideBarActive();
