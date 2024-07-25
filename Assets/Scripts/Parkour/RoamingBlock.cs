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
    public bool InvertMoveCycle;
    public float Speed = 1;
    private void Start()
    {
       
    }

    private void ActivateObject()
    {
        if (Speed == 0)
        {
            Speed = 1;
        }
        TimeToGoOneSide /= Speed;
        BasePosition = transform.position;
        RightPosition = transform.position + SideToRoam * MovesFromMiddlePosition;
        LeftPosition = transform.position - SideToRoam * MovesFromMiddlePosition;
        if (!InvertMoveCycle)
        {
            SwitchVector(GoRight);
        }
        else
        {
            SwitchVector(!GoRight);
        }
    }
    public void Freeze(bool Is)
    {
        rb.velocity = Vector3.zero;
        if (RoamSeq != null)
        {
            DOTween.Kill(RoamSeq);
        }
        DOTween.Kill(rb);

        if (Is)
        {
            transform.position = BasePosition;
            rb.DOMove(BasePosition, 0);
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
        //RoamSeq.Append();
        rb.DOMove(CurrentDestination, TimeToGoOneSide).OnComplete( ()=>SwitchVector(!GoRight));
    }
    private void Update()
    {
        if (IsFrozen)
        {
            //if (RoamSeq != null)
            //{
            //    DOTween.Kill(RoamSeq);
            //}
            //    DOTween.Kill(gameObject.transform);
            //transform.DOMove(BasePosition, 0);
            return;
        }
        //StartMove();
        //if (Vector3.Distance(transform.position, CurrentDestination) < 1)
        //{
        //    SwitchVector(!GoRight);
        //}
    }

    public void SetData(float Speed, bool InvertMoveCycle = false)
    {
        this.Speed = Speed;
        this.InvertMoveCycle = InvertMoveCycle;
        ActivateObject();
    }

    public SaveParkourBlockData GetData()
    {
        SaveParkourBlockData newData = new SaveParkourBlockData();
        newData.Speed = Speed;
        Debug.Log("Speed is" + newData.Speed);
        newData.InvertMoveCycle = InvertMoveCycle;
        return newData;
    }
}
