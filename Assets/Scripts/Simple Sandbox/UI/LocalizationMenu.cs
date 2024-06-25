using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationMenu : MonoBehaviour
{
    [Header("FromSimpleSandbox")]
    [SerializeField] TextMeshProUGUI Button3;
    [SerializeField] TextMeshProUGUI Button4;
    [SerializeField] TextMeshProUGUI Button5;
    [SerializeField] TextMeshProUGUI LeadersHeadMain;
    [SerializeField] TextMeshProUGUI LeadersHead1;
    [SerializeField] TextMeshProUGUI LeadersHead2;
    [SerializeField] TextMeshProUGUI LeadersHead3;
    [SerializeField] TextMeshProUGUI LeadersDesc1;
    [SerializeField] TextMeshProUGUI LeadersDesc2;
    [SerializeField] TextMeshProUGUI LeadersDesc3;
    [SerializeField] TextMeshProUGUI MenuButton1;
    [SerializeField] TextMeshProUGUI MenuButton2;
    [SerializeField] TextMeshProUGUI PromoAsk;
    [SerializeField] TextMeshProUGUI TelegramButton;
    [SerializeField] TextMeshProUGUI TakeButton;
    [SerializeField] TextMeshProUGUI PromoMessage1;
    [SerializeField] TextMeshProUGUI PromoMessage2;
    [SerializeField] TextMeshProUGUI PromoMessage3;
    [SerializeField] Text PlaceHolder;
    [Header("FromDestroyingSandbox")]
    [SerializeField] TextMeshProUGUI ChooseModeHeader1;
    [SerializeField] TextMeshProUGUI ChooseModeHeader2;
    [SerializeField] TextMeshProUGUI ChooseModeHeader3;
    [SerializeField] TextMeshProUGUI ChooseModeDesc1;
    [SerializeField] TextMeshProUGUI ChooseModeDesc2;
    [SerializeField] TextMeshProUGUI ChooseModeDesc3;
    [SerializeField] TextMeshProUGUI ShopButtonL;
    [SerializeField] TextMeshProUGUI ShopButtonR;
    [Header("BuildingMenu")]
    [SerializeField] TextMeshProUGUI BuildingMenuL;
    [SerializeField] TextMeshProUGUI BuildingMenuR;

    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            RuLocalization();
        }
        else if (Geekplay.Instance.language == "en")
        {
            EnLocalization();
        }
        else if (Geekplay.Instance.language == "tr")
        {
            TrLocalization();
        }
    }
    private void RuLocalization()
    {
        ChooseModeHeader1.text = "Строй и разрушай";
        ChooseModeHeader2.text = "Разрушай чужое";
        ChooseModeHeader3.text = "Паркур";
        ChooseModeDesc1.text = "В этом режиме ты можешь построить все, что захочешь, а после этого разрушить свою постройку!";
        ChooseModeDesc2.text = "В этом режиме ты сможешь разрушить то, что построили другие!";
        ChooseModeDesc3.text = "В этом режиме ты на скорость должен преодолеть препятствия и достичь финиша!";
        ShopButtonL.text = "Больше\nОружия!";
        ShopButtonR.text = "Больше\nЗолота!";
        BuildingMenuL.text = "Строй Новое";
        BuildingMenuR.text = "Загрузить сохраненную карту";
        Button3.text = "ЛИДЕРЫ";
        Button4.text = "БОНУСЫ";
        Button5.text = "НАШИ ИГРЫ";
        LeadersHeadMain.text = "ЛУЧШИЕ ИГРОКИ";
        LeadersHead1.text = "ЛУЧШИЕ СТРОИТЕЛИ";
        LeadersHead2.text = "ЛУЧШИЕ РАЗРУШИТЕЛИ";
        LeadersHead3.text = "ЛУЧШАЯ ПОДДЕРЖКА";
        LeadersDesc1.text = "Стройте как можно больше объектов!";
        LeadersDesc2.text = "Разрушайте все, что можете!";
        LeadersDesc3.text = "Поддержите разработчика донатом!";
        MenuButton1.text = "Меню";
        MenuButton2.text = "Меню";
        PromoAsk.text = "Подпишись на наш канал и введи промокод который найдешь там";
        PlaceHolder.text = "Введите код...";
        TelegramButton.text = "Телеграм";
        TakeButton.text = "Забрать";
        PromoMessage1.text = "Промокод успешно введен";
        PromoMessage2.text = "Промокод уже использован";
        PromoMessage3.text = "Такого промокода нет";
    }
    private void EnLocalization()
    {
        ChooseModeHeader1.text = "Build and destroy";
        ChooseModeHeader2.text = "Destroy someone else's";
        ChooseModeHeader3.text = "Parkour";
        ChooseModeDesc1.text = "In this mode, you can build anything you want, and then destroy your building!";
        ChooseModeDesc2.text = "In this mode, you can destroy what others have built!";
        ChooseModeDesc3.text = "In this mode, you must overcome obstacles at speed and reach the finish line!";
        ShopButtonL.text = "More\nWeapons!";
        ShopButtonR.text = "More\ngold!";
        BuildingMenuL.text = "Build a New One";
        BuildingMenuR.text = "Load a saved map";
        Button3.text = "LEADERS";
        Button4.text = "BONUSES";
        Button5.text = "OUR GAMES";
        LeadersHeadMain.text = "TOP PLAYERS";
        LeadersHead1.text = "THE BEST BUILDERS";
        LeadersHead2.text = "THE BEST DESTROYERS";
        LeadersHead3.text = "BEST SUPPORT";
        LeadersDesc1.text = "Build as many objects as possible!";
        LeadersDesc2.text = "Destroy everything you can!";
        LeadersDesc3.text = "Support the developer with a donation!";
        MenuButton1.text = "Menu";
        MenuButton2.text = "Menu";
        PromoAsk.text = "Subscribe to our channel and enter the promo code that you will find there";
        PlaceHolder.text = "Enter the code...";
        TelegramButton.text = "Telegram";
        TakeButton.text = "Pick up";
        PromoMessage1.text = "Promo code successfully entered";
        PromoMessage2.text = "Promo code has already been used";
        PromoMessage3.text = "There is no such promo code";
    }
    private void TrLocalization()
    {
        ChooseModeHeader1.text = "İnşa et ve yok et";
        ChooseModeHeader2.text = "Başkasını yok et";
        ChooseModeHeader3.text = "Parkur";
        ChooseModeDesc1.text = "Bu modda istediğiniz her şeyi inşa edebilir ve bundan sonra yapınızı yok edebilirsiniz!";
        ChooseModeDesc2.text = "Bu modda başkalarının inşa ettiklerini yok edebilirsiniz!";
        ChooseModeDesc3.text = "Bu modda engelleri hızla aşmalı ve bitiş çizgisine ulaşmalısınız!";
        ShopButtonL.text = "Daha fazla\ndaha fazla!";
        ShopButtonR.text = "Daha fazla\naltın!";
        BuildingMenuL.text = "Yeni İnşa et";
        BuildingMenuR.text = "Kayıtlı kartı yükle";
        Button3.text = "LİDERLER";
        Button4.text = "BONUSLAR";
        Button5.text = "OYUNLARIMIZ";
        LeadersHeadMain.text = "EN İYİ OYUNCULAR";
        LeadersHead1.text = "EN İYİ İNŞAATÇILAR";
        LeadersHead2.text = "EN İYİ YIKICILAR";
        LeadersHead3.text = "EN İYİ DESTEK";
        LeadersDesc1.text = "Mümkün olduğunca çok nesne oluşturun!";
        LeadersDesc2.text = "Yapabileceğiniz her şeyi yok edin!";
        LeadersDesc3.text = "Geliştiriciyi bağışla destekleyin!";
        MenuButton1.text = "Menü";
        MenuButton2.text = "Menü";
        PromoAsk.text = "Kanalımıza abone olun ve orada bulduğunuz promosyon kodunu girin";
        PlaceHolder.text = "Kodu girin...";
        TelegramButton.text = "Telegram";
        TakeButton.text = "Al";
        PromoMessage1.text = "Promosyon Kodu başarıyla girildi";
        PromoMessage2.text = "Promosyon Kodu zaten kullanılıyor";
        PromoMessage3.text = "Böyle bir promosyon kodu yok";
    }
}
