using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapperaringBlockTrigger : MonoBehaviour
{
    [SerializeField] DisappearingBlock block;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            block.StartDisappearing();
        }
    }
}
