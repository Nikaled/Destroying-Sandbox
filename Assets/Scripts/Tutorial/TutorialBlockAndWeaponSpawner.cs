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
    private bool PlayerSee;

    void OnBecameVisible()
    {
        PlayerSee = true;
    }
    private void OnBecameInvisible()
    {
        PlayerSee = false;
    }

    void Start()
    {
        StartCoroutine(InfinityLightDestroysBlock());
        StartCoroutine(InfinityPistolDestroysBlock());
    }
    private IEnumerator InfinityLightDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                Instantiate(TutorialBlockPrefab, TutorialBlockSpawnTransform.position, Quaternion.identity);
                Instantiate(TutorialBlockPrefab, TutorialBlockSpawnTransform.position+new Vector3(0,0,2), Quaternion.identity);
                Instantiate(TutorialBlockPrefab, TutorialBlockSpawnTransform.position+new Vector3(2,0,2), Quaternion.identity);
                Instantiate(TutorialBlockPrefab, TutorialBlockSpawnTransform.position+new Vector3(2,0,0), Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
                var lightning = Instantiate(LightningPrefab, TutorialBlockSpawnTransform.position, Quaternion.identity);
                lightning.Fire();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
       
    }
    private IEnumerator InfinityPistolDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                Instantiate(TutorialBlockPrefab, PistolTutorialBlockSpawnTransform.position, Quaternion.identity);
                yield return new WaitForSeconds(1.5f);
                Instantiate(Bullet, ProjectileSpawnTransform.position, ProjectileSpawnTransform.rotation);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }

    }
}
