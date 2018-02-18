(function () {

    window.fbAsyncInit = function () {
        FB.init({
            appId: '101842023547502',
            xfbml: true,
            version: 'v2.3'
        });
    };

    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement(s);
        js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

$(document).ready(function () {

    var pageHeight = $(window).height();
    var jumboContainer = $("#titleContent").height();
    carouselHeight = pageHeight * .40;
    
    function playcarousel() {
        $('.carousel-control.right').trigger("click");
    }
    
    window.setInterval(playcarousel, 5000)
    

    $("#titleCarousel").css({ maxHeight: jumboContainer + "px" });

    $("img").css("height: " + jumboContainer);
});

})();

