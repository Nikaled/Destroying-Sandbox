using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NotFlamingUI : MonoBehaviour
{
    public static NotFlamingUI instance;
    public GameObject NotFlamingWarning;
    Sequence ShowSeq;
    void Start()
    {
         
        instance = this; 
        //NotFlamingWarning.SetActive(true);
       
        //ShowSeq.Append(NotFlamingWarning.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0));
        //ShowSeq.Append(NotFlamingWarning.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f));
        //ShowSeq.Append(NotFlamingWarning.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
        //ShowSeq.Append(NotFlamingWarning.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f)).OnComplete(HideWarObj);
        //ShowWarning();
    }
    private void HideWarObj()
    {
        NotFlamingWarning.SetActive(false);
    }
    public void ShowWarning()
    {
        if(ShowSeq != null)
        {
            if (ShowSeq.IsPlaying())
            {
                return;
            }
        }
        NotFlamingWarning.SetActive(true);
        //ShowSeq.Restart();
        ShowSeq = DOTween.Sequence();
        ShowSeq.Pause();
        var t1 = NotFlamingWarning.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0);
        var t2 = NotFlamingWarning.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.4f);
        var t3 = NotFlamingWarning.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        var t4 = NotFlamingWarning.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f).SetDelay(2f).OnComplete(HideWarObj);
        ShowSeq.Append(t1);
        ShowSeq.Append(t2);
        ShowSeq.Append(t3);
        ShowSeq.Append(t4);
        ShowSeq.Play();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ShowWarning();
        }
    }
}
