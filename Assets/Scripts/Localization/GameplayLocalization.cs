using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayLocalization : MonoBehaviour
{
    [Header("Destroyed")]
    [SerializeField] TextMeshProUGUI DestroyedBarHeader;
    [Header("WinMap")]
    [SerializeField] TextMeshProUGUI YouWon;
    [SerializeField] TextMeshProUGUI TryAgain;
    [SerializeField] TextMeshProUGUI ToMenu;
    [Header("BuildingMenu")]
    [SerializeField] TextMeshProUGUI BuildingMenu1;
    [SerializeField] TextMeshProUGUI BuildingMenu2;
    [SerializeField] TextMeshProUGUI BuildingMenu3;

    [Header("UnlockWeapon")]
    [SerializeField] TextMeshProUGUI WeaponClosed;
    [SerializeField] TextMeshProUGUI UnlockOneTimeButton;
    [SerializeField] TextMeshProUGUI UnlockForeverButton;
    [Header("BuildingInstruction")]
    [SerializeField] TextMeshProUGUI Build;
    [SerializeField] TextMeshProUGUI Delete;
    [SerializeField] TextMeshProUGUI DestroyMode;
    [SerializeField] TextMeshProUGUI Inventory;
    [SerializeField] TextMeshProUGUI InMenu;
    [SerializeField] TextMeshProUGUI Save;
    [Header("PistolInstruction")]
    [SerializeField] TextMeshProUGUI Pistol1;
    [SerializeField] TextMeshProUGUI Pistol2;
    [SerializeField] TextMeshProUGUI Pistol3;
    [Header("MolotovInstruction")]
    [SerializeField] TextMeshProUGUI Molotov1;
    [SerializeField] TextMeshProUGUI Molotov2;
    [SerializeField] TextMeshProUGUI Molotov3;
    [Header("FlameThrowInstruction")]
    [SerializeField] TextMeshProUGUI FlameThrow1;
    [SerializeField] TextMeshProUGUI FlameThrow2;
    [SerializeField] TextMeshProUGUI FlameThrow3;
    [Header("PlaneInstruction")]
    [SerializeField] TextMeshProUGUI Plane1;
    [SerializeField] TextMeshProUGUI Plane2;
    [SerializeField] TextMeshProUGUI Plane3;
    [SerializeField] TextMeshProUGUI Plane4;
    [Header("LightningInstruction")]
    [SerializeField] TextMeshProUGUI Lightning1;
    [SerializeField] TextMeshProUGUI Lightning2;
    [SerializeField] TextMeshProUGUI Lightning3;
    [Header("DynamiteInstruction")]
    [SerializeField] TextMeshProUGUI DynamiteInstruction1;
    [SerializeField] TextMeshProUGUI DynamiteInstruction2;
    [SerializeField] TextMeshProUGUI DynamiteInstruction3;
    [SerializeField] TextMeshProUGUI DynamiteInstruction4;
    [SerializeField] TextMeshProUGUI DynamiteInstruction5;
    [Header("CarInstruction")]
    [SerializeField] TextMeshProUGUI Car1;
    [SerializeField] TextMeshProUGUI Car2;
    [SerializeField] TextMeshProUGUI Car3;
    [Header("MeteorInstruction")]
    [SerializeField] TextMeshProUGUI Meteor1;
    [SerializeField] TextMeshProUGUI Meteor2;
    [SerializeField] TextMeshProUGUI Meteor3;
    [Header("PressInstruction")]
    [SerializeField] TextMeshProUGUI Press1;
    [SerializeField] TextMeshProUGUI Press2;
    [SerializeField] TextMeshProUGUI Press3;
    [Header("CreeperInstruction")]
    [SerializeField] TextMeshProUGUI Creeper1;
    [SerializeField] TextMeshProUGUI Creeper2;
    [SerializeField] TextMeshProUGUI Creeper3;

    [Header("PhaseButtons")]
    public TextMeshProUGUI InventoryButton;
    public TextMeshProUGUI BuildPhaseButton;
    public TextMeshProUGUI DestroyPhaseButton;
    [Header("ParkourInsturction")]
    public TextMeshProUGUI ReloadButtonInCorner;
    public TextMeshProUGUI ParkourInstructionR;
    public TextMeshProUGUI ParkourInstructionM;

    public static GameplayLocalization instance;
    private void Awake()
    {
        instance = this;
    }
    public void SetupReloadInstruction()
    {
        ParkourInstructionR.text = $"<color=orange>[M]</color> {TryAgain.text}";
        ReloadButtonInCorner.text = TryAgain.text;
        ParkourInstructionM.text = InMenu.text;
    }
    public void SetupReloadInstructionsOnOnlyDestroyed(bool Is)
    {
        if (Is)
        {
            Pistol2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Molotov2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            FlameThrow2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Plane3.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Lightning2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            DynamiteInstruction4.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Car2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Meteor2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Press2.text = $"<color=orange>[M]</color> {TryAgain.text}";
            Creeper2.text = $"<color=orange>[M]</color> {TryAgain.text}";
        }
        else
        {
            Pistol2.transform.parent.gameObject.SetActive(false);
            Pistol2.transform.parent.gameObject.SetActive(false);
            Molotov2.transform.parent.gameObject.SetActive(false);
            FlameThrow2.transform.parent.gameObject.SetActive(false);
            Plane3.transform.parent.gameObject.SetActive(false);
            Lightning2.transform.parent.gameObject.SetActive(false);
            DynamiteInstruction4.transform.parent.gameObject.SetActive(false);
            Car2.transform.parent.gameObject.SetActive(false);
            Meteor2.transform.parent.gameObject.SetActive(false);
            Press2.transform.parent.gameObject.SetActive(false);
            Creeper2.transform.parent.gameObject.SetActive(false);
        }
    }
    public void SetupInventoryAndChangeModeButtons_PC()
    {
        InventoryButton.text = $"[I] \n {InventoryButton.text}";
        BuildPhaseButton.text = $"[M] \n {BuildPhaseButton.text}";
        DestroyPhaseButton.text = $"[M] \n {DestroyPhaseButton.text}";



    }
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
    private void EnLocalization()
    {
        DestroyedBarHeader.text = "Destroyed:";
        YouWon.text = "You've won!";
        TryAgain.text = "Again";
        ToMenu.text = "To menu";

        BuildingMenu1.text = "Blocks";
        BuildingMenu2.text = "Animals";
        BuildingMenu3.text = "Monsters";

        WeaponClosed.text = "Weapon closed!";
        UnlockOneTimeButton.text = "Unlock 1 time per advertisement";
        UnlockForeverButton.text = "Unlock forever";


        Build.text = "<color=orange>[LMB]</color> Build";
        Delete.text = "<color=orange>[RMB]</color> Delete";
        DestroyMode.text = "<color=orange>[M]</color> Destruction mode";
        Inventory.text = "<color=orange>[I]</color> Inventory";
        InMenu.text = "<color=orange>[Tab]</color> To Menu";
        Save.text = "<color=orange>[K]</color> Saving";
        Pistol1.text = "<color=orange>[LMB]</color> Shoot";
        Pistol2.text = "<color=orange>[M]</color> Building mode";
        Pistol3.text = "<color=orange>[Tab]</color> To Menu";
        Molotov1.text = "<color=orange>[LMB]</color> Throw";
        Molotov2.text = "<color=orange>[M]</color> Building mode";
        Molotov3.text = "<color=orange>[Tab]</color> To Menu";
        FlameThrow1.text = "<color=orange>[hold LMB]</color> Burn";
        FlameThrow2.text = "<color=orange>[M]</color> Building mode";
        FlameThrow3.text = "<color=orange>[Tab]</color> To Menu";
        Plane1.text = "<color=orange>[Mouse]</color> Turns";
        Plane2.text = "<color=orange>[LMB]</color> Shoot";
        Plane3.text = "<color=orange>[M]</color> Building mode";
        Plane4.text = "<color=orange>[Tab]</color> To Menu";
        Lightning1.text = "<color=orange>[LMB]</color> Summon lightning";
        Lightning2.text = "<color=orange>[M]</color> Building mode";
        Lightning3.text = "<color=orange>[Tab]</color> To Menu";
        DynamiteInstruction1.text = "<color=orange>[LMB]</color> Place dynamite";
        DynamiteInstruction2.text = "<color=orange>[RMB]</color> Remove Dynamite";
        DynamiteInstruction3.text = "<color=orange>[T]</color> Detonate dynamite";
        DynamiteInstruction4.text = "<color=orange>[M]</color> Building mode";
        DynamiteInstruction5.text = "<color=orange>[Tab]</color> To Menu";
        Car1.text = "<color=orange>[W,A,S,D]</color> Ride";
        Car2.text = "<color=orange>[M]</color> Building mode";
        Car3.text = "<color=orange>[Tab]</color> To Menu";
        Meteor1.text = "<color=orange>[LMB]</color> Summon a meteorite";
        Meteor2.text = "<color=orange>[M]</color> Building mode";
        Meteor3.text = "<color=orange>[Tab]</color> To Menu";
        Press1.text = "<color=orange>[LMB]</color> To cause a huge press";
        Press2.text = "<color=orange>[M]</color> Building mode";
        Press3.text = "<color=orange>[Tab]</color> To Menu";
        Creeper1.text = "<color=orange>[T]</color> Explode";
        Creeper2.text = "<color=orange>[M]</color> Building mode";
        Creeper3.text = "<color=orange>[Tab]</color> To Menu";
        InventoryButton.text = "INVENTORY";
        BuildPhaseButton.text = "BUILD";
        DestroyPhaseButton.text = "DESTROY";
    }
    private void TrLocalization()
    {
        DestroyedBarHeader.text = "Yok edildi:";
        YouWon.text = "Kazandınız!";
        TryAgain.text = "Yeniden başlat";
        ToMenu.text = "Menüde";

        BuildingMenu1.text = "Bloklar";
        BuildingMenu2.text = "Hayvanlar";
        BuildingMenu3.text = "Canavarlar";

        WeaponClosed.text = "Silahlar kapalı!";
        UnlockOneTimeButton.text = "Reklam başına 1 kez engellemeyi kaldır";
        UnlockForeverButton.text = "Kalıcı olarak engellemeyi kaldır";


        Build.text = "<color=orange>[LKM]</color> İnşa etmek";
        Delete.text = "<color=orange>[RMB]</color> Sil";
        DestroyMode.text = "<color=orange>[M]</color> İmha modu";
        Inventory.text = "<color=orange>[I]</color> Envanter";
        InMenu.text = "<color=orange>[Tab]</color> Menüde";
        Save.text = "<color=orange>[K]</color> Kaydetme";
        Pistol1.text = "<color=orange>[LKM]</color> Ateş etmek";
        Pistol2.text = "<color=orange>[M]</color> İnşaat modu";
        Pistol3.text = "<color=orange>[Tab]</color> Menüde";
        Molotov1.text = "<color=orange>[LKM]</color> Atmak";
        Molotov2.text = "<color=orange>[M]</color> İnşaat modu";
        Molotov3.text = "<color=orange>[Tab]</color> Menüde";
        FlameThrow1.text = "<color=orange>[boyayı kıstırmak]</color> Yakmak";
        FlameThrow2.text = "<color=orange>[M]</color> İnşaat modu";
        FlameThrow3.text = "<color=orange>[Tab]</color> Menüde";
        Plane1.text = "<color=orange>[Fare]</color> Dоnüşler";
        Plane2.text = "<color=orange>[LKM]</color> Ateş etmek";
        Plane3.text = "<color=orange>[M]</color> İnşaat modu";
        Plane4.text = "<color=orange>[Tab]</color> Menüde";
        Lightning1.text = "<color=orange>[LKM]</color> Yıldırımı ara";
        Lightning2.text = "<color=orange>[M]</color> İnşaat modu";
        Lightning3.text = "<color=orange>[Tab]</color> Menüde";
        DynamiteInstruction1.text = "<color=orange>[LKM]</color> Dinamit koy";
        DynamiteInstruction2.text = "<color=orange>[PKM]</color> Dinamiti kaldır";
        DynamiteInstruction3.text = "<color=orange>[T]</color> Dinamiti patlat";
        DynamiteInstruction4.text = "<color=orange>[M]</color> İnşaat modu";
        DynamiteInstruction5.text = "<color=orange>[Tab]</color> Menüde";
        Car1.text = "<color=orange>[W,A,S,D]</color> Gitmek";
        Car2.text = "<color=orange>[M]</color> İnşaat modu";
        Car3.text = "<color=orange>[Tab]</color> Menüde";
        Meteor1.text = "<color=orange>[LKM]</color> Gоktaşı çağırın";
        Meteor2.text = "<color=orange>[M]</color> İnşaat modu";
        Meteor3.text = "<color=orange>[Tab]</color> Menüde";
        Press1.text = "<color=orange>[LKM]</color> Büyük basın çağırın";
        Press2.text = "<color=orange>[M]</color> İnşaat modu";
        Press3.text = "<color=orange>[Tab]</color> Menüde";
        Creeper1.text = "<color=orange>[T]</color> Patlamak için";
        Creeper2.text = "<color=orange>[M]</color> İnşaat modu";
        Creeper3.text = "<color=orange>[Tab]</color> Menüde";
        InventoryButton.text = "ENVANTER";
        BuildPhaseButton.text = "İNŞA ETMEK";
        DestroyPhaseButton.text = "YOK ET";
    }
    private void RuLocalization()
    {
        DestroyedBarHeader.text = "Уничтожено:";
        YouWon.text = "Вы выиграли!";
        TryAgain.text = "Заново";
        ToMenu.text = "В меню";

        BuildingMenu1.text = "Блоки";
        BuildingMenu2.text = "Животные";
        BuildingMenu3.text = "Монстры";

        WeaponClosed.text = "Оружие закрыто!";
        UnlockOneTimeButton.text = "Разблокировать 1 раз за рекламу";
        UnlockForeverButton.text = "Разблокировать навсегда";


        Build.text = "<color=orange>[ЛКМ]</color> Строить";
        Delete.text = "<color=orange>[ПКМ]</color> Удалить";
        DestroyMode.text = "<color=orange>[M]</color> Режим разрушения";
        Inventory.text = "<color=orange>[I]</color> Инвентарь";
        InMenu.text = "<color=orange>[Tab]</color> В Меню";
        Save.text = "<color=orange>[K]</color> Сохранение";
        Pistol1.text = "<color=orange>[ЛКМ]</color> Стрелять";
        Pistol2.text = "<color=orange>[М]</color> Режим строительства";
        Pistol3.text = "<color=orange>[Tab]</color>  В Меню";
        Molotov1.text = "<color=orange>[ЛКМ]</color> Бросить";
        Molotov2.text = "<color=orange>[М]</color> Режим строительства";
        Molotov3.text = "<color=orange>[Tab]</color>  В Меню";
        FlameThrow1.text = "<color=orange>[зажать ЛКМ]</color> Жечь";
        FlameThrow2.text = "<color=orange>[М]</color> Режим строительства";
        FlameThrow3.text = "<color=orange>[Tab]</color>  В Меню";
        Plane1.text = "<color=orange>[Мышь]</color> Повороты";
        Plane2.text = "<color=orange>[ЛКМ]</color> Стрелять";
        Plane3.text = "<color=orange>[М]</color> Режим строительства";
        Plane4.text = "<color=orange>[Tab]</color>  В Меню";
        Lightning1.text = "<color=orange>[ЛКМ]</color> Вызвать молнию";
        Lightning2.text = "<color=orange>[М]</color> Режим строительства";
        Lightning3.text = "<color=orange>[Tab]</color>  В Меню";
        DynamiteInstruction1.text = "<color=orange>[ЛКМ]</color> Поставить динамит";
        DynamiteInstruction2.text = "<color=orange>[ПКМ]</color> Удалить динамит";
        DynamiteInstruction3.text = "<color=orange>[T]</color> Взорвать динамит";
        DynamiteInstruction4.text = "<color=orange>[М]</color> Режим строительства";
        DynamiteInstruction5.text = "<color=orange>[Tab]</color>  В Меню";
        Car1.text = "<color=orange>[W,A,S,D]</color> Ехать";
        Car2.text = "<color=orange>[М]</color> Режим строительства";
        Car3.text = "<color=orange>[Tab]</color>  В Меню";
        Meteor1.text = "<color=orange>[ЛКМ]</color> Вызвать метеорит";
        Meteor2.text = "<color=orange>[М]</color> Режим строительства";
        Meteor3.text = "<color=orange>[Tab]</color>  В Меню";
        Press1.text = "<color=orange>[ЛКМ]</color> Вызвать огромный пресс";
        Press2.text = "<color=orange>[М]</color> Режим строительства";
        Press3.text = "<color=orange>[Tab]</color>  В Меню";
        Creeper1.text = "<color=orange>[T]</color> Взорваться";
        Creeper2.text = "<color=orange>[М]</color> Режим строительства";
        Creeper3.text = "<color=orange>[Tab]</color>  В Меню";
        InventoryButton.text = "ИНВЕНТАРЬ";
        BuildPhaseButton.text = "СТРОИТЬ";
        DestroyPhaseButton.text = "РАЗРУШИТЬ";
    }
}
