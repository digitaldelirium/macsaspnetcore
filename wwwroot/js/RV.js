(function() {
    $(document).ready(function() {
        $("#topLeft").show();
        $("#topCenter").show();
        $("#topRight").show();

        // set up Trailer A slideshow
        $("#trailer1").carousel({
            interval: 4000
        });


        // set up trailer B slideshow
        $("#trailer2").carousel({
            interval: 4000
        });

    });
}());