function showPopup() {
    document.getElementById('popup').style.display = 'block';
}

function closePopup() {
    document.getElementById('popup').style.display = 'none';
}

function ShowAddressInfo(index,id, title, address, city, zipcode) {
    let cities = document.getElementById("city")
    let cityTexts = [];

        for (let i = 0; i < cities.options.length; i++) {
            let option = cities.options[i];
            cityTexts.push(option.text);
        }

    
    document.getElementById("clickedIndex").value = index;
    console.log(document.getElementById("clickedIndex").value);
    document.getElementById("title-input").value = title;
    document.getElementById("address-input").value = address;
    document.getElementById("zipcode").value = zipcode;
    document.getElementById("locationUpdateDto_Id").value = id;
    let cityValue = getCityValue(city, cityTexts);
    cities.value =  cityTexts.findIndex(city => city === cityValue)+1;
    ShowAddress();
}

function  SetClickedIndex(value)
{
    document.getElementById("clickedIndex").value = value;

}
function getCityValue(cityName, cityList) {

    var city = cityList.find(function (item) {
        return item === cityName;
    });
    return city ? city : "";
}
function ShowAddress()
{

    let address = document.querySelector("body > div > div > div.address > div.add-address");
    address.style.display = "";

}

function ResetAddress()
{
    document.getElementById("clickedIndex").value = -1;
    document.getElementById("title-input").value = "";
    document.getElementById("address-input").value = "";
    document.getElementById("zipcode").value = null;
    document.getElementById("city").value = 1;

    document.getElementById("title-input").placeholder = "Address Title";
    document.getElementById("address-input").placeholder = "Address";
    document.getElementById("zipcode").placeholder = "Zip code";

    ShowAddress();
}


function confirmDelete(index,locationId) {
    document.getElementById("locationIdToDelete").value = locationId;
    showConfirmationPopup();
}

function showConfirmationPopup() {
    document.getElementById("confirmationPopup").style.display = "block";
}

function closeConfirmationPopup() {
    document.getElementById("confirmationPopup").style.display = "none";
}

function confirmAndSubmit() {
    closeConfirmationPopup();
    document.getElementById("deleteForm").submit();
}