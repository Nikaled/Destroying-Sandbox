using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUnitRespawner : MonoBehaviour
{
    [SerializeField] Block[] Phase6Units;
    void Start()
    {
        for (int i = 0; i < Phase6Units.Length; i++)
        {
            if(Phase6Units[i] != null)
            {
                Phase6Units[i].AddBlockToSaveList();
            }
        }
        SerializeBlockManager.instance.SaveBlocks();
    }
}
