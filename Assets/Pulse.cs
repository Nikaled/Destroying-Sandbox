using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pulse : MonoBehaviour
{
    private IEnumerator PulsingCor;
    public bool IsUnlocked = true;

    public void StartPulsingIfUnlocked()
    {
        if (PulsingCor != null)
        {
            StopCoroutine(PulsingCor);
            transform.DOScale(new Vector3(1f, 1f, 1f), 0);
        }
        PulsingCor = Pulsing();
        StartCoroutine(PulsingCor);
    }
    public void StopPulsing()
    {
        if (PulsingCor != null)
        {
            StopCoroutine(PulsingCor);
            transform.DOScale(new Vector3(1f, 1f, 1f), 0);
        }
    }
    private IEnumerator Pulsing()
    {
        if (IsUnlocked)
        {
            while (true)
            {

                transform.DOScale(new Vector3(1.15f, 1.15f, 1.15f), 1);
                yield return new WaitForSeconds(0.5f);
                transform.DOScale(new Vector3(1f, 1f, 1f), 1);
                yield return new WaitForSeconds(0.5f);

            }
        }
        else
        {
            yield return null;
        }
    }
    void Start()
    {
        if (IsUnlocked)
        {
            PulsingCor = Pulsing();
            StartCoroutine(PulsingCor);
        }
    }
}
