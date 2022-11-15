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


var index = 1;

function OnKeyPadPressed(number) {
    const id = `#layout-wrapper > div > div > div:nth-child(1) > div > div > input:nth-child(${index})`
    $(id).val(number);
    index += 1;
}

var pins = document.getElementsByClassName('form-control pin-no')
Array.from(pins).forEach(function (pin) {
    pin.addEventListener("keyup", function (event) {
        // Number 13 is the "Enter" key on the keyboard
        if (event.keyCode === 13 || pin.value.length == 1) {
            // Focus on the next sibling
            pin.nextElementSibling.focus()
        }
    });
})

window.addEventListener('load', () => {
    const button = document.querySelector('#clear');
    button.addEventListener('click', () => {
        var pinNos = document.getElementsByClassName('form-control pin-no')
        Array.from(pinNos).forEach(function (pinNo) {
            pinNo.value = "";
        })
        index = 1;
    });
});