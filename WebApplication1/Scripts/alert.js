// Hàm xóa

function BtnDelete(classBtn, urlPost) {

    classBtn.on('click', function () {

        var id = $(this).attr('row-id');

        $.confirm({
            icon: 'fas fa-exclamation-triangle',
            boxWidth: '20%',
            useBootstrap: false,
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
                            data: {id: id, action: 'delete'},
                            success: function (data) {
                                console.log(data);
                                if (data === "true") {
                                    $.alert({
                                        icon: 'fas fa-check-circle',
                                        boxWidth: '20%',
                                        useBootstrap: false,
                                        autoClose: 'ok|2000',
                                        title: 'Thông báo',
                                        content: 'Xóa thành công',
                                        type: 'blue'
                                    })
                                } else {
                                    $.alert({
                                        icon: 'fas fa-times-circle',
                                        boxWidth: '20%',
                                        useBootstrap: false,
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

    });
}

var a = $('.a');

BtnDelete(a, "/Category/Delete");


