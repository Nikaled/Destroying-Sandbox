using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourBlock : Block
{
    Vector3 BasePosition;
    Quaternion BaseRotation;
    float Speed;
    public SaveParkourBlockData saveParkourBlockData;
    public bool InvertMoveCycle;
    [ContextMenu("AddToSerializeList")]
    private void AddToSerializeList()
    {
        AddBlockToSaveList();
    }

    private void Start()
    {
        var IParkour = GetComponent<IMoveableParkour>();
        if (IParkour != null)
            IParkour.SetData(saveParkourBlockData.Speed, saveParkourBlockData.InvertMoveCycle);
        if (SerializeBlockManager.instance.OnlyParkourMap == false)
        {
            if (IParkour != null)
            {
                IParkour.IsFrozen = true;
                IParkour.Freeze(true);
            }
        }
       
        //else
        //{
        //    if (IParkour != null)
        //    {
        //        IParkour.IsFrozen = false;
        //        IParkour.Freeze(false);
        //    }
        //}
    }
    public void SetBlockPos()
    {
        if(BasePosition == new Vector3(0, 0, 0))
        {
            return;
        }
        BasePosition = transform.position;
        BaseRotation = transform.rotation;
    }
    private void OnDestroy()
    {
        SerializeBlockManager.instance.BlocksOnScene.Remove(this);
    }
    public SaveParkourBlockData GetParkourBlockData()
    {
        var AddedData = new SaveParkourBlockData();
        var IParkour = GetComponent<IMoveableParkour>();
        if (IParkour != null)
        {
            AddedData = IParkour.GetData();
        }
        AddedData.rotation = transform.rotation;
        AddedData.Scale = transform.localScale;
        return AddedData;
    }
#if UNITY_EDITOR
    //private void OnEnable()
    //{
    //    if(CycleManager.instance.currentPhase == CycleManager.Phase.Building)
    //    {
    //        var IParkour = GetComponent<IMoveableParkour>();
    //        if (IParkour != null)
    //        {
    //            bool Is = IParkour.IsFrozen;
    //            IParkour.IsFrozen = !Is;
    //            //IParkour.Freeze(Is);
    //        }
    //    }  
    //}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var IParkour = GetComponent<IMoveableParkour>();
            if (IParkour != null)
            {
                //bool Is = IParkour.IsFrozen;
                bool Is = true;
                IParkour.IsFrozen = !Is;
                IParkour.Freeze(Is);
            }

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            var IParkour = GetComponent<IMoveableParkour>();
            if (IParkour != null)
            {
                //bool Is = IParkour.IsFrozen;
                bool Is = false;
                IParkour.IsFrozen = !Is;
                IParkour.Freeze(Is);
            }

        }
    }
#endif
}
