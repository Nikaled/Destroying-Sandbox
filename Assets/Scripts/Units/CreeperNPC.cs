using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CreeperNPC : MonoBehaviour
{
    [SerializeField] DestroyEffect ExplosionAnimation;
    [SerializeField] DamageArea DestroyArea;
    [SerializeField] MeshRenderer CreeperMesh; 
    List<DestroyCollision> targetsInExplosion = new();
    [SerializeField] float DelayBeforeExplosion = 0.6f;
    [SerializeField] float ExplosionScale = 3;
    [SerializeField] AudioExplosion Source;
    [SerializeField] ExplosionForceChecker explosionForceChecker;
    public bool IsExploding;
    private IEnumerator ExplosionCor;
    [SerializeField] Material CreeperWhiteMaterial;
    [SerializeField] GameObject creeperModel;
    public Action OnCreeperEndsExplosion;
    private Material expMatInstance;
    public void StartExplosion()
    {
        if (IsExploding == false)
        {
            IsExploding = true;
            ExplosionCor = Explosion();
            StartCoroutine(ExplosionCor);
        }
    }
    protected virtual void ExplosionCreeperAnimation()
    {
        creeperModel.transform.DOScale(creeperModel.transform.localScale * 1.3f, DelayBeforeExplosion);
      
        Material matInstance = Instantiate(CreeperWhiteMaterial);
        expMatInstance = matInstance;
        CreeperMesh.materials = new Material[2] { CreeperMesh.materials[0], matInstance };
        StartCoroutine(TurnWhiteCor());
        IEnumerator TurnWhiteCor()
        {
            float CreepersAlpha = 0;
            float step = 0.05f;
            while (CreepersAlpha < 0.8f)
            {
                yield return new WaitForSeconds(0.1f);
                CreepersAlpha += step;
                step += 0.01f;
                Debug.Log("CreepersAlpha:" + CreepersAlpha);
                ChangeAlphaOnMaterial(matInstance, CreepersAlpha);
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
        creeperModel.SetActive(false);
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
        Destroy(DestroyArea.gameObject);
        Destroy(explosionForceChecker.gameObject);
        //DestroyArea.GetComponent<SphereCollider>().enabled = false;
        //explosionForceChecker.GetComponent<SphereCollider>().enabled = false;
        ChangeAlphaOnMaterial(expMatInstance, 0);
        //yield return new WaitForSeconds(1f);
        IsExploding = false;
        OnCreeperEndsExplosion?.Invoke();
    }
}
