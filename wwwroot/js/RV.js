(function () {
    $(document).ready(function() {
        $("#topLeft").show();
        $("#topCenter").show();
        $("#topRight").show();

        // set up Trailer A slideshow
        $("#trailer1").slick({
            // Trailer 1 options
            infinite: true,
            autoplay: true,
            arrows: true,
            appendArrows: "#t1Container",
            autoplaySpeed: 3000,
            dots: true,
            dotsClass: "slick-dots",
            fade: true,
            lazyLoad: "ondemand",
            pauseOnHover: true,
            waitForAnimate: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });

        // set up trailer B slideshow
        $("#trailer2").slick({
            infinite: true,
            autoplay: true,
            arrows: true,
            appendArrows: "#t2Container",
            autoplaySpeed: 3000,
            dots: true,
            dotsClass: "slick-dots",
            fade: true,
            lazyLoad: "ondemand",
            pauseOnHover: true,
            waitForAnimate: true,
            slidesToShow: 1,
            slidesToScroll: 1
        });

    });
}());