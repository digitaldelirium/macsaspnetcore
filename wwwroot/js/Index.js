﻿(function() {

    window.fbAsyncInit = function() {
        FB.init({
            appId: "101842023547502",
            xfbml: true,
            version: "v2.3"
        });
    };

    (function(d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
            return;
        }
        js = d.createElement(s);
        js.id = id;
        js.src = "//connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, "script", "facebook-jssdk"));

    $(document).ready(function() {
        $("#titleCarousel").carousel({
            interval: 3000
        });
    });
    
    const config = {
        rootMargin: '0px 0px 50px 0px',
        threshold: 0
    };
    
    let observer = new IntersectionObserver(function (entries) { 
        entries.forEach( entry => {
            if(entry.isIntersecting) {
                preloadImage(entry.target);
                self.unobserve(entry.target);
            }
        })
    }, config);
    
    const imgs = document.querySelectorAll('[data-src]');
    imgs.forEach(img => {
        observer.observe(img)
    });
})();