//// Lấy danh sách tất cả các thẻ audio trong tài liệu
//var audioElements = document.getElementsByTagName("audio");

//// Lặp qua từng thẻ audio và gán sự kiện cho từng thẻ
//for (var i = 0; i < audioElements.length; i++) {
//    var audio = audioElements[i];

//    // Gán sự kiện onplay
//    audio.onplay = function () {
//        console.log("Audio is playing:", this.currentSrc);
//        // Thực hiện các hành động khác khi audio bắt đầu phát
//    };

//    // Gán sự kiện onpause
//    audio.onpause = function () {
//        console.log("Audio is paused:", this.currentSrc);
//        // Thực hiện các hành động khác khi audio tạm dừng
//    };

//    // Gán sự kiện onended
//    audio.onended = function () {
//        console.log("Audio has ended:", this.currentSrc);
//        // Thực hiện các hành động khác khi audio phát đến hết và dừng lại
//    };
//}



//audio
async function playMusic(audio, listenedCount) {
    // khi chạy audio, các sự kiện dừng, tua nhanh sẽ bị vô hiệu hóa
    audio.style.pointerEvents = "none";
    // ngăn người dùng tua nhanh
    audio.currentTime = 0;
    var solanNghe = document.getElementById(listenedCount);
    solanNghe.value--;
    var so_lan_con_lai = parseInt(solanNghe.value);
    if (so_lan_con_lai > 0) {
        await new Promise(resolve => {
            setTimeout(resolve, audio.duration * 1000); // Chờ cho đến khi thời lượng của audio đã qua
        });

        audio.style.pointerEvents = "all";
    }

}
