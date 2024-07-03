using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Creeper : MonoBehaviour
{
    [SerializeField] DestroyEffect ExplosionAnimation;
    [SerializeField] GameObject ParentObject;
    [SerializeField] DamageArea DestroyArea;
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion = 0.6f;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    public static Creeper instance;
    public bool IsExploding;
    private IEnumerator ExplosionCor;
    private Vector3 DefaultScale;
    [SerializeField] Material CreeperWhiteMaterial;
    [SerializeField] GameObject creeperModel;
    protected virtual void Awake()
    {
        instance = this;
        DefaultScale = transform.localScale;
    }
    protected virtual void OnEnable()
    {
        ChangeAlphaOnMaterial(CreeperWhiteMaterial, 0);
        transform.localScale = DefaultScale;
        DestroyArea.GetComponent<SphereCollider>().enabled = false;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = false;
        creeperModel.SetActive(true);
        IsExploding = false;
    }
    public void DisableCollidersOnSwitch()
    {
        DestroyArea.GetComponent<SphereCollider>().enabled = false;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = false;
        Player.instance.examplePlayer.MyLockOnShoot = false;
        if (ExplosionCor != null)
        {
            StopCoroutine(ExplosionCor);
        }
    }
    public void StartExplosion()
    {
        if (IsExploding == false)
        {
            IsExploding = true;
            Player.instance.examplePlayer.MyLockOnShoot = true;
            ExplosionCor = Explosion();
            StartCoroutine(ExplosionCor);
        }
    }
    protected virtual void ExplosionCreeperAnimation()
    {
        transform.DOScale(transform.localScale * 1.3f, DelayBeforeExplosion);
        StartCoroutine(TurnWhiteCor());

        IEnumerator TurnWhiteCor()
        {
            float CreepersAlpha = 0;
            float step = 0.05f;
            while(CreepersAlpha < 0.8f)
            {
                yield return new WaitForSeconds(0.1f);
                CreepersAlpha += step;
                step += 0.01f;
                Debug.Log("CreepersAlpha:" + CreepersAlpha);
                ChangeAlphaOnMaterial(CreeperWhiteMaterial, CreepersAlpha);
            }     
        }
    }
    protected virtual void ChangeAlphaOnMaterial(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);

    }
    protected virtual IEnumerator Explosion()
    {
        targetsInExplosion = new();
        DestroyArea.GetComponent<SphereCollider>().enabled = true;
        ExplosionCreeperAnimation();
        yield return new WaitForSeconds(DelayBeforeExplosion);
        //Source.PlayExplosionSound();
        DestroyEffect explosionAnim = Instantiate(ExplosionAnimation, ExplosionAnimation.transform.position, ExplosionAnimation.transform.rotation);
        explosionAnim.enabled = true;
        explosionAnim.ShowEffectAndDestroyAfterDelay();

        explosionForceChecker.GetComponent<SphereCollider>().enabled = true;
        targetsInExplosion = DestroyArea.targetsInExplosion;
        for (int i = 0; i < targetsInExplosion.Count; i++)
        {
            if (targetsInExplosion[i] != null)
            {
                targetsInExplosion[i].TakeDamage(targetsInExplosion[i].transform.position + new Vector3(0, 2, 0));
            }
        }
        DestroyArea.GetComponent<SphereCollider>().enabled = false;
        explosionForceChecker.GetComponent<SphereCollider>().enabled = false;
        creeperModel.SetActive(false);
        yield return new WaitForSeconds(1f);
        IsExploding = false;
        CreeperManager.OnCreeperExploded();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        var DestrCol = other.GetComponent<DestroyCollision>();
        if (DestrCol != null)
        {
            DestrCol.TakeDamage(transform.position);
        }
    }
}
