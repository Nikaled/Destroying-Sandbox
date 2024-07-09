using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public string PhaseName1 /*= "PhaseName1"*/;
    public string PhaseName2 /*= "PhaseName2"*/;
    public string PhaseName3 /*= "PhaseName3"*/;
    public string PhaseName4 /*= "PhaseName4"*/;
    public string PhaseName5 /*= "PhaseName5"*/;
    public string PhaseName6 /*= "PhaseName6"*/;
    public string PhaseName7 /*= "PhaseName7"*/;
    [SerializeField] TutorialPhaseBorders[] PhaseBorders;
    public bool AnimalZoneReached;
    public bool AbleToChangeMode;
    public bool AbleToEndTutorial;
    public GameObject Phase4Objects;
    public GameObject Phase5Objects;
    public GameObject Phase6Objects;
    public GameObject Phase7Objects;
    public GameObject[] PhasesCanvases;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PhasesNames = new string[] { PhaseName1, PhaseName2, PhaseName3, PhaseName4, PhaseName5, PhaseName6, PhaseName7 };
        Phases = new bool[] { Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7 };
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(false);
        }
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
                PhasesCanvases[i].SetActive(false);
                if (i + 1 < PhasesCanvases.Length)
                {
                    PhasesCanvases[i + 1].SetActive(true);
                }
            }
        }
    }
    private void OnPhaseCompleted(int PhaseIndex)
    {
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
                break;
            case 1:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[1] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[1] = true;
                    Analytics.instance.SendEvent("Tutorial_2_PhaseCompleted_BlockSelected");
                }
                PhaseBorders[0].UnlockNewPhasePath();
                break;
            case 2:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[2] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[2] = true;
                    Analytics.instance.SendEvent("Tutorial_3_PhaseCompleted_BlockPlaced");
                }
                PhaseBorders[1].UnlockNewPhasePath();
                Phase4Objects.SetActive(true);
                break;
            case 3:
                CycleManager.instance.ActivateDestroyingPhase();
                break;
            case 4:
                if (Geekplay.Instance.PlayerData.TutorialPhasesCompleted[4] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialPhasesCompleted[4] = true;
                    Analytics.instance.SendEvent("Tutorial_5_PhaseCompleted_BlockDestroyed");
                }
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
                Phase7Objects.SetActive(true);
                AbleToEndTutorial = true;
                break;
        }
        Geekplay.Instance.Save();
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
