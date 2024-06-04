using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationGameplay : MonoBehaviour
{
    public TextMeshProUGUI text1;

    public TextMeshProUGUI Heli1;
    public TextMeshProUGUI Heli2;
    public TextMeshProUGUI Heli3;
    public TextMeshProUGUI Heli4;
    public TextMeshProUGUI Heli5;

    public TextMeshProUGUI Plane1;
    public TextMeshProUGUI Plane2;
    public TextMeshProUGUI Plane3;
    public TextMeshProUGUI Plane4;

    public TextMeshProUGUI ObjectInteraction;

    public TextMeshProUGUI BuildObject1;
    public TextMeshProUGUI BuildObject2;
    public TextMeshProUGUI BuildObject3;

    public TextMeshProUGUI DeletingObject1;
    public TextMeshProUGUI DeletingObject2;

    public TextMeshProUGUI RotatingObject1;
    public TextMeshProUGUI RotatingObject2;

    public TextMeshProUGUI IdleInstruction1;
    public TextMeshProUGUI IdleInstruction2;
    public TextMeshProUGUI IdleInstruction3;
    public TextMeshProUGUI IdleInstruction4;
    public TextMeshProUGUI IdleInstruction5;
    public TextMeshProUGUI IdleInstruction6;
    public TextMeshProUGUI IdleInstruction7;
    public TextMeshProUGUI IdleInstruction8;

    public TextMeshProUGUI CitizenEnterInstruction;

    public TextMeshProUGUI CarControl1;
    public TextMeshProUGUI CarControl2;
    public TextMeshProUGUI CarControl3;
    public TextMeshProUGUI CarControl4;
    public TextMeshProUGUI CarControl5;

    [Header("Mobile")]
    public TextMeshProUGUI LeftUpButton1;
    public TextMeshProUGUI LeftUpButton2;
    public TextMeshProUGUI LeftUpButton3;

    [Header("Multi")]
    public TextMeshProUGUI Weapon2;
    public TextMeshProUGUI Weapon3;
    public TextMeshProUGUI Weapon4;
    public TextMeshProUGUI Weapon5;

    public TextMeshProUGUI BuildMenu1;
    public TextMeshProUGUI BuildMenu2;
    public TextMeshProUGUI BuildMenu3;
    public TextMeshProUGUI BuildMenu4;
    public TextMeshProUGUI BuildMenu5;
    public TextMeshProUGUI BuildMenu6;
    public TextMeshProUGUI BuildMenu7;
    public TextMeshProUGUI BuildMenu8;
    public TextMeshProUGUI BuildMenu9;


    public TextMeshProUGUI RotatingObj1;
    public TextMeshProUGUI RotatingObj2;
    public TextMeshProUGUI RotatingObj3;
    public TextMeshProUGUI RotatingObj4;
    public TextMeshProUGUI RotatingObj5;
    public TextMeshProUGUI RotatingObj6;
    public TextMeshProUGUI RotatingObj7;
    public TextMeshProUGUI RotatingObj8;
    public TextMeshProUGUI RotatingObj9;
    public TextMeshProUGUI RotatingObj10;

    public TextMeshProUGUI SaveMapHeader;
    public TextMeshProUGUI MoreCoins;

    public TextMeshProUGUI InAppShop1;
    public TextMeshProUGUI InAppShop2;
    public TextMeshProUGUI InAppShop3;
    public TextMeshProUGUI InAppShop4;
    public TextMeshProUGUI InAppShop5;
    public TextMeshProUGUI InAppShopB1;
    public TextMeshProUGUI InAppShopB2;
    public TextMeshProUGUI InAppShopB3;

    public TextMeshProUGUI InAppShopConfirm1;
    public TextMeshProUGUI InAppShopConfirm2;

    public TextMeshProUGUI AdWarningConfirm1;
    public TextMeshProUGUI AdWarningConfirm2;
    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            RuLocalization();
        }
        if (Geekplay.Instance.language == "en")
        {
            EnLocalization();
        }
        if (Geekplay.Instance.language == "tr")
        {
            TrLocalization();
        }
    }
    private void RuLocalization()
    {
        text1.text = "Нажмите F, чтобы сесть";

        Heli1.text = "Наклон: W,A,S,D";
        Heli2.text = "Поворот: Q, E";
        Heli3.text = "Увеличить/уменьшить мощность: Пробел, Z";
        Heli4.text = "Включить/выключить двигатель: X";
        Heli5.text = "Выйти: F";

        Plane1.text = "Взлететь: удерживай пробел";
        Plane2.text = "Наклон: W,A,S,D";
        Plane3.text = "Поворот: Q, E";
        Plane4.text = "Выйти: F";
        ObjectInteraction.text = "Нажмите F, чтобы взаимодействовать";
        BuildObject1.text = "Поставить объект: Левая кнопка мыши";
        BuildObject2.text = "Повернуть: R";
        BuildObject3.text = "Выйти из режима: B";

        DeletingObject1.text = "Удалить объект: Левая кнопка мыши";
        DeletingObject2.text = "Выйти из режима:N";

        RotatingObject1.text = "Выбрать объект: Левая кнопка мыши";
        RotatingObject2.text = "Выйти из режима:M";

        IdleInstruction1.text = "Строительство: B";
        IdleInstruction2.text = "Удаление: N";
        IdleInstruction3.text = "Перестройка: M";
        IdleInstruction4.text = "Скриншот: P";
        IdleInstruction5.text = "Сохранение: K";
        IdleInstruction6.text = "Сменить камеру: Q";
        IdleInstruction7.text = "Магазин: I";
        IdleInstruction8.text = "В меню: Tab";

        CitizenEnterInstruction.text = "Нажмите F, чтобы поменяться телами";

        CarControl1.text = "Управление: W,A,S,D";
        CarControl2.text = "Тормоз: Пробел";
        CarControl3.text = "Сменить камеру: Q";
        CarControl4.text = "Выйти: F";
        CarControl5.text = "Выстрел: Левая кнопка мыши";

        LeftUpButton1.text = "Скриншот";
        LeftUpButton2.text = "Сохранить";
        LeftUpButton3.text = "В меню";

        Weapon2.text = "Пистолет";
        Weapon3.text = "Нож";
        Weapon4.text = "Рука";
        Weapon5.text = "Граната";

        BuildMenu1.text = "Машины";
        BuildMenu2.text = "Воздушный транспорт";
        BuildMenu3.text = "Аэропорт";
        BuildMenu4.text = "Ферма";
        BuildMenu5.text = "Военное";
        BuildMenu6.text = "Город";
        BuildMenu7.text = "Жители";
        BuildMenu8.text = "Животные";
        BuildMenu9.text = "Небо";

        RotatingObj1.text = "Размер X";
        RotatingObj2.text = "Размер Y";
        RotatingObj3.text = "Размер Z";
        RotatingObj4.text = "Поворот X";
        RotatingObj5.text = "Поворот Y";
        RotatingObj6.text = "Поворот Z";
        RotatingObj7.text = "РАЗМЕР";
        RotatingObj8.text = "ПОВОРОТ";
        RotatingObj9.text = "Применить";
        RotatingObj10.text = "Сбросить";

        SaveMapHeader.text = "ВЫБЕРИТЕ СЛОТ ДЛЯ СОХРАНЕНИЯ";
        MoreCoins.text = "Получить Больше";

        InAppShop1.text = "МАГАЗИН";
        InAppShop2.text = "10 монет";
        InAppShop3.text = "50 монет";
        InAppShop4.text = "100 монет";
        InAppShop5.text = "500 монет";
        InAppShopB1.text = "10 Ян";
        InAppShopB2.text = "10 Ян";
        InAppShopB3.text = "10 Ян";

        InAppShopConfirm1.text = "Вы получили:";
        InAppShopConfirm2.text = "ОТЛИЧНО!";

        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;
    }
    private void EnLocalization()
    {
        text1.text = "Press F to sit";

        Heli1.text = "Pitch: W,A,S,D";
        Heli2.text = "Roll: Q, E";
        Heli3.text = "Increase/decrease power: Space, Z";
        Heli4.text = "Turn on/off the engine: X";
        Heli5.text = "Exit: F";

        Plane1.text = "Take off: hold the space bar";
        Plane2.text = "Pitch: W,A,S,D";
        Plane3.text = "Roll: Q, E";
        Plane4.text = "Exit: F";

        ObjectInteraction.text = "Press F to interact";
        BuildObject1.text = "Put the object: Left mouse button";
        BuildObject2.text = "Rotate: R";
        BuildObject3.text = "Exit mode: B";

        DeletingObject1.text = "Delete object: Left mouse button";
        DeletingObject2.text = "Exit mode:N";

        RotatingObject1.text = "Select an object: Left mouse button";
        RotatingObject2.text = "Exit mode:M";

        IdleInstruction1.text = "Building: B";
        IdleInstruction2.text = "Deleting: N";
        IdleInstruction3.text = "Rebuild: M";
        IdleInstruction4.text = "Screenshot: P";
        IdleInstruction5.text = "Save: K";
        IdleInstruction6.text = "Change camera: Q";
        IdleInstruction7.text = "Store: I";
        IdleInstruction8.text = "Go to menu: Tab";

        CitizenEnterInstruction.text = "Press F to swap bodies";

        CarControl1.text = "Controls: W,A,S,D";
        CarControl2.text = "Brake: Space bar";
        CarControl3.text = "Change camera: Q";
        CarControl4.text = "Exit: F";
        CarControl5.text = "Shoot: Left mouse button";

        LeftUpButton1.text = "Screenshot";
        LeftUpButton2.text = "Save";
        LeftUpButton3.text = "Go to menu";

        Weapon2.text = "Pistol";
        Weapon3.text = "Knife";
        Weapon4.text = "Hand";
        Weapon5.text = "Grenade";

        BuildMenu1.text = "Cars";
        BuildMenu2.text = "Air transport";
        BuildMenu3.text = "Airport";
        BuildMenu4.text = "Farm";
        BuildMenu5.text = "Military";
        BuildMenu6.text = "City";
        BuildMenu7.text = "Citizens";
        BuildMenu8.text = "Animals";
        BuildMenu9.text = "Sky";

        RotatingObj1.text = "Size X";
        RotatingObj2.text = "Size Y";
        RotatingObj3.text = "Size Z";
        RotatingObj4.text = "Rotate X";
        RotatingObj5.text = "Rotate Y";
        RotatingObj6.text = "Rotate Z";
        RotatingObj7.text = "SIZE";
        RotatingObj8.text = "ROTATION";
        RotatingObj9.text = "Apply";
        RotatingObj10.text = "Reset";

        SaveMapHeader.text = "SELECT A SLOT TO SAVE";
        MoreCoins.text = "Get More";

        InAppShop1.text = "STORE";
        InAppShop2.text = "10 coins";
        InAppShop3.text = "50 coins";
        InAppShop4.text = "100 coins";
        InAppShop5.text = "500 coins";
        InAppShopB1.text = "10 Yan";
        InAppShopB2.text = "10 Yan";
        InAppShopB3.text = "10 Yan";

        InAppShopConfirm1.text = "You got:";
        InAppShopConfirm2.text = "GREAT!";
        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;
    }
    private void TrLocalization()
    {
        text1.text = "Oturmak için F'ye basın";

        Heli1.text = "Eğim: W,A,S,D";
        Heli2.text = "Dönüş: Q, E";
        Heli3.text = "Gücü Arttır/azalt: Boşluk, Z";
        Heli4.text = "Motoru aç/kapat: X";
        Heli5.text = "Çık: F";

        Plane1.text = "Kalkış: boşluğu koru";
        Plane2.text = "Eğim: W,A,S,D";
        Plane3.text = "Dönüş: Q, E";
        Plane4.text = "Çık: F";
        ObjectInteraction.text = "Etkileşim kurmak için F'ye basın";
        BuildObject1.text = "Nesneyi koy: Farenin sol düğmesi";
        BuildObject2.text = "Döndür: R";
        BuildObject3.text = "Moddan çık: B";

        DeletingObject1.text = "Nesneyi sil: Farenin sol düğmesi";
        DeletingObject2.text = "Moddan çık:N";

        RotatingObject1.text = "Nesne seç: Farenin sol düğmesi";
        RotatingObject2.text = "Moddan çık:M";

        IdleInstruction1.text = "İnşaat: B";
        IdleInstruction2.text = "Silme: N";
        IdleInstruction3.text = "Yeniden yapılandırma: M";
        IdleInstruction4.text = "Ekran görüntüsü: P";
        IdleInstruction5.text = "Kaydet: K";
        IdleInstruction6.text = "Kamerayı değiştir:Q";
        IdleInstruction7.text = "Mağaza: İ";
        IdleInstruction8.text = "Menüde: Sekme";

        CitizenEnterInstruction.text = "Beden değiştirmek için F'ye basın";

        CarControl1.text = "Kontrol: W,A,S,D";
        CarControl2.text = "Fren: Boşluk Çubuğu";
        CarControl3.text = "Kamerayı değiştir: Q";
        CarControl4.text = "Çık: F";
        CarControl5.text = "Atış: Farenin sol düğmesi";


        LeftUpButton1.text = "Ekran görüntüsü";
        LeftUpButton2.text = "Kaydet";
        LeftUpButton3.text = "Menüde";

        Weapon2.text = "Tabanca";
        Weapon3.text = "Bıçak";
        Weapon4.text = "El";
        Weapon5.text = "El bombası";

        BuildMenu1.text = "Makineler";
        BuildMenu2.text = "Hava taşımacılığı";
        BuildMenu3.text = "Havaalanı";
        BuildMenu4.text = "Çiftlik";
        BuildMenu5.text = "Askeri";
        BuildMenu6.text = "Şehir";
        BuildMenu7.text = "Sakinler";
        BuildMenu8.text = "Hayvanlar";
        BuildMenu9.text = "Gökyüzü";

        RotatingObj1.text = "Boyut X";
        RotatingObj2.text = "Boyut Y";
        RotatingObj3.text = "Boyut Z";
        RotatingObj4.text = "dönüşü X";
        RotatingObj5.text = "dönüşü Y";
        RotatingObj6.text = "dönüşü Z";
        RotatingObj7.text = "BOYUT";
        RotatingObj8.text = "DÖNDÜRME";
        RotatingObj9.text = "Uygula";
        RotatingObj10.text = "Sıfırla";

        SaveMapHeader.text = "KAYDEDİLECEK YUVAYI SEÇİN";
        MoreCoins.text = "Daha Fazlasını alın";

        InAppShop1.text = "MAĞAZA";
        InAppShop2.text = "10 jeton";
        InAppShop3.text = "50 jeton";
        InAppShop4.text = "100 jeton";
        InAppShop5.text = "500 jeton";
        InAppShopB1.text = "10 Yan";
        InAppShopB2.text = "10 Yan";
        InAppShopB3.text = "10 Yan";

        InAppShopConfirm1.text = "Aldınız:";
        InAppShopConfirm2.text = "HARİKA!";

        AdWarningConfirm1.text = InAppShopConfirm1.text;
        AdWarningConfirm2.text = InAppShopConfirm2.text;
    }
}
