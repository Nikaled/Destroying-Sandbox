using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestroySystem : MonoBehaviour
{
    [SerializeField] private BlockDestroyingAnimation parentSUKAAnimation;
    [SerializeField] private Transform parentSUKA;
    [SerializeField] public GameObject RootObject;
    [SerializeField] GameObject MeshObject;
    [SerializeField] GameObject DestroyAnimation;
    [SerializeField] AudioExplosion AudioExplosionPrefab;
    [SerializeField] Material UnitDieMaterial;
    [SerializeField] GameObject FireAnimation;
    private readonly string AnalyticsDestroyObject = "ObjectDestroyed";
    public bool IsFireable;
    public bool IsUnit;
    public bool IsTransparent;
    public event Action OnDied;
    public bool ObjectIsDestroying;
    private Vector3 ProjectilePosition;
    [SerializeField] GameObject[] smallBlocks;
    [SerializeField] Rigidbody[] smallBlocksRb;
    [SerializeField] BoxCollider[] smallBlocksColliders;

    private float AnimationTime = 0.2f;
    private float DelayInCycle = 0.1f;

    private void Start()
    {
        if (smallBlocks != null && IsUnit ==false)
        {
            if(smallBlocks.Length > 0)
            {
                for (int i = 0; i < smallBlocks.Length; i++)
                {
                    smallBlocks[i].GetComponent<MeshRenderer>().material = MeshObject.GetComponent<MeshRenderer>().material;
                }
            }         
        } 
    }
    protected virtual bool CheckPhaseNotDestroying()
    {

        if (CycleManager.instance.currentPhase != CycleManager.Phase.Destroying)
        {
            return true;
        }
        return false;
    }
    public void DamageTaked(Vector3 position)
    {
        if (CheckPhaseNotDestroying() || ObjectIsDestroying || DestroyLimiter.AvailableToDestroyAndAddCount() == false)
        {
            return;
        }
        //ProjectilePosition = position;
        ObjectIsDestroying = true;
        if (IsUnit == false)
        {
            BlocksAnimation();
        }
        else
        {
            UnitDieAnimation();
        }
        ObjectDies();
    }
    public void FireObject()
    {
        if (CheckPhaseNotDestroying())
        {
            return;
        }
        if (IsFireable == false && IsUnit == false)
        {
            if (Player.instance.CurrentWeapon == Player.WeaponType.FlameThrower)
            {
                NotFlamingUI.instance.ShowWarning();
            }
            return;
        }
        if (ObjectIsDestroying)
        {
            return;
        }
        SoundManager.instance.PlayBlockBurningSound();
        ObjectIsDestroying = true;
        var Fire = Instantiate(FireAnimation, transform.position, transform.rotation);
        Fire.transform.parent = MeshObject.transform;

        GameObject ParentPivot = new GameObject();
        ParentPivot.transform.position = transform.position - new Vector3(0, 1f, 0);
        gameObject.transform.parent = ParentPivot.transform;
        float ResizeModifier = 2;
        ParentPivot.transform.DOScale(ParentPivot.transform.localScale / ResizeModifier, 1.7f).OnComplete(OnEndFire);


        void OnEndFire()
        {
            ParentPivot.transform.parent = null;
            Destroy(ParentPivot);
            if (IsUnit)
            {
                Fire.transform.parent = null;
                Destroy(Fire);
                UnitDieAnimation(ResizeModifier);
            }
            ObjectDies();
        }
    }
    private void UnitDieAnimation(float BodyScaleModifier = 1)
    {
        GameObject GhostUnit = Instantiate(MeshObject, MeshObject.transform.position, MeshObject.transform.rotation);
        GhostUnit.transform.parent = MeshObject.transform;
        GhostUnit.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        GhostUnit.transform.localScale *= BodyScaleModifier;
        GhostUnit.transform.parent = null;
        GhostUnit.GetComponent<BoxCollider>().enabled = false;
        GhostUnit.GetComponent<Rigidbody>().useGravity = false;
        GhostUnit.GetComponent<Rigidbody>().isKinematic = true;

        MeshRenderer[] Renderers = GhostUnit.GetComponentsInChildren<MeshRenderer>();
        for (int j = 0; j < Renderers.Length; j++)
        {
            Renderers[j].material = UnitDieMaterial;
            //for (int i = 0; i < Renderers[j].materials.Length; i++)
            //{
            //    Renderers[j].materials[i] = UnitDieMaterial;
            //    Renderers[j].materials[i] = null;
            //}
            //Material[] RedMats = new Material[Renderers[j].materials.Length];
            //for (int i = 0; i < Renderers[j].materials.Length; i++)
            //{
            //    RedMats[i] = UnitDieMaterial;
            //}
        }

        var DieScirpt = GhostUnit.AddComponent<UnitDieAnimation>();
        DieScirpt.DieAnimation();
    }
    public void ExplosionForce(Vector3 force)
    {
        if (IsUnit || CheckPhaseNotDestroying())
        {
            return;
        }
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            AddRigidbodyWhenForcing();
            rb = gameObject.GetComponent<Rigidbody>();
        }
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(force);
    }
    private void BlocksAnimation()
    {
        parentSUKA.parent = null;
        SoundManager.instance.PlayBlockCrushedSound();
        for (int i = 0; i < smallBlocks.Length; i++)
        {
            if (IsTransparent)
            {
                smallBlocks[i].SetActive(true);
            }
            smallBlocksColliders[i].enabled = true;
            //smallBlocks[i].SetActive(true);
            float RandomSpawnModifier = UnityEngine.Random.Range(-1.3f, 1.3f);
            Vector3 RandomPosition = new Vector3(MeshObject.transform.position.x + RandomSpawnModifier, MeshObject.transform.position.y + RandomSpawnModifier, MeshObject.transform.position.z + RandomSpawnModifier);
            //smallBlocks[i].transform.parent = null;
            smallBlocks[i].transform.position = RandomPosition;
            float RandomModifierX = UnityEngine.Random.Range(-1, 1);
            float RandomModifierY = UnityEngine.Random.Range(-1, 1);
            float RandomModifierZ = UnityEngine.Random.Range(-1, 1);
            float RandomModifierPower = UnityEngine.Random.Range(0, 4);
            Vector3 RandomVector = new Vector3(RandomModifierX, RandomModifierY, RandomModifierZ) * RandomModifierPower;
            smallBlocksRb[i].isKinematic = false;
            smallBlocksRb[i].AddForce((RandomVector + Vector3.up * RandomModifierPower) * 100);
            smallBlocks[i].transform.DOScale(0, AnimationTime).SetDelay(1f).SetEase(Ease.InCirc);
            // smallBlocks[i].GetComponent<Rigidbody>().AddForce((RandomVector + Vector3.up * RandomModifierPower) * 100 * 100);
        }
        GameObject objToSpawn = new GameObject("BlockFragmentDestroyer");
        //BlockDestroyingAnimation smallBlockParent = objToSpawn.AddComponent<BlockDestroyingAnimation>();

       // parentSUKAAnimation.smallBlocks = smallBlocks;
       // parentSUKAAnimation.BlockDisappearing();
        Destroy(parentSUKA.gameObject, 2.5f);
    }
    private void AddRigidbodyWhenForcing()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.angularDrag = 0.3f;
        rb.drag = 0.3f;
    }
    protected virtual void PlusCountOnObjectDestroyed()
    {
        Geekplay.Instance.PlayerData.Coins += 1;
        Geekplay.Instance.PlayerData.DestroyCount += 1;
        DestroyCounter.instance.ObjectDestroyed();
        //Geekplay.Instance.Save();
        //Geekplay.Instance.Leaderboard("Destroy", Geekplay.Instance.PlayerData.DestroyCount);
    }
    private void ObjectDies()
    {
        OnDied?.Invoke();
        PlusCountOnObjectDestroyed();
        if (MeshObject != null)
        {
            Destroy(RootObject);
        }
        else
        {
            MeshObject = RootObject;
            if (RootObject != null)
                Destroy(RootObject);
        }
    }
}
