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
        $(".list-category").stop().slideToggle("slow");
        event.stopPropagation();
    });

    $(".navbar-toggler").click(function () {
        $(".list-category").stop().slideUp("fast");
    });

    $(window).click(function () {
        $(".list-category").stop().slideUp("slow");
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
        
        var nameComic = $('input[name = name]').val();
        var author = $('input[name = author]').val();
        var nation = $('select[name = nation]').children('option:selected').val();
        var status = $('select[name = status]').children('option:selected').val();

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

        $.ajax({
            url: "/Comic/Test",
            method: "GET",
            dataType: "JSON",
            traditional: true,
            data: {
                arrayIn: listIn,
                arrayNotIn: listNotIn,
                nameComic: nameComic,
                status: status,
                author: author,
                nation: nation
            },
            success: function (data) {
                console.log(data);
            }
        })


    }

    //check box 3 status


    var check = $('.check');

    check.change(function () {

        var dataCheck = $(this).children("input").attr("data-check");

        switch (dataCheck) {
            case "0":
                $(this).children("input").attr("data-check", "1");
                break;
            case "1":
                $(this).children("input").attr("data-check", "-1");
                break;
            case "-1":
                $(this).children("input").attr("data-check", "0");
                break;

        }

        getData();

    })


    //more

    $('.more').click(function () {
        $('.more').siblings('.form-search').stop().toggleClass("max-h-370");
        $('.more').remove();
    })

    //un fix nav

    if ($('completed') != null) {
        $('nav').css('position', 'unset');
    }
    console.log($('completed'));


    // text editer


});

