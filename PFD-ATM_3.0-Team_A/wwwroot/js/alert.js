const APIKEY = "6373dcd4c890f30a8fd1f3c2";

$(document).ready(function () {
    let settings = {
        "async": true,
        "crossDomain": true,
        "url": "https://pfdatm3teama-3b47.restdb.io/rest/atm-alerts",
        "method": "GET",
        "headers": {
            "content-type": "application/json",
            "x-apikey": APIKEY,
            "cache-control": "no-cache"
        }
    }

    $.ajax(settings).done(function (response) {
        console.log(response);

        if (response.length != 0) {
            for (i = 0; i < response.length; i++) {
                $("#alert_table").html(`<tr><td>${response[i].date}</td><td>${response[i].accountNo}</td><td>${response[i].atmId}</td></tr>`);
            }
        }
        else {
            $("#alert_table").html(`<span class="text-secondary">No records found.</span>`);
        }
        $("#alert_num").html(`${response.length}`);
    });
});