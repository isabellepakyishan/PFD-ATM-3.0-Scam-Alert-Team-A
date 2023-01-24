const APIKEY = "6373dcd4c890f30a8fd1f3c2";
//const spawner = require('child_process').spawn

var fear = setInterval(function () {
    if (sessionStorage.getItem("AccountNo") != null) {
        clearInterval(fear);
        setInterval(function () {
            fetch('/StaticFiles/fear.txt') // fetches fear value from python script constantly
                .then(response => response.text())
                .then(txt => {
                    if (txt.includes("true")) { // Checks how many times the fear threshold has been reached
                        facialExpressionCheck(); // Shows modal
                    }
                    console.log("has fear: " + txt);
                });
        }, 500); // delay
    } else {
        getAccountNo();
    }
}, 3000);


setInterval( function () {
    fetch('/StaticFiles/depth.txt') // fetches depth diff value from python script constantly
        .then(response => response.text())
        .then(txt => {
            distanceWarning(txt); // Shows modal
            //console.log("depth_diff: "+txt);
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
        getClip()
            .then(vid => {
                console.log(vid);
                postAlert(sessionStorage.getItem("AccountNo"), 1, $.now(), vid);
            });
        $("#ferModal").modal("show");
    }   
}

function modalHide() {
    $(".modal").modal("hide");
}
/*function postAlert(accNo, atmId, date) {
    fetch("/StaticFiles/output.avi")
        .then(function (response) {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return response.blob();
        })
        .then(function (blob) {
            const file = new File([blob], 'output.avi', { type: 'video/x-msvideo' });
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function () {
            }
        })
        .catch(function(error) {
            console.log(error);
        });
}*/
function postAlert(accNo, atmId, date, clip) {
    let jsondata = { "atmId": atmId, "accountNo": accNo, "date": date, "clip": clip }
    let settings = {
        "async": true,
        "crossDomain": true,
        "url": "https://pfdatm3teama-3b47.restdb.io/rest/atm-alerts",
        "method": "POST",
        "headers": {
            "content-type": "application/json",
            "x-apikey": APIKEY,
            "cache-control": "no-cache"
        },
        "processData": false,
        "data": JSON.stringify(jsondata)
    }
    $.ajax(settings).done();
}

function getClip() {
    return fetch("/StaticFiles/vid.txt")
        .then(response => response.text())
        .then(text => text);
}

var index = 0;

function OnKeyPadPressed(number) {
    const id = "#layout-wrapper > div > div > form > div:nth-child(1) > input";
    const element = $(id)[index];
    element.value = number;

    if (index == 5) {
        $("#enter").removeAttr("disabled");
        $("#asterisk").removeAttr("disabled");
        setTimeout(function () {
            $("#pin_form").submit();
        }, 3000);
    } else if (index == 6)
        $("#pin_form").submit();

    index += 1;
}
setInterval(function () {
    $("#account-no").keypress(function () {
        if ($("#account-no").val().toString().length == 12)
            $("#checkAccountExists").removeAttr("disabled");
    });
});


var pins = document.getElementsByClassName('form-control pin-no');

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

window.addEventListener('load', () => {
    const button = document.querySelector("#layout-wrapper > div.page-content > div > form > div:nth-child(2) > div:nth-child(1) > div > button");
    button.addEventListener('click', () => {
        var accountNos = document.getElementsByClassName('form-control account-no')
        Array.from(accountNos).forEach(function (accountNo) {
            accountNo.value = "";
        })
    });
});

function getAccountNo() {
    $.ajax({
        type: "POST",
        url: "/Home/AjaxCallAccountNo",
        data: { "sessionName": "AccountNo" },
        success: function (response) {
            if (response != null) {
                sessionStorage.setItem("AccountNo", response);
            }
        }
    });
}