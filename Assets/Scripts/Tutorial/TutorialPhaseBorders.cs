using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPhaseBorders : MonoBehaviour
{
    [SerializeField] BoxCollider BedrockCollider;
    [SerializeField] BoxCollider InvisibleCollider;
    [SerializeField] MeshRenderer BedrockMesh;
    public bool Phase4Border;
    public bool Phase5Border;
    public PhaseBorders ThisBorderPhase;
    public enum PhaseBorders
    {
        PhaseBorderBlockOnPanelSelected,
        PhaseBorderBlockPlaced,
        PhaseBorderWeaponDemonstrated,
        PhaseBorderWeaponUsed,
    }
    public void UnlockNewPhasePath()
    {
        BedrockCollider.enabled = false;
        BedrockMesh.enabled = false;
        InvisibleCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ThisBorderPhase == PhaseBorders.PhaseBorderWeaponDemonstrated)
            {
                TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName4);               
            }
            if (ThisBorderPhase == PhaseBorders.PhaseBorderWeaponUsed)
            {
                CycleManager.instance.ActivateBuildingPhase();
                TutorialManager.instance.AnimalZoneReached = true;
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.ChangePhaseButton.gameObject.SetActive(true);
                }              
            }
            SendAnalyticsPlayerGoThrough();
            gameObject.SetActive(false);
        }
    }

    private void SendAnalyticsPlayerGoThrough()
    {
        if (Geekplay.Instance.PlayerData.TutorialBordersCompleted == null)
        {
            Geekplay.Instance.PlayerData.TutorialBordersCompleted = new bool[5];
        }
        if(Geekplay.Instance.PlayerData.TutorialBordersCompleted.Length == 0)
        {
            Geekplay.Instance.PlayerData.TutorialBordersCompleted = new bool[5];
        }
        switch (ThisBorderPhase)
        {
            case PhaseBorders.PhaseBorderBlockOnPanelSelected:
                if(Geekplay.Instance.PlayerData.TutorialBordersCompleted[0] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted[0] = true;
                Analytics.instance.SendEvent("Tutorial_2_PhaseCompleted_BlockSelected_Border");
                }
            break;                        
            case PhaseBorders.PhaseBorderBlockPlaced:
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted[1] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted[1] = true;
                Analytics.instance.SendEvent("Tutorial_3_PhaseCompleted_BlockPlaced_Border");
                }
            break;                        
            case PhaseBorders.PhaseBorderWeaponDemonstrated:
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted[2] == false)
                {
                Analytics.instance.SendEvent("Tutorial_4_PhaseCompleted_WeaponDemonstrated_Border");
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted[2] = true;
                }
            break;                        
            case PhaseBorders.PhaseBorderWeaponUsed:
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted[3] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted[3] = true;
                Analytics.instance.SendEvent("Tutorial_5_PhaseCompleted_BlockDestroyed_Border");
                }
            break;
        }

        Geekplay.Instance.Save();
    }
}
