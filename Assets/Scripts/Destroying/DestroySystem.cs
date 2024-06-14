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
        if(IsUnit == false)
        {
        BlocksAnimation();
        }
        else
        {

        }
        ObjectDies();
    }
    public void FireObject()
    {
        if (IsFireable == false && IsUnit == false)
        {
            return;
        }
    }
    private void UnitDieAnimation()
    {
        GameObject GhostUnit = Instantiate(MeshObject, MeshObject.transform.position, MeshObject.transform.rotation);
        GhostUnit.GetComponent<BoxCollider>().enabled = false;
        GhostUnit.GetComponent<Rigidbody>().useGravity = false;
        GhostUnit.GetComponent<Rigidbody>().isKinematic = true;
        for (int i = 0; i < GhostUnit.GetComponent<MeshRenderer>().materials.Length; i++)
        {
            GhostUnit.GetComponent<MeshRenderer>().materials[i] = UnitDieMaterial;
        }
    }
    public void ExplosionForce(Vector3 force)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
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
            smallBlock.GetComponent<Rigidbody>().isKinematic = false;
            smallBlock.GetComponent<Rigidbody>().useGravity = true;
            smallBlock.layer = 2;
            Vector3 PosDifference = (transform.position - ProjectilePosition).normalized;
            smallBlock.GetComponent<Rigidbody>().AddForce((PosDifference + Vector3.down) * 5);
        }
        GameObject objToSpawn = new GameObject("Cool GameObject made from Code");
        BlockDestroyingAnimation smallBlockParent = objToSpawn.AddComponent<BlockDestroyingAnimation>();

        smallBlockParent.smallBlocks = smallBlocks;
        smallBlockParent.BlockDisappearing();
    }
    private void ObjectDies()
    {
        OnDied?.Invoke();
        if (MeshObject != null)
        {
            DestroyObjectAnimation(MeshObject);
            Destroy(RootObject);
        }
        else
        {
            Debug.Log("Не назначен родитель удаления");
            MeshObject = RootObject;
            DestroyObjectAnimation(RootObject);
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
