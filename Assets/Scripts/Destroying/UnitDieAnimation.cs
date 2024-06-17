using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UnitDieAnimation : MonoBehaviour
{
    public void DieAnimation()
    {
        transform.DOMoveY(transform.position.y + 2, 2).OnComplete(DestroyUnit);
    }
    private void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
