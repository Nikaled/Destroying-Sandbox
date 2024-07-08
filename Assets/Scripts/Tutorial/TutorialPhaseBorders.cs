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
            if (Phase4Border)
            {
                TutorialManager.instance.OpenPhase(TutorialManager.instance.PhaseName4);
                gameObject.SetActive(false);
            }
            if (Phase5Border)
            {
                CycleManager.instance.ActivateBuildingPhase();
                TutorialManager.instance.AnimalZoneReached = true;
                gameObject.SetActive(false);
            }
        }
    }
}
