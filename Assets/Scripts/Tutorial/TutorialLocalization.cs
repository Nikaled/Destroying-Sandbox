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
        TutorialTextPhase1 = "��������� �������� � ����� ����� ������ ��� ������������";
        TutorialTextPhase2 = "����� �� ������ ������ �����, ����� ������� ������ ����";
        TutorialTextPhase3 = "����� �� ������ ������, ����� ��������� ���� � ����";
        TutorialTextPhase4 = "� ���� ���� ����� �� ������ �������, �� � ���������!";
        TutorialTextPhase5 = "� ������ �������� ���! ������������ ����� �������� � ������� ������ � ��������� �����!";
        TutorialTextPhase6 = "����� ������ ��� ������� ������ ��� �������� � ����� ���������� � �������� ������ ������!";
        TutorialTextPhase7 = "����� � ���� � ����� ������, ����� ��������� ��������. �������� ����!";
    }
    private void RuPCLocalization()
    {
        TutorialTextPhase1 = "��������� <color=orange>[W]</color>,<color=orange>[A]</color>,<color=orange>[S]</color>,<color=orange>[D]</color> ��� ������";
        TutorialTextPhase2 = "����� �� ���������� ����� �� <color=orange>[1]</color> �� <color=orange>[0]</color>, ����� ������� ����";
        TutorialTextPhase3 = "����� <color=orange> ����� ������� ����</color> � ���� ������ ����� ��������� ����";
        TutorialTextPhase4 = "� ���� ���� ����� �� ������ �������, �� � ���������!";
        TutorialTextPhase5 = "� ������ �������� ���! ������� ������ �� ����� �� <color=orange>[1]</color> �� <color=orange>[0]</color> � ��������� �����! (���������� ��� ������� ������)";
        TutorialTextPhase6 = "����� <color=orange>[M]</color> ��� �������� � ����� ���������� � �������� ������ ������!";
        TutorialTextPhase7 = "����� � ���� � ����� ������, ����� ��������� ��������. �������� ����!";
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
