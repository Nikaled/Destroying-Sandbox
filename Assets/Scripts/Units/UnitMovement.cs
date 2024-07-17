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
    [HideInInspector] private float PunchInterval = 1;
    private float timeFromPunching;
    public UnitType Type;
    private bool BattleUnit;
    private bool IsUpstairNow;
    private bool EnemyNear;
    [SerializeField] LayerMask OnPunchedRayCollider;
    [SerializeField] Material HittedMatPrefab;
    [SerializeField] MeshRenderer[] UnitMesh;
    [SerializeField] BoxCollider Vision;


    Material HittedMatInstance;
    List<Material> HittedMatInstanceList;
    Sequence WalkSequence;
    [Header("OnlyCreeper")]
    public DestroySystem destroySystem;
    public CreeperNPC creeperNPC;




    private Vector3 GizPos;
    private Vector3 GizDir;
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
        if (Type != UnitType.Animal) { BattleUnit = true; }
    }
    public void ResetTimeToFoundEnemy() // by Vision Collider
    {
        EnemyFoundTime = Time.time;
        RotateToEnemy();
    }
    public void OnUnitDie()
    {
        for (int i = 0; i < HittedMatInstanceList.Count; i++)
        {
            ChangeAlphaOnMaterial(HittedMatInstanceList[i], 0);
        }
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
        SoundManager.instance.PlayUnitedHittedSound();
        if (HittedMatInstance == null)
        {
            HittedMatInstanceList = new();
            for (int i = 0; i < UnitMesh.Length; i++)
            {
                HittedMatInstance = Instantiate(HittedMatPrefab);
                UnitMesh[i].materials = new Material[2] { UnitMesh[i].materials[0], HittedMatInstance };
                HittedMatInstanceList.Add(UnitMesh[i].materials[1]);
            }
        }
        for (int i = 0; i < HittedMatInstanceList.Count; i++)
        {
            ChangeAlphaOnMaterial(HittedMatInstanceList[i], 1);
        }
        AwaitForGroundAfterPunch = true;
        charController.enabled = false;

        Vector3 PunchDirection = (transform.position - Puncher.transform.position).normalized * 3 + Vector3.up * 0.9f;
        Vector3 EndPosition = transform.position + PunchDirection;
        CalculateEndPoint();
        transform.DOMove(EndPosition, 0.3f).OnComplete(EnCharC);
        void EnCharC()
        {

            charController.enabled = true;
            AwaitForGroundAfterPunch = false;
            for (int i = 0; i < HittedMatInstanceList.Count; i++)
            {
                ChangeAlphaOnMaterial(HittedMatInstanceList[i], 0);
            }
        }
        void CalculateEndPoint()
        {
            //Ray ray = new Ray(gameObject.transform.position, transform.position + PunchDirection);
            Ray ray = new Ray(transform.position, transform.position + PunchDirection);
            float Distance = Vector3.Distance(transform.position, PunchDirection);
            if (Physics.Raycast(ray, out RaycastHit HittedBlock, Distance, OnPunchedRayCollider))
            {

                Debug.Log("Punch Raycast hitted:" + HittedBlock.collider.name);
                Vector3 GoodPoint = Vector3.Lerp(transform.position, HittedBlock.point, 0.8f);
                EndPosition = GoodPoint;
                GizPos = transform.position;
                GizDir = EndPosition;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(GizPos, GizDir);
    }
    private void ChangeAlphaOnMaterial(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        //mat.SetColor("_Color", newColor);
        mat.color = newColor;
        Debug.Log("Alpha changed to " + alphaVal);

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

        if (BattleUnit)
        {
            Vision.enabled = true;
        }
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
        if (charController.enabled == true)
        {
            charController.Move((CurrentVectorMove - Vector3.up * 10) * Time.deltaTime * Speed);
        }
        if (BattleUnit)
        {
            if (Time.time - EnemyFoundTime > TimeToKillEnemy)
            {
                EnemyFoundTime = Time.time;
                CurrentEnemy = null;
            }
            if (CurrentEnemy != null)
            {

                if (EnemyNear == false)
                {
                    RotateToEnemy();
                }
                if (Vector3.Distance(gameObject.transform.position, CurrentEnemy.transform.position) < 2.65f)
                {
                    if (Vector3.Distance(gameObject.transform.position, CurrentEnemy.transform.position) < 0.5f)
                    { EnemyNear = true; }
                    else
                    {
                        EnemyNear = false;
                    }
                    var EnemyHP = CurrentEnemy.GetComponent<UnitHpSystem>();
                    if (EnemyHP != null)
                    {

                        if (Time.time - timeFromPunching > PunchInterval)
                        {
                            timeFromPunching = Time.time;
                            Debug.Log("timeFromPunching:" + timeFromPunching);
                            if (Type != UnitType.Creeper)
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
                else
                {
                    EnemyNear = false;
                }
            }
        }
    }
    public void CreeperExplosion()
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
        if(CurrentEnemy != null)
        {
            return;
        }
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
    }
    public void UpOnBlock()
    {
        if (IsUpstairNow)
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
