/*$.ajax({
    url: "_CV.cshtml",
    type: "post",
    dataType: "text",
    success: function (data) {
        $("#test").html(data);
    }
})
let { PythonShell } = require('python-shell');

PythonShell.run('FaceDepthMeasurement.py', function (err, results) {
    console.log(results);
})*/
$(document).ready(function () {
    const body = document.getElementById("test");
    setInterval(async function () {
        await fetch('StaticFiles/depth.txt')
            .then(response => response.text())
            .then(txt => {
                body.innerHTML = txt
                console.log(txt)
            });
    }, 5);
});