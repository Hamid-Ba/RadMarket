// ! owl carousel
$('.owl-carousel').owlCarousel({
    rtl: true,
    nav: true,
    lazyLoad: true,
    autoplay: true,
    autoplayTimeout: 4000,
    loop: true,
    dots: false,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 2
        },
        1024: {
            items: 3
        },
        1025: {
            items: 4
        }
    }
})
// ! owl carousel