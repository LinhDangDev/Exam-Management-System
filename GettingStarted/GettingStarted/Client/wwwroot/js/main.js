// Toast function
function toast({ title = "", message = "", type = "info", duration = 500, callback }) {
    const main = document.getElementById("toast");
    if (main) {
        const toast = document.createElement("div");

        // Auto remove toast
        const autoRemoveId = setTimeout(function () {
            main.removeChild(toast);
            if (typeof callback === 'function') {
                callback(); // Gọi callback sau khi toast đã biến mất
            }
        }, duration + 1000);

        // Remove toast when clicked
        toast.onclick = function (e) {
            if (e.target.closest(".toast__close")) {
                main.removeChild(toast);
                clearTimeout(autoRemoveId);
                if (typeof callback === 'function') {
                    callback(); // Gọi callback nếu có khi toast bị đóng
                }
            }
        };

        const icons = {
            success: "fas fa-check-circle",
            info: "fas fa-info-circle",
            warning: "fas fa-exclamation-circle",
            error: "fas fa-exclamation-circle",
        };
        const icon = icons[type];
        const delay = (duration / 1000).toFixed(2);

        toast.classList.add("toast", `toast--${type}`);
        toast.style.animation = `slideInLeft ease .3s, fadeOut linear 1s ${delay}s forwards`;

        toast.innerHTML = `
                  <div class="toast__icon">
                      <i class="${icon}"></i>
                  </div>
                  <div class="toast__body">
                      <h3 class="toast__title">${title}</h3>
                      <p class="toast__msg">${message}</p>
                  </div>
                  <div class="toast__close">
                      <i class="fas fa-times"></i>
                  </div>
              `;
        main.appendChild(toast);
    }
}

// Function to submit the exam
function submitExam() {
    // Hiển thị hộp thoại xác nhận
    if (confirm("Bạn có chắc chắn muốn nộp bài không?")) {
        // Nếu người dùng xác nhận, thì hiển thị toast thành công
        toast({
            title: "Nộp bài",
            message: "Bạn đã nộp bài thành công!",
            type: "success",
            duration: 500,
            callback: function () {
                window.location.href = "/";
            }
        });
    }
}

// Function to exit the exam with confirmation
function exitExam() {
    // Hiển thị hộp thoại xác nhận
    if (confirm("Bạn có chắc chắn muốn thoát khỏi kỳ thi không?")) {
        // Nếu người dùng xác nhận, thì hiển thị toast thoát
        toast({
            title: "Thoát",
            message: "Bạn đã thoát khỏi kỳ thi thành công!",
            type: "info",
            duration: 500,
            callback: function () {
                window.location.href = "/login";
            }
        });
    }
}

// Function to save the exam without confirmation
function saveExam() {
    // Hiển thị toast thông báo đã lưu bài thi thành công
    toast({
        title: "Lưu bài thi",
        message: "Bài thi của bạn đã được lưu thành công!",
        type: "info",
        duration: 1000,
        callback: function () {
            // Có thể thực hiện các hành động khác nếu cần sau khi toast biến mất
        }
    });
}



function changeButtonColor(button) {
    // Tìm phần tử cha của nút được click trong từng vòng lặp riêng biệt
    const parent = button.closest('.form-input');

    // Nếu không tìm thấy phần tử cha, không làm gì cả
    if (!parent) {
        return;
    }

    // Tìm tất cả các nút trong phần tử cha
    const buttons = parent.querySelectorAll(".btn1.answer");

    // Đặt lại màu nền và bóng đổ của tất cả các nút trong phần tử cha
    buttons.forEach((btn) => {
        btn.style.backgroundColor = "#fff";
        btn.style.boxShadow = "none";
    });

    // Thay đổi màu nền và bóng đổ của nút được click
    button.style.backgroundColor = "#7be56a";
    button.style.boxShadow = "0 0 40px #7be56a";
    button.style.transition = ".5s ease";
}


function scrollToQuestion(questionNumber) {
    var questionElement = document.getElementById("question" + questionNumber );
    if (questionElement) {
        questionElement.scrollIntoView({ behavior: 'smooth' });
    }
}