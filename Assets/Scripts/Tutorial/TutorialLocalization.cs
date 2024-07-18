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
        TutorialTextPhase1.text = "��������� �������� � ����� ����� ������ ��� ������������";
        TutorialTextPhase2.text = "����� �� ������ ������ �����, ����� ������� ������ ����";
        TutorialTextPhase3.text = "����� �� ������ ������, ����� ��������� ���� � ����";
        TutorialTextPhase4.text = "� ���� ���� ����� �� ������ �������, �� � ���������! ��� ������ � �������� ���!";
        TutorialTextPhase5.text = "������������ ����� �������� � ������� ������ � ��������� �����!";
        TutorialTextPhase6.text = "����� ������ ��� ������� ������ ��� �������� � ����� ���������� � �������� ������ ������!";
        TutorialTextPhase7.text = "����� � ����, ����� ��������� ��������. �������� ����!";
    }
    private void RuPCLocalization()
    {
        TutorialTextPhase1.text = "��������� [W],[A],[S],[D] ��� ������";
        TutorialTextPhase2.text = "����� �� ���������� ����� �� 1 �� 0 ����� ������� ����";
        TutorialTextPhase3.text = "����� ����� ������� ���� � ���� ������ ����� ��������� ����";
        TutorialTextPhase4.text = "� ���� ���� ����� �� ������ �������, �� � ���������! ��� ������ � �������� ���!";
        TutorialTextPhase5.text = "������� ������ �� ����� � ��������� �����! (���������� ��� ������� ������)";
        TutorialTextPhase6.text = "����� <color=orange>[M]</color> ��� �������� � ����� ���������� � �������� ������ ������!";
        TutorialTextPhase7.text = "����� � ����, ����� ��������� ��������. �������� ����!";
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
