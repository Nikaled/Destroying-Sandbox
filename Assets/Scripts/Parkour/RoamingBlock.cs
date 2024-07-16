using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RoamingBlock : MonoBehaviour, IMoveableParkour
{
    [SerializeField] Rigidbody rb;
    public float TimeToGoOneSide;
    public int MovesFromMiddlePosition;
    bool GoRight;
    public Vector3 SideToRoam;
    Vector3 RightPosition;
    Vector3 LeftPosition;
    Vector3 CurrentDestination;
    Sequence RoamSeq;
    Vector3 BasePosition;
    public bool IsFrozen { get; set; }
    private void Start()
    {
        BasePosition = transform.position;
        RightPosition = transform.position + SideToRoam*MovesFromMiddlePosition;
        LeftPosition = transform.position - SideToRoam * MovesFromMiddlePosition;
        SwitchVector(GoRight);
    }
    public void Freeze(bool Is)
    {
        rb.velocity = Vector3.zero;
        if (RoamSeq != null)
        {
            DOTween.Kill(RoamSeq);
        }
        if (Is)
        {
            transform.position = BasePosition;
            transform.DOMove(BasePosition, 0);
        }
        else
        {
            RightPosition = transform.position + SideToRoam * MovesFromMiddlePosition;
            LeftPosition = transform.position - SideToRoam * MovesFromMiddlePosition;
            BasePosition = transform.position;
            StartMove();
        }
    }
    private void SwitchVector(bool IsRight)
    {
        rb.velocity = Vector3.zero;
        GoRight = IsRight;
        if (GoRight)
        {
            CurrentDestination = RightPosition;
        }
        else
        {

            CurrentDestination = LeftPosition;
        }
        StartMove();
    }
    private void StartMove()
    {
        RoamSeq = DOTween.Sequence();
        RoamSeq.Append(transform.DOMove(CurrentDestination, TimeToGoOneSide));
    }
    private void Update()
    {
        if (IsFrozen)
        {
            if (RoamSeq != null)
            {
                DOTween.Kill(RoamSeq);
            }
            //transform.DOMove(BasePosition, 0);
            return;
        }
        StartMove();
        if (Vector3.Distance(transform.position, CurrentDestination) < 1)
        {
            SwitchVector(!GoRight);
        }
    }
}
