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
        TutorialTextPhase1 = "Используй джойстик в левой части экрана для передвижения";
        TutorialTextPhase2 = "Нажми на панель блоков снизу, чтобы выбрать нужный блок";
        TutorialTextPhase3 = "Нажми на кнопку справа, чтобы поставить блок в зоне";
        TutorialTextPhase4 = "В этой игре можно не только строить, но и разрушать! Иди вперед и попробуй сам!";
        TutorialTextPhase5 = "Переключайся между оружиями с помощью панели и уничтожай блоки!";
        TutorialTextPhase6 = "Нажми кнопку над панелью блоков для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7 = "Зайди в арку в конце дороги, чтобы закончить обучение. Приятной игры!";
    }
    private void RuPCLocalization()
    {
        TutorialTextPhase1 = "Используй [W],[A],[S],[D] для ходьбы";
        TutorialTextPhase2 = "Нажми на клавиатуре цифры от 1 до 0 чтобы выбрать блок";
        TutorialTextPhase3 = "Нажми левой кнопкой мыши в зоне справа чтобы поставить блок";
        TutorialTextPhase4 = "В этой игре можно не только строить, но и разрушать! Иди вперед и попробуй сам!";
        TutorialTextPhase5 = "Выбирай оружие на цифры и уничтожай блоки! (Инструкции над панелью оружия)";
        TutorialTextPhase6 = "Нажми <color=orange>[M]</color> для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7 = "Зайди в арку в конце дороги, чтобы закончить обучение. Приятной игры!";
    }
    private void EnMobileLocalization()
    {

    }
    private void EnPCLocalization()
    {

    }
    private void TrMobileLocalization()
    {

    }
    private void TrPCLocalization()
    {

    }
}
