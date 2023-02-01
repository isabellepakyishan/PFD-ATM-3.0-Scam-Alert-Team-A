const APIKEY = "6373dcd4c890f30a8fd1f3c2";

function getClipLink(clipLink) {
    document.getElementById("clipId").src = clipLink
    document.getElementById("videoContainer").load();
}

function deleteClip(id) {
    var settings = {
        "async": true,
        "crossDomain": true,
        "url": "https://pfdatm3teama-3b47.restdb.io/rest/atm-alerts/"+id,
        "method": "DELETE",
        "headers": {
            "content-type": "application/json",
            "x-apikey": APIKEY,
            "cache-control": "no-cache"
        }
    }

    $.ajax(settings).done(function (response) {
        console.log(response);
        location.reload();
    });
}

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
                var alertDate = new Date(response[i].date);
                var alertDateFormatted = alertDate.getDate() + "/" + (alertDate.getMonth() + 1) + "/" + alertDate.getFullYear() + " " + alertDate.getHours() + ":" + (alertDate.getMinutes() < 10 ? "0" : "") + alertDate.getMinutes();
                $("#alert_table").append(`<tr><td>${alertDateFormatted}</td><td>${response[i].accountNo}</td><td>${response[i].atmId}</td><td><button type="button" class="btn btn-secondary" onclick="getClipLink('${response[i].clip}')">View</button></td><td><button type="button" class="btn btn-secondary" onclick="deleteClip('${response[i]._id}')">Delete</button></td></tr>`);
            }
        }
        else {
            $("#alert_table").html(`<span class="text-secondary">No records found.</span>`);
        }
        $("#alert_num").html(`${response.length}`);
    });
});