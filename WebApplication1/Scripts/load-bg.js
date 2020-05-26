$('.img-bg').each( async function () {
    var urlBg = await $(this).attr("url-bg");
    var style = {
        backgroundImage: "url(" + urlBg + ")"
    }
    await $(this).css(style)
});