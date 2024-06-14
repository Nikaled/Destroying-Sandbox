using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BlockDestroyingAnimation : MonoBehaviour
{
    public GameObject[] smallBlocks;
    private float AnimationTime = 0.2f;
    private float DelayBeforeDisappearing = 1f;
    private float DelayInCycle = 0.1f;
    public void BlockDisappearing()
    {
        StartCoroutine(BlockDisappearingCor());
    }
    private IEnumerator BlockDisappearingCor()
    {
        yield return new WaitForSeconds(DelayBeforeDisappearing);
        for (int i = 0; i < smallBlocks.Length; i++)
        {
            if (smallBlocks[i] != null)
            {
                int currentIndex = i;
                smallBlocks[i].transform.DOScale(0, AnimationTime).SetEase(Ease.InCirc).OnComplete(()=> DestroySmallBlock(currentIndex));
                yield return new WaitForSeconds(DelayInCycle);
            }
        }
    }
    private void DestroySmallBlock(int index)
    {
        Destroy(smallBlocks[index]);
        if (index == smallBlocks.Length - 1)
        {
            Destroy(gameObject);
        }
    }

}
