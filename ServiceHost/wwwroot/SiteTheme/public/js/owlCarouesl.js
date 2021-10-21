// ! owl carousel
$('.owl-carousel').owlCarousel({
    rtl: true,
    nav: true,
    lazyLoad: true,
    autoplay: true,
    autoplayTimeout: 2000,
    loop: true,
    dots: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1024: {
            items: 2
        },
        1025: {
            items: 3
        }
    }
})
// ! owl carousel