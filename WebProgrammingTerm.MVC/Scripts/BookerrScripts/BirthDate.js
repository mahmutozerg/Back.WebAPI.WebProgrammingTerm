function populateMonths() {
    var select = document.getElementById("birthMonth");

    for (var i = 1; i <= 12; i++) {
        var month = ("0" + i).slice(-2);
        var option = document.createElement("option");
        option.value = month;
        option.text = month;
        select.appendChild(option);
    }
}

function populateDays() {
    var select = document.getElementById("birthDay");

    for (var i = 1; i <= 31; i++) {
        var option = document.createElement("option");
        option.value = i;
        option.text = i;
        select.appendChild(option);
    }
}

function populateYears() {
    var select = document.getElementById("birthYear");
    var currentYear = new Date().getFullYear();
    for (var i = currentYear; i >= currentYear - 100; i--) {
        var option = document.createElement("option");
        option.value = i;
        option.text = i;
        select.appendChild(option);
    }
}

window.onload = function () {
    populateMonths();
    populateDays();
    populateYears();
};