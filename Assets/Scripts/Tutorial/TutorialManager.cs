using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private bool[] Phases;
    private string[] PhasesNames;
    private bool Phase1; // WASD
    private bool Phase2; // 1-0 block selected
    private bool Phase3; // Block Placed
    private bool Phase4; // Destroy Showed
    private bool Phase5; // Player Destoryed block
    private bool Phase6; // Player Switched State
    private bool Phase7; // Player Finished Tutorial
    public string PhaseName1;
    public string PhaseName2;
    public string PhaseName3;
    public string PhaseName4;
    public string PhaseName5;
    public string PhaseName6;
    public string PhaseName7;
    [SerializeField] TutorialPhaseBorders[] PhaseBorders;
    public bool AnimalZoneReached;
    public bool AbleToChangeMode;
    public bool AbleToEndTutorial;
    public GameObject Phase4Objects;
    public GameObject Phase5Objects;
    public GameObject Phase6Objects;
    public GameObject Phase7Objects;
    public GameObject[] PhasesCanvases;
    [SerializeField] TutorialLocalization TutorLoc;
    public GameObject GoForwardText;
    public GameObject TutorialPhaseText;
    [SerializeField] GameObject BlockPanel;
    [SerializeField] GameObject PlaceBlockButton;
    [SerializeField] GameObject ChangeModeButton;
    [SerializeField] Transform TutorialGoForwPhase4Pos;
    private Vector3 DefaultGoForwPos;
    GameObject currentPulsingObject;
    IEnumerator PulseCor;
    [SerializeField] Image[] DoButtonImages;
    private void Awake()
    {
        instance = this;
    }
    private IEnumerator Pulsing(Transform PulseObj)
    {
        while (true)
        {

            PulseObj.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.75f).OnKill(()=> PulseObj.DOScale(new Vector3(1f, 1f, 1f), 0.75f));
            //PulseObj.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 0.75f).OnKill(()=> PulseObj.DOScale(new Vector3(1f, 1f, 1f), 0.75f));
            yield return new WaitForSeconds(1.5f);
            //PulseObj.DOScale(new Vector3(1f, 1f, 1f), 0.75f).OnKill(() => PulseObj.DOScale(new Vector3(1f, 1f, 1f), 0.1f));
            //yield return new WaitForSeconds(0.8f);
        }
    }
    void Start()
    {
        DefaultGoForwPos = GoForwardText.transform.position;
        PhasesNames = new string[] { PhaseName1, PhaseName2, PhaseName3, PhaseName4, PhaseName5, PhaseName6, PhaseName7 };
        Phases = new bool[] { Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7 };
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(false);
        }
        GoForwardText.SetActive(false);
        CanvasManager.instance.ShowBiggerButtons(false);
    }

    public void OpenPhase(string PhaseName)
    {
        for (int i = 0; i < PhasesNames.Length; i++)
        {
            if (PhasesNames[i] == PhaseName)
            {
                if (i - 1 >= 0)
                {
                    if (Phases[i - 1] == true)
                    {
                        if (Phases[i] == false)
                        {
                            Phases[i] = true;
                            Debug.Log("ActivatedPhase:" + PhasesNames[i].ToString());
                            Debug.Log("ActivatedPhaseNumber:" + i + 1);
                            OnPhaseCompleted(i);
                        }
                    }
                }
                else
                {
                    Phases[0] = true;
                    OnPhaseCompleted(i);
                    Debug.Log("ActivatedPhase:" + PhasesNames[i].ToString());
                }
            }
        }
    }

    public void OnBorderCompleted(TutorialPhaseBorders.PhaseBorders BorderEnum) 
    {
        TutorialPhaseText.SetActive(true);
        GoForwardText.SetActive(false);

        switch (BorderEnum)
        {
            case TutorialPhaseBorders.PhaseBorders.PhaseBorderBlockOnPanelSelected:
                PulseCor = Pulsing(PlaceBlockButton.transform);
                StartCoroutine(PulseCor);
                for (int i = 0; i < DoButtonImages.Length; i++)
                {
                    DoButtonImages[i].color = Color.yellow;
                }
                break;
            case TutorialPhaseBorders.PhaseBorders.PhaseBorderWeaponDemonstrated:
                   OpenPhase(PhaseName4);
                break;
            case TutorialPhaseBorders.PhaseBorders.PhaseBorderWeaponUsed:
                CycleManager.instance.ActivateBuildingPhase();
                CanvasManager.instance.ShowBiggerButtons(true);
                AnimalZoneReached = true;
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(true);
                }

                PulseCor = Pulsing(ChangeModeButton.transform);
                StartCoroutine(PulseCor);
                break;
           
            case TutorialPhaseBorders.PhaseBorders.PhaseBorderBlockPlaced:
                GoForwardText.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void OnPhaseCompleted(int PhaseIndex)
    {
        TutorialPhaseText.SetActive(false);
        if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted == null)
        {
            Geekplay.Instance.PlayerData.TutorialPhasesCompleted = new bool[6];
        }
        if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted.Length == 0)
        {
            Geekplay.Instance.PlayerData.TutorialPhasesCompleted = new bool[6];
        }

        switch (PhaseIndex)
        {
            case 0:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[0] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[0] = true;
                    Analytics.instance.SendEvent("Tutorial_1_PhaseCompleted_Movement");
                }
                    TutorialPhaseText.SetActive(true);

                PulseCor = Pulsing(BlockPanel.transform);
                currentPulsingObject = BlockPanel;
                StartCoroutine(PulseCor);

                break;
            case 1:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[1] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[1] = true;
                    Analytics.instance.SendEvent("Tutorial_2_PhaseCompleted_BlockSelected");
                }
                PhaseBorders[0].UnlockNewPhasePath();

                StopCoroutine(PulseCor);
                DOTween.Kill(BlockPanel);
                //BlockPanel.transform.DOScale(new Vector3(1, 1, 1), 0);
              

                GoForwardText.SetActive(true);
                break;
            case 2:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[2] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[2] = true;
                    Analytics.instance.SendEvent("Tutorial_3_PhaseCompleted_BlockPlaced");
                }
                PhaseBorders[1].UnlockNewPhasePath();
                Phase4Objects.SetActive(true);
                TutorialPhaseText.SetActive(true);
                GoForwardText.SetActive(true);
                GoForwardText.transform.position = TutorialGoForwPhase4Pos.position;
                StopCoroutine(PulseCor);
                DOTween.Kill(PlaceBlockButton);
                //PlaceBlockButton.transform.DOScale(new Vector3(1, 1, 1), 0);

                for (int i = 0; i < DoButtonImages.Length; i++)
                {
                    DoButtonImages[i].color = Color.white;
                }

                break;
            case 3:
                CycleManager.instance.ActivateDestroyingPhase();
                TutorialPhaseText.SetActive(true);

                PulseCor = Pulsing(PlaceBlockButton.transform);
                StartCoroutine(PulseCor);
                for (int i = 0; i < DoButtonImages.Length; i++)
                {
                    DoButtonImages[i].color = Color.yellow;
                }
                break;
            case 4:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[4] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[4] = true;
                    Analytics.instance.SendEvent("Tutorial_5_PhaseCompleted_BlockDestroyed");
                }
                GoForwardText.transform.position = DefaultGoForwPos;
                StopCoroutine(PulseCor);
                DOTween.Kill(PlaceBlockButton);
                for (int i = 0; i < DoButtonImages.Length; i++)
                {
                    DoButtonImages[i].color = Color.white;
                }
                //PlaceBlockButton.transform.DOScale(new Vector3(1, 1, 1), 0);

                GoForwardText.SetActive(true);
                PhaseBorders[3].UnlockNewPhasePath();
                Phase6Objects.SetActive(true);
                AbleToChangeMode = true;
                break;
            case 5:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[5] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[5] = true;
                    Analytics.instance.SendEvent("Tutorial_6_PhaseCompleted_StateSwitched");
                }
                StopCoroutine(PulseCor);
                DOTween.Kill(ChangeModeButton);
                //ChangeModeButton.transform.DOScale(new Vector3(1, 1, 1), 0);

                GoForwardText.SetActive(true);
                GoForwardText.transform.position = TutorialGoForwPhase4Pos.position;
                Phase7Objects.SetActive(true);
                AbleToEndTutorial = true;
                TutorialPhaseText.SetActive(true);
                break;
        }
        Geekplay.Instance.Save();
        TutorLoc.SetNewText(PhaseIndex+1);
    }

    private void Update()
    {
        if (Player.instance.animationPlayer.IsMoving == true)
        {
            if (Phases[0] == false)
            {
                OpenPhase(PhaseName1);
            }
        }
    }
}
