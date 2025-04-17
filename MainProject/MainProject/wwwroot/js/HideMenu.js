window.registerOutsideClick = function (elementId, dotNetHelper) {
    function onClick(event) {
        const el = document.getElementById(elementId);
        if (el && !el.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('HidePopup');
            document.removeEventListener('click', onClick);
        }
    }

    setTimeout(() => {
        document.addEventListener('click', onClick);
    }, 0);
}