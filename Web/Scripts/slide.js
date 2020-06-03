﻿$(document).ready(function () {

    LoadBg();

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
    var page = 1;

    function getData() {

        var listIn = new Array();
        var listNotIn = new Array();
        var nameComic = $('input[name = name]').val();
        var author = $('input[name = author]').val();
        var nation = $('select[name = nation]').children('option:selected').val();
        var status = $('select[name = status]').children('option:selected').val();
        var sort = $('select[name = sort]').children('option:selected').val();


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
                nation: nation,
                sort: sort,
                page: page
            },
            success: function (data) {
                var urlNew = this.url;
                urlNew = urlNew.replace("Test?", "SearchAdvanced?")
                history.pushState('', 'Search', urlNew);

                // console.log(data);

                url = $(this).attr('href');
                history.pushState({key: url}, '', url);

                if (data.PageSize - 1 < page) {
                    $('.more-comic').hide("slow");
                } else {
                    $('.more-comic').show("slow");
                }

                // $('.pagination').children().remove();
                //
                //
                // if (page > 1) {
                //     $('.pagination').append(
                //         '<li class="page-item">' +
                //         '<a class="page-link page-search" page="' +
                //         (page - 1) + '" tabindex="-1" aria-disabled="true"  >' +
                //         '<i class="ion-chevron-left"></i>' +
                //         '</a>' +
                //         '</li>'
                //     );
                // }
                //
                // for (i = 1; i <= data.PageSize; i++) {
                //
                //     if (i == page) {
                //         $('.pagination').append(
                //             '<li class="page-item active">' +
                //             '<a class="page-link page-search" page="' + i + '" tabindex="-1">' +
                //             i +
                //             '</a>' +
                //             '</li>'
                //         );
                //     } else {
                //         $('.pagination').append(
                //             '<li class="page-item">' +
                //             '<a class="page-link page-search" page="' + i + '" tabindex="-1" aria-disabled="true">' +
                //             i +
                //             '</a>' +
                //             '</li>'
                //         );
                //     }
                // }
                //
                // if (page < data.PageSize) {
                //     $('.pagination').append(
                //         '<li class="page-item">' +
                //         '<a class="page-link page-search" href="' + (page + 1) + '" tabindex="-1" aria-disabled="true">' +
                //         '<i class="ion-chevron-right"></i>' +
                //         '</a>' +
                //         '</li>'
                //     );
                // }

                $(data.Comics).each(function (index, comic) {

                    var newChapter = "";

                    if (comic.chapters.length > 0) {
                        var chapter = comic.chapters[comic.chapters.length - 1];
                        var ChapterId = chapter.ChapterId;
                        var NameChapter = chapter.NameChapter;

                        newChapter = '<div class="new-chapter"><a href="/Comic?comicId=1">' +
                            '</a><a href="/Chapter?chapterId=' + ChapterId + '">' + NameChapter + '</a>' +
                            '</div>';
                    }

                    $('.ajax-show').append(
                        '<div class="col-sm-3 col-6">' +
                        '<div class="comic-item">' +
                        '<a href="/Comic?comicId=' + comic.ComicId + '">' +
                        '<div class="item-main">' +
                        '<div class="img-cover">' +
                        '<div class="img-bg bg-img-full" url-bg="' + comic.CommicBanner + '" ></div>' +
                        '</div>' +
                        newChapter +
                        '</div>' +
                        '</a>' +
                        '<div class="name-comic text-light">' +
                        '<a href="/Comic?comicId=10">' + comic.NameComic + '</a>' +
                        '</div>' +
                        '</div>' +
                        '</div>'
                    );


                })
                LoadBg();
            }
        })


    }

    //check box 3 status


    var check = $('.check');

    check.change(function () {

        $('.ajax-show').children().remove();

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


    //even bộ lọc

    $('input[name = name]').stop().keyup(function () {
        $('.ajax-show').children().remove();
        page = 1;
        getData();

    });
    $('input[name = author]').stop().keyup(function () {
        $('.ajax-show').children().remove();
        page = 1;
        getData();
    });
    $('select[name = nation]').stop().change(function () {
        $('.ajax-show').children().remove();
        page = 1;
        getData();
    });
    $('select[name = status]').stop().change(function () {
        $('.ajax-show').children().remove();
        page = 1;
        getData();
    });
    $('select[name = sort]').stop().change(function () {
        $('.ajax-show').children().remove();
        page = 1;
        getData();
    });

    $('#page-search').click(function () {
        page++;
        getData();
    })

    // $('.page-search').click(function () {
    //     page = $(this).attr('page');
    //     console.log(page);
    //     getData();
    // })


    //select sort search

    function selectOption() {
        var sortAc = $('select[name = sort]').attr("select");
        $('select[name = sort]').children("option").each(function () {
            if (sortAc == $(this).val()) {
                $(this).attr("selected", "selected");
            }
        })
    }

    selectOption();

    //more

    $('.more').click(function () {
        $('.more').siblings('.form-search').stop().toggleClass("max-h-370");
        $('.more').remove();
    })

    //un fix nav

    if ($('#read-chapter').hasClass('ok')) {
        $('nav').css('position', 'unset');
    }
    // text editer


});

