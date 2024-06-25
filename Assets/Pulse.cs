using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pulse : MonoBehaviour
{
    IEnumerator Start()
    {
        while (true)
        {
            transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 1);
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(new Vector3(1f, 1f, 1f), 1);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
