using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialLocalization : MonoBehaviour
{
     string TutorialTextPhase1;
     string TutorialTextPhase2;
     string TutorialTextPhase3;
     string TutorialTextPhase4;
     string TutorialTextPhase5;
     string TutorialTextPhase6;
     string TutorialTextPhase7;
    [SerializeField] TextMeshProUGUI WinUIAsk;
    [SerializeField] TextMeshProUGUI WinUINo;
    [SerializeField] TextMeshProUGUI WinUIYes;
    [SerializeField] TextMeshProUGUI CurrentTutorialText;
    [SerializeField] TextMeshProUGUI GoForwardText;
    [HideInInspector] public string[] TutorialTexts;
    private int currentPhaseIndex;
    private void Start()
    {
        if (Geekplay.Instance.language == "ru")
        {
            if (Geekplay.Instance.mobile)
            {
                RuMobileLocalization();
            }
            else
            {
                RuPCLocalization();
            }
        }
        if (Geekplay.Instance.language == "en")
        {
            if (Geekplay.Instance.mobile)
            {
                EnMobileLocalization();
            }
            else
            {
                EnPCLocalization();
            }
        }
        if (Geekplay.Instance.language == "tr")
        {
            if (Geekplay.Instance.mobile)
            {
                TrMobileLocalization();
            }
            else
            {
                TrPCLocalization();
            }
        }

        TutorialTexts = new string[7] { TutorialTextPhase1, TutorialTextPhase2, TutorialTextPhase3, TutorialTextPhase4, TutorialTextPhase5, TutorialTextPhase6, TutorialTextPhase7 };
        CurrentTutorialText.text = TutorialTexts[0];
    }
    public void SetNewText(int PhaseIndex)
    {
        if (PhaseIndex < TutorialTexts.Length)
        {
            CurrentTutorialText.text = TutorialTexts[PhaseIndex];
        }
    }
    public void CurrentPhaseCompleted()
    {

    }
    private void RuMobileLocalization()
    {
        TutorialTextPhase1 = "Используй <color=orange>джойстик</color> в левой части экрана для передвижения";
        TutorialTextPhase2 = "Нажми на <color=orange>панель блоков снизу</color>, чтобы выбрать нужный блок";
        TutorialTextPhase3 = "Нажми на <color=orange>кнопку справа</color>, чтобы поставить блок в кирпичной зоне";
        TutorialTextPhase4 = "В этой игре можно не только строить, но и разрушать!";
        TutorialTextPhase5 = "А теперь попробуй сам! Переключайся между оружиями с помощью <color=orange>панели снизу</color> и уничтожай блоки!";
        TutorialTextPhase6 = "Нажми <color=orange>кнопку над панелью блоков</color> для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7 = "<color=orange>Зайди в портал</color> в конце дороги, чтобы закончить обучение. Приятной игры!";

        WinUIAsk.text = "Закончить обучение?";
        WinUINo.text = "НЕТ";
        WinUIYes.text = "ДА";
        GoForwardText.text = "Иди далее";
    }
    private void RuPCLocalization()
    {
        TutorialTextPhase1 = "Используй <color=orange>[W]</color>,<color=orange>[A]</color>,<color=orange>[S]</color>,<color=orange>[D]</color> для ходьбы";
        TutorialTextPhase2 = "Нажми на клавиатуре цифры от <color=orange>[1]</color> до <color=orange>[0]</color>, чтобы выбрать блок";
        TutorialTextPhase3 = "Нажми <color=orange> левой кнопкой мыши</color> в кирпичной зоне, чтобы поставить блок";
        TutorialTextPhase4 = "В этой игре можно не только строить, но и разрушать!";
        TutorialTextPhase5 = "А теперь попробуй сам! Выбирай оружие на цифры от <color=orange>[1]</color> до <color=orange>[0]</color> и уничтожай блоки! (Инструкции над панелью оружия)";
        TutorialTextPhase6 = "Нажми <color=orange>[M]</color> для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7 = "<color=orange>Зайди в портал</color> в конце дороги, чтобы закончить обучение. Приятной игры!";

        WinUIAsk.text = "Закончить обучение?";
        WinUINo.text = "НЕТ";
        WinUIYes.text = "ДА";
        GoForwardText.text = "Иди далее";
    }
    private void EnMobileLocalization()
    {
        TutorialTextPhase1 = "Use the <color=orange>joystick</color> on the left side of the screen to move around";
        TutorialTextPhase2 = "Click on the <color=orange>block panel from below</color> to select the desired block";
        TutorialTextPhase3 = "Click on the <color=orange>button on the right</color> to place a block in the brick zone";
        TutorialTextPhase4 = "In this game you can not only build, but also destroy!";
        TutorialTextPhase5 = "Now try it yourself! Switch between weapons using <color=orange>the bottom panel</color> and destroy blocks!";
        TutorialTextPhase6 = "Press the <color=orange>button above the block panel</color> to switch to destruction mode and the animals will start walking!";
        TutorialTextPhase7 = "<color=orange>Enter the portal</color> at the end of the road to finish your training. Have a nice game!";

        WinUIAsk.text = "End tutorial?";
        WinUINo.text = "NO";
        WinUIYes.text = "YES";
        GoForwardText.text = "Go forward";
    }
    private void EnPCLocalization()
    {
        TutorialTextPhase1 = "Use <color=orange>[W]</color>,<color=orange>[A]</color>,<color=orange>[S]</color>,<color=orange>[D]</color > for walking";
        TutorialTextPhase2 = "Press the numbers from <color=orange>[1]</color> to <color=orange>[0]</color> on the keyboard to select a block";
        TutorialTextPhase3 = "Click <color=orange>left mouse button</color> in the brick zone on the right to place a block";
        TutorialTextPhase4 = "In this game you can not only build, but also destroy!";
        TutorialTextPhase5 = "Now try it yourself! Choose weapons based on numbers from <color=orange>[1]</color> to <color=orange>[0]</color> and destroy blocks! (Instructions above the weapon panel)";
        TutorialTextPhase6 = "Press <color=orange>[M]</color> to switch to destruction mode and the animals will start walking!";
        TutorialTextPhase7 = "<color=orange>Enter the portal</color> at the end of the road to finish your training. Have a nice game!";

        WinUIAsk.text = "End tutorial?";
        WinUINo.text = "NO";
        WinUIYes.text = "YES";
        GoForwardText.text = "Go forward";
    }
    private void TrMobileLocalization()
    {
        TutorialTextPhase1 = "Hareket etmek için ekranın sol tarafındaki <color=orange>joystick'i</color> kullan";
        TutorialTextPhase2 = "İstediğiniz bloğu seçmek için alttan <color=orange>blok paneline tıklayın";
        TutorialTextPhase3 = "Bloğu bölgeye yerleştirmek için sağdaki <color=orange>düğmesine tıklayın";
        TutorialTextPhase4 = "Bu oyunda sadece inşa etmekle kalmaz, aynı zamanda yok edebilirsiniz!";
        TutorialTextPhase5 = "Şimdi kendin dene! Alttaki <color=orange>panelini kullanarak</color> silahlar arasında geçiş yap ve blokları yok et!";
        TutorialTextPhase6 = "İmha moduna geçmek için <color=orange>blok panelinin üzerindeki düğmeye</color> bas ve hayvanlar yürümeye başlayacak!";
        TutorialTextPhase7 = "<color=orange>Eğitiminizi tamamlamak için yolun sonundaki</color> kemerine gir. Oyunun tadını çıkar!";

        WinUIAsk.text = "Eğitiminizi bitirmek mi?";
        WinUINo.text = "HAYIR";
        WinUIYes.text = "EVET";
        GoForwardText.text = "Devam et";
    }
    private void TrPCLocalization()
    {
        TutorialTextPhase1 = "Kullan <color=orange>[W]</color>,<color=orange>[A]</color>,<color=orange>[S]</color>,<color=orange>[D]</color > yürümek için";
        TutorialTextPhase2 = "Bloğu seçmek için klavyedeki <color=orange>[1]</color> ile <color=orange>[0]</color> arasındaki sayılara basın";
        TutorialTextPhase3 = "Bloğu yerleştirmek için sağdaki <color=orange>alana sol fare tuşu ile tuşuna basın</color>";
        TutorialTextPhase4 = "Bu oyunda sadece inşa etmekle kalmaz, aynı zamanda yok edebilirsiniz!";
        TutorialTextPhase5 = "Şimdi kendin dene! Silahları <color=orange>[1]</color> ile <color=orange>[0]</color> arasındaki sayılara göre seçin ve blokları yok edin! (Silah panelinin üzerindeki talimatlar)";
        TutorialTextPhase6 = "Yıkım moduna geçmek için <color=orange>[M]</color> tuşuna basarsanız hayvanlar yürümeye başlar!";
        TutorialTextPhase7 = "Eğitiminizi tamamlamak için yolun sonundaki kemere gir. Oyunun tadını çıkar!";

        WinUIAsk.text = "Eğitiminizi bitirmek mi?";
        WinUINo.text = "HAYIR";
        WinUIYes.text = "EVET";
        GoForwardText.text = "Devam et";
    }
}
