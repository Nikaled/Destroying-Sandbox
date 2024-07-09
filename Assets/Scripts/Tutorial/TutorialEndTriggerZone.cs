using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TutorialManager.instance.AbleToEndTutorial)
            {
                CanvasManager.instance.ShowWinMapUI(true);
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted == null)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted = new bool[5];
                }
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted.Length == 0)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted = new bool[5];
                }
                if (Geekplay.Instance.PlayerData.TutorialBordersCompleted[4] == false)
                {
                    Geekplay.Instance.PlayerData.TutorialBordersCompleted[4] = true;
                    Analytics.instance.SendEvent("Tutorial_PhaseCompleted_EndTutorial");
                    Geekplay.Instance.Save();
                }
            }
        }
    }
}
