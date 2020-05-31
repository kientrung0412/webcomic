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

    //get data
    function getData() {
        var listIn = new Array();
        var listNotIn = new Array();
        
        $("input[name = category]").each(function () {
            var idCategorey = $(this).val();
            var status = $(this).attr("data-check");

            switch (status) {
                case "1":
                    listIn.push(idCategorey);
                    break
                case "-1":
                    listNotIn.push(idCategorey)
                    break
            }

        })

        //ajax to ComicController
        
        
    }


    //check box 3 status

    var check = $('.check');

    check.click(function () {

        var dataCheck = $(this).children("input").attr("data-check");

        switch (dataCheck) {
            case "0":
                $(this).children("input").attr("data-check", "1");
                break
            case "1":
                $(this).children("input").attr("data-check", "-1");
                break
            case "-1":
                $(this).children("input").attr("data-check", "0");
                break

        }

        getData();

    })


});

