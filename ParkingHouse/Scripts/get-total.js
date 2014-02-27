$(document).ready(function () {
    $.get("/GetTotal", {
        sum: sum,
        carsTotal: carsTotal
    }, function (data) {
        var sum = data.sum;
        var cars = data.carsTotal;
        alert("Total sum: " + sum + ", Total cars: " + cars);
    });
});