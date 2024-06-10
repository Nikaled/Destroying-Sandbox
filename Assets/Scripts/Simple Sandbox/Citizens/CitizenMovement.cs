using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CitizenMovement : MonoBehaviour
{
    private bool isMoving;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform CurrentDestination;
    private Vector3? CurrentDest;
    private NavMeshChecker checker;
    private bool IsDying;
    [SerializeField] HpSystem hpSystem;
    public float DieAnimationTime = 2f;
    [SerializeField] GameObject ChildrenUnit;
    [SerializeField] Vector3 ChildrenStartLocalPosition;
    Sequence WalkSequence;
   public  Ease WalkTweenTypeUp;
   public  Ease WalkTweenTypeDown;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            agent.enabled = false;
            agent.enabled = true;
        }
    }
    public void TryJump()
    {
        gameObject.transform.position += new Vector3(0, 100, 0);
    }
    private IEnumerator UpdateDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            FindNewDestination();
        }
    }
    private void Start()
    {
        StartCoroutine(UpdateDestination());
        ChildrenStartLocalPosition = ChildrenUnit.transform.localPosition;
        Sequence WalkSequence = DOTween.Sequence();
        WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenUnit.transform.localPosition + new Vector3(0, 1, 0), 0.15f)).SetEase(WalkTweenTypeUp);
        WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenStartLocalPosition, 1f).SetEase(WalkTweenTypeDown));
        WalkSequence.SetLoops(1000);
        checker = Instantiate(CitizenNavMeshManager.instance.Checker, gameObject.transform.position, Quaternion.identity);
        checker.citizen = this;
        checker.GetComponent<SphereCollider>().enabled = true;
        FindNewDestination();
        //hpSystem.OnDied += CitizenDie;
    }
    public void MoveToPosition(Vector3 DestinationPosition)
    {

        agent.SetDestination(DestinationPosition);
        //animator.SetBool("IsWalk", true);
        //animator.SetFloat("Speed_f", 1);
    }
    private void StopMoving()
    {
        //animator.SetBool("IsWalk", false);
        //animator.SetFloat("Speed_f", 0);
    }
    private void CitizenDie()
    {
        if(gameObject.GetComponent<CapsuleCollider>() != null)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        agent.speed = 0;
        //animator.SetBool("IsWalk", false);
        //animator.SetTrigger("Die");
        IsDying = true;
        agent.SetDestination(transform.position);
        StartCoroutine(WaitForDyingAnimation());
    }
    private IEnumerator WaitForDyingAnimation()
    {
        hpSystem.enabled = false;
        yield return new WaitForSeconds(DieAnimationTime);
        Destroy(hpSystem.RootObject);
    }
    public void FindNewDestination()
    {
        CitizenNavMeshManager.instance.MoveCheckerToNewPoint(checker.gameObject, transform);
    }
    void Update()
    {
        if(IsDying == false)
        {
            MoveToPosition(checker.transform.position);
            if (Vector3.Distance(gameObject.transform.position, checker.transform.position) < 1)
            {
                StopMoving();
                FindNewDestination();
            }
        }

    }
}
