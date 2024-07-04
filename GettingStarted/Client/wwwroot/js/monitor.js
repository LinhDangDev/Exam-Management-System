

function resetLogin(maSV, last_logged_in) {
    if (confirm(`Thí sinh đăng nhập lần cuối vào lúc ${last_logged_in}. Hãy cân nhắc thời gian trên và chắc chắn rằng sinh viên không gian lận`)) {
        fetch(`api/Admin/UpdateLogoutSinhVien?ma_sinh_vien=${maSV}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Update Logout SV failed');
                }
            })
    }
}
