(function () {

    window.fbAsyncInit = function () {
        FB.init({
            appId: '732145460227308',
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

    $("#titleCarousel").slick({
        infinite: true,
        autoplay: true,
        arrows: true,
        appendArrows: "#titleCarousel",
        appendDots: "#titleCarousel",
        autoplaySpeed: 3000,
        dots: true,
        dotsClass: "slick-dots",
        fade: true,
        lazyLoad: "ondemand",
        pauseOnHover: true,
        waitForAnimate: true,
        prevArrow: "prevSlide",
        nextArrow: "nextSlide",
        slidesToShow: 1,
        slidesToScroll: 1
});

    $("#titleCarousel").css({ maxHeight: jumboContainer + "px" });

    $("img").css("height: " + jumboContainer);
});

})();

