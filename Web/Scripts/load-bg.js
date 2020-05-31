$('.img-bg').each(function () {
    var urlBg = $(this).attr("url-bg");
    var style = {
        backgroundImage: "url(" + urlBg + ")"
    }
    $(this).css(style)
});


