using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourBlock : Block
{
    Vector3 BasePosition;
    Quaternion BaseRotation;
    public SaveParkourBlockData saveParkourBlockData;
   
    [ContextMenu("AddToSerializeList")]
    private void AddToSerializeList()
    {
        AddBlockToSaveList();
    }
    private void Start()
    {
        var IParkour = GetComponent<IMoveableParkour>();
        if (IParkour != null)
        {
            IParkour.IsFrozen = false;
            IParkour.Freeze(false);
        }
    }
    public void SetBlockPos()
    {
        BasePosition = transform.position;
        BaseRotation = transform.rotation;
    }
    private void OnDestroy()
    {
        SerializeBlockManager.instance.BlocksOnScene.Remove(this);
    }
    public SaveParkourBlockData GetParkourBlockData()
    {
        return new SaveParkourBlockData(transform.rotation, transform.localScale);
    }
#if UNITY_EDITOR
    private void OnEnable()
    {
        if(CycleManager.instance.currentPhase == CycleManager.Phase.Building)
        {
            var IParkour = GetComponent<IMoveableParkour>();
            if (IParkour != null)
            {
                bool Is = IParkour.IsFrozen;
                IParkour.IsFrozen = !Is;
                //IParkour.Freeze(Is);
            }
        }  
    }
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
