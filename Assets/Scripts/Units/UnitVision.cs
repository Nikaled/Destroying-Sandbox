using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    [SerializeField] UnitMovement Controller;
   
    private void OnTriggerEnter(Collider other)
    {
        if(Controller.DestroyPhaseStarted == false)
        {
            return;
        }
        if (other.CompareTag("Unit"))
        {
           var EnemyUM =  other.GetComponent<UnitMovement>();
            if(EnemyUM != null)
            {
                if(EnemyUM.Type == Controller.Type)
                {
                    return;
                }
            }
            if(Controller.CurrentEnemy == null)
            {
                Controller.CurrentEnemy = other.gameObject;
                Controller.ResetTimeToFoundEnemy();
            }
        }
    }
}
