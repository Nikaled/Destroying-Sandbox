using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DestroySystem : MonoBehaviour
{
    [SerializeField] public GameObject RootObject;
    [SerializeField] GameObject MeshObject;
    [SerializeField] GameObject DestroyAnimation;
    [SerializeField] AudioExplosion AudioExplosionPrefab;
    [SerializeField] Material UnitDieMaterial;
    [SerializeField] GameObject FireAnimation;
    private readonly string AnalyticsDestroyObject = "ObjectDestroyed";
    public bool IsFireable;
    public bool IsUnit;
    public event Action OnDied;
    public bool ObjectIsDestroying;
    private Vector3 ProjectilePosition;

    private void Awake()
    {

    }
    public void DamageTaked(Vector3 position)
    {
        ProjectilePosition = position;
        Debug.Log("Damage Taked");
        if (ObjectIsDestroying)
        {
            return;
        }
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
        if (IsFireable == false && IsUnit == false)
        {
            if(Player.instance.CurrentWeapon == Player.WeaponType.FlameThrower)
            {
            NotFlamingUI.instance.ShowWarning();
            }
            return;
        }
        if (ObjectIsDestroying)
        {
            return;
        }
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
        if (IsUnit || CycleManager.instance.currentPhase != CycleManager.Phase.Destroying)
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
        GameObject[] smallBlocks = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            float RandomSpawnModifier = UnityEngine.Random.Range(-1.3f, 1.3f);
            Vector3 RandomPosition = new Vector3(MeshObject.transform.position.x + RandomSpawnModifier, MeshObject.transform.position.y + RandomSpawnModifier, MeshObject.transform.position.z + RandomSpawnModifier);
            GameObject smallBlock = Instantiate(MeshObject, RandomPosition, Quaternion.identity);
            smallBlocks[i] = smallBlock;
            smallBlock.transform.parent = null;
            smallBlock.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0);
            smallBlock.GetComponent<DestroyCollision>().enabled = false;
            var rb = smallBlock.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            smallBlock.layer = 12;
            Vector3 PosDifference = (transform.position - ProjectilePosition).normalized;
            smallBlock.GetComponent<Rigidbody>().AddForce((PosDifference + Vector3.down) * 5);
        }
        GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        BlockDestroyingAnimation smallBlockParent = objToSpawn.AddComponent<BlockDestroyingAnimation>();

        smallBlockParent.smallBlocks = smallBlocks;
        smallBlockParent.BlockDisappearing();
    }
    private void AddRigidbodyWhenForcing()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.angularDrag = 0.3f;
        rb.drag = 0.3f;
    }
    private void ObjectDies()
    {
        OnDied?.Invoke();
        Geekplay.Instance.PlayerData.Coins += 1;
        Geekplay.Instance.Save();
        DestroyCounter.instance.ObjectDestroyed();
        if (MeshObject != null)
        {
            Destroy(RootObject);
        }
        else
        {
            Debug.Log("Не назначен родитель удаления");
            MeshObject = RootObject;
            if (RootObject != null)
                Destroy(RootObject);
        }

        //Geekplay.Instance.PlayerData.DestroyCount++;
        //Geekplay.Instance.Leaderboard("Destroy", Geekplay.Instance.PlayerData.DestroyCount);

        //Analytics.instance.SendEvent(AnalyticsDestroyObject);
        Geekplay.Instance.Save();
    }
    private void DestroyObjectAnimation(GameObject rootObject)
    {

        //AudioExplosion audioExplosion = Instantiate(AudioExplosionPrefab, transform.position, Quaternion.identity);
        //audioExplosion.PlayExplosionSound();
        //var fx = Instantiate(DestroyAnimation, transform.position, Quaternion.identity);
        ////fx.transform.parent = null;
        ////fx.transform.DOScale(DestroyAnimation.transform.localScale * SumOfSides, 0);
    }
}
