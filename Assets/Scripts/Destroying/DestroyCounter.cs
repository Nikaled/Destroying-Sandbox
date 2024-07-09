using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyCounter : MonoBehaviour
{
    public int DestroyedCurrent
    {
        get { return _destroyedCurrent; }
        set
        {
            _destroyedCurrent = value;
            DestroyBlockCountChanged?.Invoke(value);
        }
    }
    private int _destroyedCurrent;
    [HideInInspector] public int DestroyedMax;
    public Action<int> DestroyBlockCountChanged;
    public Action AllBlockDestroyed;
    public static DestroyCounter instance;
    private void Awake()
    {
        instance = this;
    }
    public void DestroyPhaseStarted()
    {
        DestroyedMax = SerializeBlockManager.instance.BlocksOnScene.Count;
        CanvasManager.instance.DestroyCountChanged(0);
        DestroyedCurrent = 0;
    }
    private IEnumerator DelayToWin()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("All blocks destroy Event");
        AllBlockDestroyed?.Invoke();
        Player.instance.examplePlayer.LockCursor(false);
    }
    public virtual void ObjectDestroyed()
    {
        DestroyedCurrent++;
        if(DestroyedCurrent == DestroyedMax)
        {
            StartCoroutine(DelayToWin());
           
        }
    }
}
