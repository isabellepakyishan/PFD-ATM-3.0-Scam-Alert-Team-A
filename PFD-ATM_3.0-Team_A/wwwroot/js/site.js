$("#modal-title").text("Too close!");
$(".modal-body").html(' \
        <div class="text-center"> \
            <p>Please ensure you are at least 0.5m away from any individual.</p> \
            <lottie-player src="https://assets10.lottiefiles.com/packages/lf20_6twxg3pm.json" background="transparent" speed="1" style="width: 300px; height: 300px; margin: 0 auto" loop autoplay></lottie-player> \
        </div > ');
setInterval( function () {
    fetch('/StaticFiles/depth.txt')
        .then(response => response.text())
        .then(txt => {
            distanceWarning(txt);
            console.log(txt);
        });
}, 4000);

function distanceWarning(d) {
    if (d < 51 && d) {
        $(".modal").modal("show");
    }
    else {
        $(".modal").modal("hide");
    }
}