//(function () {


    $(document).ready(function () {
        // Hide Top panes as this uses an alt view
        $("#topLeft").hide();
        $("#topCenter").hide();
        $("#topRight").hide();

        //Initiate Slick slide show
    $("#macCarousel").slick({
        infinite: true,
        autoplay: true,
        arrows: true,
        appendArrows: "#macCarousel",
        appendDots: "#macCarousel",
        autoplaySpeed: 3000,
        dots: true,
        dotsClass: "slick-dots",
        fade: true,
        lazyLoad: "ondemand",
        pauseOnHover: true,
        waitForAnimate: true,
        prevArrow: "#prevSlide",
        nextArrow: "#nextSlide",
        slidesToShow: 1,
        slidesToScroll: 1
});


    });


//})();