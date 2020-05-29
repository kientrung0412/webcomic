$(document).ready(function () {
    //slide
    $(".owl-carousel").owlCarousel({
        loop: true,
        autoplay: true,
        margin: 10,
        autohide: true,
        responsive: {
            0: {
                items: 2
            },
            575: {
                items: 4
            },
            991: {
                items: 5
            }
        }
    });

    //menu
    $('#show-category').click(function () {
        $(".list-category").slideToggle("slow");
        event.stopPropagation();
    });

    $(".navbar-toggler").click(function () {
        $(".list-category").slideUp("fast");
    });

    $(window).click(function () {
        $(".list-category").slideUp("slow");
    });

    //cuộn
    var nav = $('.navbar');

    $(document).scroll(function () {
        var scrollTop = $(window).scrollTop();

        if (scrollTop > 0) {
            nav.addClass("p-fix").delay("slow");
        } else {
            nav.removeClass("p-fix").delay("slow");
        }

    })

    //tooltip
    var item = $('.comic-item');

    // item.hover(function () {
    //
    //     var id = $(this).attr("id-comic");
    //
    //     tippy('.comic-item', {
    //         content: $(this).attr("id-comic"),
    //         arrowStyles: 'narrow',
    //         placement: "right",
    //         allowHTML: true,
    //         maxWidth: 240,
    //     })
    //
    //    
    // })


    
    
    
});

