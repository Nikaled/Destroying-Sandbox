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
    private bool Phase6; // Player Placed Animal
    private bool Phase7; // Player Switched State
    public string PhaseName1 /*= "PhaseName1"*/;
    public string PhaseName2 /*= "PhaseName2"*/;
    public string PhaseName3 /*= "PhaseName3"*/;
    public string PhaseName4 /*= "PhaseName4"*/;
    public string PhaseName5 /*= "PhaseName5"*/;
    public string PhaseName6 /*= "PhaseName6"*/;
    public string PhaseName7 /*= "PhaseName7"*/;
    [SerializeField] TutorialPhaseBorders[] PhaseBorders;
    public bool AnimalZoneReached;
    public bool AnimalPlaced;
    public GameObject Phase4Objects;
    public GameObject Phase5Objects;
    public GameObject Phase6Objects;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PhasesNames = new string[] { PhaseName1, PhaseName2, PhaseName3, PhaseName4, PhaseName5, PhaseName6, PhaseName7 };
        Phases = new bool[] {Phase1, Phase2, Phase3, Phase4, Phase5, Phase6, Phase7 };
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
                            Debug.Log("ActivatedPhaseNumber:" + i+1);
                            OnPhaseCompleted(i);
                        }
                    }
                }
                else
                {
                    Phases[0] = true;
                    Debug.Log("ActivatedPhase:" + PhasesNames[i].ToString());
                }
            }
        }
    }
    private void OnPhaseCompleted(int PhaseIndex)
    {
        switch (PhaseIndex)
        {
            case 0:
                break;
            case 1:
                PhaseBorders[0].UnlockNewPhasePath();
                break;
            case 2:
                PhaseBorders[1].UnlockNewPhasePath();
                Phase4Objects.SetActive(true);
                break;
            case 3:
                CycleManager.instance.ActivateDestroyingPhase();
                break;
            case 4:
                PhaseBorders[3].UnlockNewPhasePath();
                Phase6Objects.SetActive(true);
                break;
            case 5:
                AnimalPlaced = true;
                break;
            case 6:
                AnimalPlaced = true;
                break;
        }
    }

    private void Update()
    {
        if(Player.instance.animationPlayer.IsMoving == true)
        {
            if(Phases[0] == false)
            {
            OpenPhase(PhaseName1);
            }
        }
    }
}
