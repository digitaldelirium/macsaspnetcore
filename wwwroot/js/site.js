// Write your Javascript code.
function createIframe(parentId) {
    $(parentId).append("<div class='col-md-12 col-lg-12' id='iframeDiv'><iframe  id='generatedIframe'></iframe></div>");
}

var collapse = (function () {
    $(document).ready(function () {
        if ($(document).width > 768 && $(document).width < 992){
            $(".collapse").collapse("hide");
        }
    });



})();