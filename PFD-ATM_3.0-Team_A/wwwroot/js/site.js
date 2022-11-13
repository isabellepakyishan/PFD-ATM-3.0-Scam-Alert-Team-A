
setInterval(function () {
    fetch('/StaticFiles/fear.txt')
        .then(response => response.text())
        .then(txt => {
            facialExpressionCheck(txt);
            console.log("fear: "+txt);
        });
}, 2500);

setInterval( function () {
    fetch('/StaticFiles/depth.txt')
        .then(response => response.text())
        .then(txt => {
            distanceWarning(txt);
            console.log("depth_diff: "+txt);
        });
}, 2500);

function distanceWarning(d) {
    if (d < 51 && d) {
        $("#modal-title").text("Too close!");
        $(".modal-body").html(' \
        <div class="text-center"> \
            <p>Please ensure you are at least 0.5m away from any individual.</p> \
            <lottie-player src="https://assets10.lottiefiles.com/packages/lf20_6twxg3pm.json" background="transparent" speed="1" style="width: 300px; height: 300px; margin: 0 auto" loop autoplay></lottie-player> \
        </div > ');
        $(".modal").modal("show");
    }
}

function facialExpressionCheck(em) {
    if (em > 79) {
        $("#modal-title").text("Do you require assistance?");
        $(".modal-body").html(' \
        <div class="text-center"> \
            <p>Our system has detected that you are in some distress.</p> \
            <p>Would you like to continue this trasaction?</p> \
            <div class="d-sm-inline-flex"> \
                <button type="button" class="btn btn-primary mx-2">End Transaction</button> \
                <button type="button" onclick="modalHide()" class="btn btn-primary mx-2">Continue</button> \
            </div > \
        </div > ');
        $(".modal").modal("show");
    }
}

function modalHide() {
    $(".modal").modal("hide");
}