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
                items: 3
            },
            991: {
                items: 5
            }
        }
    });

    //menu
    $('#show-category').click(function () {
        $(".list-category").stop().slideToggle(300);
        event.stopPropagation();
    });

    $(".navbar-toggler").click(function () {
        $(".list-category").stop().slideUp(300);
    });

    $(window).click(function () {
        $(".list-category").stop().slideUp(300);
    });

    //cuộn
    var nav = $('#top-nav');

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
        var nameComic = $('#name-seach').val();
        var author = $('#author-seach').val();
        var nation = $('#nation-search').children('option:selected').val();
        var status = $('#status-search').children('option:selected').val();
        var sort = $('#sort-seach').children('option:selected').val();


        $(".category-search").each(function () {
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
                if (page == 1) {
                    $('.ajax-show').empty();
                }

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

        $('.ajax-show').empty();

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

        page = 1;

        setTimeout(getData(), 2000);

    })


    //even bộ lọc

    $('#name-seach').stop().keyup(function () {
        $('.ajax-show').empty();
        page = 1;
        setTimeout(getData(), 2000);

    });
    $('#author-seach').stop().keyup(function () {
        $('.ajax-show').empty();
        page = 1;
        setTimeout(getData(), 2000);
    });
    $('#name-seach').stop().change(function () {
        $('.ajax-show').empty();
        page = 1;
        setTimeout(getData(), 2000);
    });
    $('#status-search').stop().change(function () {
        $('.ajax-show').empty();
        page = 1;
        setTimeout(getData(), 2000);
    });
    $('#sort-seach').stop().change(function () {
        $('.ajax-show').empty();
        page = 1;
        setTimeout(getData(), 2000);
    });

    $('#page-search').click(function () {
        page++;
        setTimeout(getData(), 2000);
    })


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
        $('#top-nav').css('position', 'unset');
    }
    // text editer

    //slidetab
    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });

    //requetcomic

    $("#requestcomic").click(function () {
        Update();
    })


    //chuyển chap
    $(window).keydown(function (e) {
        if (e.which == 37) {
            $('#back').trigger('click');
        }
    })

    //nav-bottom

    $(window).scroll(function () {
        let start = $(window).scrollTop();

        setTimeout(function () {

            let end = $(window).scrollTop();

            if (start - end < 0) {
                $('#nav-bottom').slideUp(100);
            } else {
                $('#nav-bottom').slideDown(100);
            }

        }, 200)


    })


    //.list-read-chapter

    $("#menu").click(function () {
        ShowChapter();
    });

    function ShowChapter() {
        $(".list-read-chapter").stop().toggleClass('active');
    }

    //go top

    function GoTop() {
        $(window).scroll(function () {
            let e = $(window).scrollTop();
            if (e > 300) {
                $(".btn-top").show(300)
            } else {
                $(".btn-top").hide(300)
            }
        });
        $(".btn-top").click(function () {
            $('body,html').animate({
                scrollTop: 0
            })
        })
    }

    GoTop();

    //comment

    $(".btn-cmt").click(function () {

        let content = $("#comment").val();
        let id = $(this).attr("id-chapter");

        if (content.trim().length > 5) {

            $.ajax({
                url: 'User/Comment',
                type: 'POST',
                data: {content: content, chapterId: id},
                dataType: "json",
                success: function (data) {
                    if (data == "null") {
                        $.alert({
                            theme: 'modern',
                            icon: 'ion-alert-circled',
                            title: 'Lỗi',
                            content: "Đăng bình luận thất bại",
                            type: "red"
                        });
                    } else {


                        console.log(data);

                        $('.show-cmt').children().before(
                            '<div class="item-comment d-flex"> <div class="avatar"> <img src=" ' +
                            data.Avatar +
                            ' " alt=""> </div> <div class="body-comment"> <div class="title-comment"> <p>' +
                            data.Username +
                            '</p> </div> <div class="content-comment"> <p>' +
                            data.CommentConten +
                            '</p> </div> <div class="footer-comment"> <p>' +
                            data.CommentTime +
                            '</p> </div> </div> </div>');
                    }
                }

            })

        } else {
            $.alert({
                theme: 'modern',
                icon: 'ion-alert-circled',
                title: 'Lỗi',
                content: "Nội dung bình luận quá ngắn học bị để trống",
                type: "red"
            });
        }
    })


    //ẩn bình luận

    $('.block-cmt').click(function () {
        let id = $(this).attr("id-comment");
        
        var btn = $(this);
        
        $.ajax({
            url: '/User/ChangeStatusCmt',
            type: 'POST',
            data: {id: id},
            success: function (data) {

                if (data == "True") {

                    var a = btn.parents('.item-comment').removeClass("d-flex").hide(300);


                    $.alert({
                        theme: 'modern',
                        icon: 'ion-android-done',
                        autoClose: 'ok|2000',
                        title: 'Thông báo',
                        content: 'Thay đổi thành công',
                        type: 'green'
                    })
                } else {
                    $.alert({
                        theme: 'modern',
                        icon: 'ion-alert-circled',
                        autoClose: 'ok|2000',
                        title: 'Thông báo',
                        content: 'Thay đổi thất bại',
                        type: 'red'
                    })
                }

            }
        })

    })

});

