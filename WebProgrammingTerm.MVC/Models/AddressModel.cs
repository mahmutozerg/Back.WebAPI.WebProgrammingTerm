using System.Collections.Generic;
using System.Web.Mvc;
using SharedLibrary.DTO;
using SharedLibrary.Models;

namespace WebProgrammingTerm.MVC.Models;

public class AddressModel
{
    public List<Location> locations { get; set; } = new List<Location>();
    public LocationUpdateDto locationUpdateDto { get; set; } = new LocationUpdateDto();
    public List<SelectListItem> CityList { get; set; }
    public int clickedIndex { get; set; } = -1;


    public AddressModel()
    {
        CityList = new List<SelectListItem>
        {
            new() { Value = "1", Text = "Adana" },
            new () { Value = "2", Text = "Adıyaman" },
            new () { Value = "3", Text = "Afyonkarahisar" },
            new () { Value = "4", Text = "Ağrı" },
            new () { Value = "5", Text = "Amasya" },
            new () { Value = "6", Text = "Ankara" },
            new () { Value = "7", Text = "Antalya" },
            new () { Value = "8", Text = "Ardahan" },
            new () { Value = "9", Text = "Artvin" },
            new () { Value = "10", Text = "Aydın" },
            new () { Value = "11", Text = "Balıkesir" },
            new () { Value = "12", Text = "Bartın" },
            new () { Value = "13", Text = "Batman" },
            new () { Value = "14", Text = "Bayburt" },
            new () { Value = "15", Text = "Bilecik" },
            new () { Value = "16", Text = "Bingöl" },
            new () { Value = "17", Text = "Bitlis" },
            new () { Value = "18", Text = "Bolu" },
            new () { Value = "19", Text = "Burdur" },
            new () { Value = "20", Text = "Bursa" },
            new () { Value = "21", Text = "Çanakkale" },
            new () { Value = "22", Text = "Çankırı" },
            new () { Value = "23", Text = "Çorum" },
            new () { Value = "24", Text = "Denizli" },
            new () { Value = "25", Text = "Diyarbakır" },
            new () { Value = "26", Text = "Düzce" },
            new () { Value = "27", Text = "Edirne" },
            new () { Value = "28", Text = "Elazığ" },
            new () { Value = "29", Text = "Erzincan" },
            new () { Value = "30", Text = "Erzurum" },
            new () { Value = "31", Text = "Eskişehir" },
            new () { Value = "32", Text = "Gaziantep" },
            new () { Value = "33", Text = "Giresun" },
            new () { Value = "34", Text = "Gümüşhane" },
            new () { Value = "35", Text = "Hakkari" },
            new () { Value = "36", Text = "Hatay" },
            new () { Value = "37", Text = "Iğdır" },
            new () { Value = "38", Text = "Isparta" },
            new () { Value = "39", Text = "İstanbul" },
            new () { Value = "40", Text = "İzmir" },
            new () { Value = "41", Text = "Kahramanmaraş" },
            new () { Value = "42", Text = "Karabük" },
            new () { Value = "43", Text = "Karaman" },
            new () { Value = "44", Text = "Kars" },
            new () { Value = "45", Text = "Kastamonu" },
            new () { Value = "46", Text = "Kayseri" },
            new () { Value = "47", Text = "Kilis" },
            new () { Value = "48", Text = "Kırıkkale" },
            new () { Value = "49", Text = "Kırklareli" },
            new () { Value = "50", Text = "Kırşehir" },
            new () { Value = "51", Text = "Kocaeli" },
            new () { Value = "52", Text = "Konya" },
            new () { Value = "53", Text = "Kütahya" },
            new () { Value = "54", Text = "Malatya" },
            new () { Value = "55", Text = "Manisa" },
            new () { Value = "56", Text = "Mardin" },
            new () { Value = "57", Text = "Mersin" },
            new () { Value = "58", Text = "Muğla" },
            new () { Value = "59", Text = "Muş" },
            new () { Value = "60", Text = "Nevşehir" },
            new () { Value = "61", Text = "Niğde" },
            new () { Value = "62", Text = "Ordu" },
            new () { Value = "63", Text = "Osmaniye" },
            new () { Value = "64", Text = "Rize" },
            new () { Value = "65", Text = "Sakarya" },
            new () { Value = "66", Text = "Samsun" },
            new () { Value = "67", Text = "Siirt" },
            new () { Value = "68", Text = "Sinop" },
            new () { Value = "69", Text = "Sivas" },
            new () { Value = "70", Text = "Şanlıurfa" },
            new () { Value = "71", Text = "Şırnak" },
            new () { Value = "72", Text = "Tekirdağ" },
            new () { Value = "73", Text = "Tokat" },
            new () { Value = "74", Text = "Trabzon" },
            new () { Value = "75", Text = "Tunceli" },
            new () { Value = "76", Text = "Uşak" },
            new () { Value = "77", Text = "Van" },
            new () { Value = "78", Text = "Yalova" },
            new () { Value = "79", Text = "Yozgat" },
            new () { Value = "80", Text = "Zonguldak" }
        };
    }
}