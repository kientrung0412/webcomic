$(document).ready(function () {
    // var urlBg = $('.img-bg').attr("url-bg");
    // $(this).css(" background-image", "a");
    // console.log(urlBg);

    $('.img-bg').each(function () {
        var item =  $(this)
        var urlBg = item.attr("url-bg");
        console.log(urlBg);
        item.css("background-image", urlBg);
    });
})