(function() {
    $(document).ready(function() {
        $("#topLeft").show();
        $("#topCenter").show();
        $("#topRight").show();

        // set up Lakeview slideshow
        $("#lakeview").carousel({
            interval: 4000
        });


        // set up Lilypad slideshow
        $("#lilypad").carousel({
            interval: 4000
        });
        const config = {
            rootMargin: '0px',
            threshold: 0
        };

        let observer = new IntersectionObserver(function (entries, self) {
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

        function preloadImage(img) {
            const src = img.getAttribute('data-src');
            if (!src) { return; }
            img.src = src;
        }
    });
}());