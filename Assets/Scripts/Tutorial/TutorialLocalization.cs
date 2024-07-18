using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialLocalization : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TutorialTextPhase1;
    [SerializeField] TextMeshProUGUI TutorialTextPhase2;
    [SerializeField] TextMeshProUGUI TutorialTextPhase3;
    [SerializeField] TextMeshProUGUI TutorialTextPhase4;
    [SerializeField] TextMeshProUGUI TutorialTextPhase5;
    [SerializeField] TextMeshProUGUI TutorialTextPhase6;
    [SerializeField] TextMeshProUGUI TutorialTextPhase7;
    [SerializeField] TextMeshProUGUI WinUIAsk;
    [SerializeField] TextMeshProUGUI WinUINo;
    [SerializeField] TextMeshProUGUI WinUIYes;
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
    }
    private void RuMobileLocalization()
    {
        TutorialTextPhase1.text = "Используй джойстик в левой части экрана для передвижения";
        TutorialTextPhase2.text = "Нажми на панель блоков снизу, чтобы выбрать нужный блок";
        TutorialTextPhase3.text = "Нажми на кнопку справа, чтобы поставить блок в зоне";
        TutorialTextPhase4.text = "В этой игре можно не только строить, но и разрушать! Иди вперед и попробуй сам!";
        TutorialTextPhase5.text = "Переключайся между оружиями с помощью панели и уничтожай блоки!";
        TutorialTextPhase6.text = "Нажми кнопку над панелью блоков для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7.text = "Зайди в арку, чтобы закончить обучение. Приятной игры!";
    }
    private void RuPCLocalization()
    {
        TutorialTextPhase1.text = "Используй [W],[A],[S],[D] для ходьбы";
        TutorialTextPhase2.text = "Нажми на клавиатуре цифры от 1 до 0 чтобы выбрать блок";
        TutorialTextPhase3.text = "Нажми левой кнопкой мыши в зоне справа чтобы поставить блок";
        TutorialTextPhase4.text = "В этой игре можно не только строить, но и разрушать! Иди вперед и попробуй сам!";
        TutorialTextPhase5.text = "Выбирай оружие на цифры и уничтожай блоки! (Инструкции над панелью оружия)";
        TutorialTextPhase6.text = "Нажми <color=orange>[M]</color> для перехода в режим разрушения и животные начнут ходить!";
        TutorialTextPhase7.text = "Зайди в арку, чтобы закончить обучение. Приятной игры!";
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
