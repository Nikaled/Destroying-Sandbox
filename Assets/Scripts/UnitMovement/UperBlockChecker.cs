using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UperBlockChecker : MonoBehaviour
{
    [SerializeField] BoxCollider col;
    [HideInInspector] public List<GameObject> collisions = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("UperBlock collision detected");
        collisions.Add(other.gameObject);
    }

    public void ActivateCollider()
    {
        collisions.Clear();
        col.enabled = true;
    }
    public void DeactivateCollider()
    {
        col.enabled = false;
    }
}
