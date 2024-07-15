using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourBlock : Block
{
    Vector3 BasePosition;
    Quaternion BaseRotation;
    public SaveParkourBlockData saveParkourBlockData;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GetComponent<IMoveableParkour>() != null)
            {
                bool Is = GetComponent<IMoveableParkour>().IsFrozen;
              GetComponent<IMoveableParkour>().IsFrozen = !Is;
            }
             
        }
    }
    [ContextMenu("AddToSerializeList")]
    private void AddToSerializeList()
    {
        AddBlockToSaveList();
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
    private void OnEnable()
    {
        if(GetComponent<IMoveableParkour>() !=null)
        GetComponent<IMoveableParkour>().IsFrozen = true;
    }
}
