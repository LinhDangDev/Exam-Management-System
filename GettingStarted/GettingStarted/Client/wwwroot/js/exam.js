
//audio
function playMusic(audio, listenedCount) {
    // khi chạy audio, các sự kiện dừng, tua nhanh sẽ bị vô hiệu hóa
    audio.style.pointerEvents = "none";
    // ngăn người dùng tua nhanh
    audio.currentTime = 0;
    var solanNghe = document.getElementById(listenedCount);
    solanNghe.value--;
}
function pauseMusic(audio, listenedCount) {
    var solanNghe = document.getElementById(listenedCount);
    if ((int)(solanNghe.value) > 0) {
        audio.style.pointerEvents = "all";
    }
}
function renderMathInBlazor() {
    MathJax.typesetPromise();
}