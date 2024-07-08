using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlockAndWeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject TutorialBlockPrefab;
    [SerializeField] Transform TutorialBlockSpawnTransform;
    [SerializeField] Transform PistolTutorialBlockSpawnTransform;
    [SerializeField] Transform ProjectileSpawnTransform;
    [SerializeField] Lightning LightningPrefab;
    [SerializeField] ShootingProjectile Bullet;

    void Start()
    {
        StartCoroutine(InfinityLightDestroysBlock());
        StartCoroutine(InfinityPistolDestroysBlock());
    }
    private IEnumerator InfinityLightDestroysBlock()
    {
        while (true)
        {
            Instantiate(TutorialBlockPrefab, TutorialBlockSpawnTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            var lightning = Instantiate(LightningPrefab, TutorialBlockSpawnTransform.position, Quaternion.identity);
            lightning.Fire();
            yield return new WaitForSeconds(2f);
        }
       
    }
    private IEnumerator InfinityPistolDestroysBlock()
    {
        while (true)
        {
            Instantiate(TutorialBlockPrefab, PistolTutorialBlockSpawnTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            Instantiate(Bullet, ProjectileSpawnTransform.position, ProjectileSpawnTransform.rotation);
            yield return new WaitForSeconds(2f);
        }

    }
}
