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
    [HideInInspector] public bool DestroyPhaseStarted;
    [SerializeField] bool IsZombie;
    [SerializeField] Animator ZombieAnimator;
    [HideInInspector] public GameObject CurrentEnemy;
    private float EnemyFoundTime;
    private float TimeToKillEnemy = 6;
    [HideInInspector] public bool AwaitForGroundAfterPunch;
    [HideInInspector]  public float PunchInterval =0;
    private float timeFromPunching;
    public UnitType Type;
    private bool BattleUnit;
    private bool IsUpstairNow;
    [SerializeField] LayerMask OnPunchedRayCollider;
    [Header("OnlyCreeper")]
    public DestroySystem destroySystem;
    public CreeperNPC creeperNPC;
    Sequence WalkSequence;
    public enum UnitType
    {
        Zombie,
        Spider,
        Creeper,
        Animal
    }
    private void Start()
    {
        CycleManager.instance.DestroyingPhaseStarted += OnActivatedDestroyingPhase;
        SetBattleUnitBool();
    }
    private void SetBattleUnitBool()
    {
        if(Type != UnitType.Animal) { BattleUnit = true; }
    }
    public void ResetTimeToFoundEnemy   () // by Vision Collider
    {
        EnemyFoundTime = Time.time;
        RotateToEnemy();
    }
    public void GetPunch(GameObject Puncher)
    {
        if (BattleUnit)
        {
            CurrentEnemy = Puncher;
            ResetTimeToFoundEnemy();
        } 
        if (AwaitForGroundAfterPunch == true)
        {
            return;
        }
        AwaitForGroundAfterPunch = true;
        charController.enabled = false;
        Vector3 PunchDirection = (gameObject.transform.position - Puncher.transform.position).normalized*3 + Vector3.up * 0.9f;
        Vector3 EndPosition = transform.position + PunchDirection;
        CalculateEndPoint();
        transform.DOMove(EndPosition, 0.3f).OnComplete(EnCharC);
        void EnCharC()
        {

            charController.enabled = true;
            AwaitForGroundAfterPunch = false;
        }
        void CalculateEndPoint()
        {
            Ray ray = new Ray(gameObject.transform.position, transform.position + PunchDirection);
            float Distance = Vector3.Distance(transform.position, PunchDirection);
            if (Physics.Raycast(ray, out RaycastHit HittedBlock, Distance, OnPunchedRayCollider))
            {
                
                Debug.Log("Punch Raycast hitted:" + HittedBlock.collider.name);
                EndPosition = HittedBlock.point;
            }
        }
    }
    private void OnActivatedDestroyingPhase()
    {
        for (int i = 0; i < BlockCollidersParentAndSides.Length; i++)
        {
            if (BlockCollidersParentAndSides[i] != null)
                BlockCollidersParentAndSides[i].enabled = false;
        }
        ChildrenStartLocalPosition = ChildrenUnit.transform.localPosition;
         WalkSequence = DOTween.Sequence();
        if (IsZombie == false)
        {
            WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenUnit.transform.localPosition + new Vector3(0, 1, 0), 0.15f)).SetEase(Ease.InExpo);
            WalkSequence.Append(ChildrenUnit.transform.DOLocalMove(ChildrenStartLocalPosition, 1f).SetEase(Ease.InOutExpo));
            WalkSequence.SetLoops(10000);
        }
        else
        {
            ZombieAnimator.SetBool("Walking", true);
        }
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
        if (DestroyPhaseStarted == false)
        {
            return;
        }
        CurrentVectorMove = (MoveToObj.position - transform.position).normalized;
        Debug.Log("CurrentVectorMove:" + CurrentVectorMove);
        if (charController.enabled == true)
        {
            charController.Move((CurrentVectorMove - Vector3.up * 10) * Time.deltaTime * Speed);
        }
        if (BattleUnit)
        {
            if(Time.time - EnemyFoundTime > TimeToKillEnemy)
            {
                EnemyFoundTime = Time.time;
                CurrentEnemy = null;
            }
            if (CurrentEnemy != null)
            {
                RotateToEnemy();
                if (Vector3.Distance(gameObject.transform.position, CurrentEnemy.transform.position) < 2.65f)
                {
                    var EnemyHP = CurrentEnemy.GetComponent<UnitHpSystem>();
                    if (EnemyHP != null)
                    {

                        if (Time.time - timeFromPunching > PunchInterval)
                        {
                            //timeFromPunching = Time.time;
                            if(Type != UnitType.Creeper)
                            {
                                bool IsEnemyPunched = EnemyHP.TryGetPunch(gameObject);
                                if (IsEnemyPunched)
                                {
                                    ResetTimeToFoundEnemy();
                                }
                            }
                            else
                            {
                                CreeperExplosion();
                            }
                        }
                    }
                }
            }
        }    
    }
    private void CreeperExplosion()
    {
        WalkSequence.Kill();
        charController.enabled = false;
        DestroyPhaseStarted = false;
        creeperNPC.OnCreeperEndsExplosion += OnDieCreeper;
        creeperNPC.StartExplosion();
        void OnDieCreeper()
        {
            destroySystem.DamageTaked(Vector3.zero);
        }
    }
    private IEnumerator RandomRotatingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (charController.enabled == true && CurrentEnemy == null)
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
    private void RotateToEnemy()
    {
        Vector3 targetPostition = new Vector3(CurrentEnemy.transform.position.x, this.transform.position.y, CurrentEnemy.transform.position.z);
        transform.LookAt(targetPostition);
        Debug.Log("Rotate to Enemy");
    }
    public void UpOnBlock()
    {
        if(IsUpstairNow)
        {
            return;
        }
        IsUpstairNow = true;
        charController.enabled = false;
        Vector3 upPos = UpPosTransform.position;
        transform.DOMove(upPos, 1).OnComplete(EnCharC);
        void EnCharC()
        {
            charController.enabled = true;
            IsUpstairNow = false;
        }

    }
}
