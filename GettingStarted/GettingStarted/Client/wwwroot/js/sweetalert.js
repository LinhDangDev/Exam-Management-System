// sweet alert
function alertNormal(textMessage) {
    var text = textMessage;
    var ketQua = false;
    Swal.fire(text).then(() => {
        ketQua = true;
    });
    return ketQua;
}
function alertError(textMessage) {
    Swal.fire({
        icon: "error",
        title: "Xin lỗi...",
        text: textMessage,
        customClass: {
            popup: "custom-sweetalert",
        }
    });
    var swalPopup = document.querySelector(".swal2-popup");
    swalPopup.style.fontFamily = "Segoe UI-Bold", Helvetica;
    swalPopup.style.fontSize = "2rem";
}
function alertSave(textMessage) {
    Swal.fire({
        position: "top-end",
        icon: "success",
        title: textMessage,
        showConfirmButton: false,
        timer: 1500,
        customClass: {
            popup: "custom-sweetalert",
        }
    });
}
function alertConfirmExam() {
    let ketQua = false;
    const swalWithBootstrapButtons = Swal.mixin({
        customClass:
        {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Bạn có chắc chắn muốn nộp bài?",
        text: "Bạn không thể quay lại được!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true,
        customClass: {
            popup: "custom-sweetalert",
        }
    }).then((result) => {
        if (result.isConfirmed) {
            ketQua = true;
            swalWithBootstrapButtons.fire({
                title: "Đã nộp bài!",
                text: "Bài của bạn nộp thành công.",
                icon: "success"
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            ketQua = false;
            swalWithBootstrapButtons.fire({
                title: "Đã hủy bỏ",
                text: "Bài của bạn nộp không thành công",
                icon: "error"
            });
        }
    });
    return ketQua;
}

function alertConfirmDangXuat() {
    let ketQua = false;
    const swalWithBootstrapButtons = Swal.mixin({
        customClass:
        {
            confirmButton: "btn btn-success",
            cancelButton: "btn btn-danger"
        },
        buttonsStyling: false
    });
    swalWithBootstrapButtons.fire({
        title: "Bạn có chắc chắn muốn đăng xuất?",
        text: "Bạn không thể quay lại được!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes!",
        cancelButtonText: "No, cancel!",
        reverseButtons: true,
        customClass: {
            popup: "custom-sweetalert",
        }
    }).then((result) => {
        if (result.isConfirmed) {
            ketQua = true;
            swalWithBootstrapButtons.fire({
                title: "Đăng xuất thành công!",
                text: "Tài khoản đăng xuất thành công.",
                icon: "success"
            });
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            ketQua = false;
            swalWithBootstrapButtons.fire({
                title: "Đã hủy bỏ",
                text: "Tài khoản đăng xuất không thành công",
                icon: "error"
            });
        }
    });
    return ketQua;
}
