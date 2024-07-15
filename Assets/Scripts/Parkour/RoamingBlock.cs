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
    public bool IsFrozen { get; set; }
    private void Start()
    {
        RightPosition = transform.position + SideToRoam*MovesFromMiddlePosition;
        LeftPosition = transform.position - SideToRoam * MovesFromMiddlePosition;
        SwitchVector(GoRight);
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
    }
    private void Update()
    {
        if (IsFrozen)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.DOMove(CurrentDestination, TimeToGoOneSide);
        rb.MovePosition(CurrentDestination);
        if(Vector3.Distance(transform.position, CurrentDestination) < 2)
        {
            SwitchVector(!GoRight);
        }
    }
}
