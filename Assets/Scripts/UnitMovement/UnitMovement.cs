using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitMovement : MonoBehaviour
{
    [SerializeField] CharacterController charController;
    public float Speed = 2f;
    Vector3 CurrentVectorMove;
    [SerializeField] Transform MoveToObj;
    [SerializeField] Transform UpPosTransform;

    [SerializeField] GameObject ChildrenUnit;
    [SerializeField] BoxCollider ForwardCollider;
    [SerializeField] BoxCollider[] BlockCollidersParentAndSides;
    Vector3 ChildrenStartLocalPosition;
    private bool DestroyPhaseStarted;
    private IEnumerator rotatingCor;
    private void Start()
    {
        CycleManager.instance.DestroyingPhaseStarted += OnActivatedDestroyingPhase;
    }
    private void OnActivatedDestroyingPhase()
    {
        for (int i = 0; i < BlockCollidersParentAndSides.Length; i++)
        {
            if(BlockCollidersParentAndSides[i]!=null)
            BlockCollidersParentAndSides[i].enabled = false;
        }
        ChildrenStartLocalPosition = ChildrenUnit.transform.localPosition;
        Sequence WalkSequence = DOTween.Sequence();
        WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenUnit.transform.localPosition + new Vector3(0, 1, 0), 0.15f)).SetEase(Ease.InExpo);
        WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenStartLocalPosition, 1f).SetEase(Ease.InOutExpo));
        WalkSequence.SetLoops(10000);
        StartCoroutine(RandomRotatingCycle());
        ForwardCollider.enabled = true;
        DestroyPhaseStarted = true;
    }
    private void OnDestroy()
    {
        CycleManager.instance.DestroyingPhaseStarted -= OnActivatedDestroyingPhase;

    }
    private void FixedUpdate()
    {
        if(DestroyPhaseStarted == false)
        {
            return;
        }
        CurrentVectorMove = (MoveToObj.position - transform.position).normalized;
        Debug.Log("CurrentVectorMove:" + CurrentVectorMove);
        if(charController.enabled == true)
        {
        charController.Move((CurrentVectorMove - Vector3.up*10)*Time.deltaTime*Speed);
        }
    }
    private IEnumerator RandomRotatingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (charController.enabled == true)
            {
                RotateUnit();
            }
        }
      
    }
    public void RotateUnit()
    {
        charController.enabled = false;
        int RandomDegree = Random.Range(20, 91);
        transform.DOLocalRotate(transform.rotation.eulerAngles + new Vector3(0, RandomDegree, 0), 1).OnComplete(EnCharC);
        void EnCharC()
        {
            charController.enabled = true;
        }
    }
    public void UpOnBlock()
    {
        charController.enabled = false;
        Vector3 upPos = UpPosTransform.position;
        transform.DOMove(upPos, 1).OnComplete(EnCharC);        
        void EnCharC()
        {
            charController.enabled = true;
        }

    }
}
