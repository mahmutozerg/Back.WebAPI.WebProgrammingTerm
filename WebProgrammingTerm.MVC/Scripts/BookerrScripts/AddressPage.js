function showPopup() {
    document.getElementById('popup').style.display = 'block';
}

function closePopup() {
    document.getElementById('popup').style.display = 'none';
}

function ShowAddressInfo(index,id, title, address, city, zipcode, value) {
    document.getElementById("clickedIndex").value = index;
    document.getElementById("title-input").value = title;
    document.getElementById("address-input").value = address;
    document.getElementById("zipcode").value = zipcode;
    document.getElementById("locationUpdateDto_Id").value = id;
    let cityDropdown = document.getElementById("city");
    cityDropdown.value = getCityValue(city, value);

    ShowAddress();
}
function getCityValue(cityName, cityList) {


    var city = cityList.find(function (item) {
        return item.Text === cityName;
    });

    return city ? city.Value : "";
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
    document.getElementById("zipcode").value = 0;
    document.getElementById("city").value = 1;

    document.getElementById("clickedIndex").value = -1;
    document.getElementById("title-input").placeholder = "Address Title";
    document.getElementById("address-input").placeholder = "Address";
    document.getElementById("zipcode").placeholder = "Zip code";

    ShowAddress();
}
