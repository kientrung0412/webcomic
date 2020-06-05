// Hàm xóa

function Delete(urlPost, id) {
    $.confirm({
        icon: 'ion-alert-circled',

        theme: 'modern',
        title: 'Xác thực ?',
        content: 'Bạn có chắc trắn muốn xóa',
        autoClose: 'no|5000',
        type: 'orange',
        buttons: {
            yes: {

                keys: ['y'],
                text: "Đồng ý",
                btnClass: 'btn-green',

                action: function () {

                    $.ajax({
                        url: urlPost,
                        type: 'POST',
                        data: {id: id},
                        success: function (data) {
                            if (data === "True") {
                                $.alert({
                                    icon: 'ion-android-done',
                                    autoClose: 'ok|2000',
                                    title: 'Thông báo',
                                    content: 'Xóa thành công',
                                    type: 'green'
                                })
                            } else {
                                $.alert({
                                    icon: 'ion-alert-circled',
                                    autoClose: 'ok|2000',
                                    title: 'Thông báo',
                                    content: 'Xóa thất bại',
                                    type: 'red'
                                })
                            }

                        }
                    })

                }

            },
            no: {
                keys: ['n'],
                text: "Không",
                btnClass: 'btn-red',
                action: function () {
                }
            }
        }
    });
}

function Update(urlPost, id) {
    var q;

    $.confirm({
        icon: 'ion-alert',
        columnClass: 'col-md-4 col-12',
        theme: 'modern',
        title: 'Xác thực ?',
        content: 'Bạn có chắc trắn muốn gửi yêu cầu',
        autoClose: 'no|5000',
        type: 'orange',
        buttons: {
            yes: {
                keys: ['y'],
                text: "Đồng ý",
                btnClass: 'btn-green',
                action: function () {

                    $.ajax({
                        url: urlPost,
                        type: 'POST',
                        data: {id: id},
                        success: function (data) {

                            if (data === "True") {
                                $.alert({
                                    icon: 'ion-android-done',
                                    autoClose: 'ok|2000',
                                    title: 'Thông báo',
                                    content: 'Cập nhật thành công',
                                    type: 'green'
                                })
                            } else {
                                $.alert({
                                    icon: 'ion-alert-circled',
                                    autoClose: 'ok|2000',
                                    title: 'Thông báo',
                                    content: 'Cập nhật thất bại',
                                    type: 'red'
                                })
                            }

                        }
                    })
                }
            },
            no: {
                keys: ['n'],
                text: "Không",
                btnClass: 'btn-red',
            }
        }
    });

    return q;

}

$(document).ready(function () {

    //cấp phép
    $('.btn-active-comic').click(function () {
        $(this).parents("tr").slideUp("slown").removeClass("d-flex");

        let id = $(this).attr("id-comic");
        let b = Update("/User/UpdateCensorship", id);
    });

    //xoa chuyeenj

    $('.btn-delete-comic').click(function () {
        $(this).parents("tr").slideUp("slown").removeClass("d-flex");

        let id = $(this).attr("id-comic");
        let b = Delete("/User/DeleteComic", id);
    })
})


