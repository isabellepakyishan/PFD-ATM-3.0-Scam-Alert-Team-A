var fearCount = 0;

setInterval(function () {
    fetch('/StaticFiles/fear.txt') // fetches fear value from python script constantly
        .then(response => response.text())
        .then(txt => {
            if (fearCount > 1) { // Checks how many times the fear threshold has been reached
                facialExpressionCheck(); // Shows modal
            } else if (txt > 79) {
                fearCount += 1
            }
            console.log("fear: "+txt+"\ncount: "+fearCount);
        });
}, 2000); // delay

setInterval( function () {
    fetch('/StaticFiles/depth.txt') // fetches depth diff value from python script constantly
        .then(response => response.text())
        .then(txt => {
            distanceWarning(txt); // Shows modal
            console.log("depth_diff: "+txt);
        });
}, 2500); // delay

function distanceWarning(d) {
    if (d < 76 && d) {
        $("#faceDepthModal").modal("show");
    }
    else {
        $("#faceDepthModal").modal("hide"); // automatic modal close
    }
}

function facialExpressionCheck() {
    if (!$("#ferModal").is(":visible")) { // Checks if modal is open and skips over if it is
        $("#ferModal").modal("show");
        fearCount = 0; // Reset fear counter
    }   
}

function modalHide() {
    $(".modal").modal("hide");
    fearCount = 0
}