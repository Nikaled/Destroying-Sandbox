using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TutorialBlockAndWeaponSpawner : MonoBehaviour
{
    [SerializeField] GameObject TutorialBlockPrefab;
    [SerializeField] GameObject TutorialHousePrefab;
    [SerializeField] Transform[] TutorialSpawnTransforms;
    [SerializeField] Transform TutorialCreeperSpawnTransform;
    [SerializeField] Transform PistolTutorialBlockSpawnTransform;
    [SerializeField] Transform ProjectileSpawnTransform;
    [SerializeField] Lightning LightningPrefab;
    [SerializeField] Meteor MeteorPrefab;
    [SerializeField] ShootingProjectile Bullet;
    [SerializeField] UnitMovement creeper;
    [SerializeField] GameObject pistolModel;
    private bool PlayerSee;
    private GameObject[] currentHouses;
    private int currentWeaponIndex;
    void OnBecameVisible()
    {
        PlayerSee = true;
    }
    private void OnBecameInvisible()
    {
        PlayerSee = false;
    }

    [ContextMenu("SpawnHouses")]
    private void DevSpawnHouses()
    {
        currentHouses = new GameObject[4];
        for (int i = 0; i < TutorialSpawnTransforms.Length; i++)
        {
            SpawnHouse(0, TutorialSpawnTransforms[i].position);
        }
    }
    void Start()
    {
        currentHouses = new GameObject[4];
        StartCoroutine(InfinityLightDestroysBlock());
        StartCoroutine(InfinityMeteorDestroysBlock());
        StartCoroutine(InfinityCreeperDestroysBlock());
        StartCoroutine(InfinityPistolDestroysBlock());
    }
    private IEnumerator InfinityWeaponCycle()
    {
        while (true)
        {
            if (PlayerSee)
            {
                //if (currentHouses != null)
                //{
                //    Destroy(currentHouses);
                //}
                //currentHouses = Instantiate(TutorialHousePrefab, TutorialSpawnTransforms[0].position, TutorialHousePrefab.transform.rotation);
                //currentHouses.SetActive(true);
                //currentHouses.transform.rotation = TutorialHousePrefab.transform.rotation;

                yield return new WaitForSeconds(1.5f);
                switch (currentWeaponIndex)
                {
                    case 0:
                        FireLigtning();
                        break;
                    case 1:
                        FireMeteor();
                        break;
                    case 2:
                        FireCreeper();
                        break;
                    default:
                        FireLigtning();
                        currentWeaponIndex = 0;
                        break;
                }
                currentWeaponIndex++;

                yield return new WaitForSeconds(6f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
    private void FireCreeper()
    {
        var expCreeper = Instantiate(creeper, TutorialCreeperSpawnTransform.position, Quaternion.identity);
        expCreeper.CreeperExplosion();
    }
    private void FireLigtning()
    {

    }
    private void FireMeteor()
    {
        var meteor = Instantiate(MeteorPrefab, TutorialSpawnTransforms[2].position + new Vector3(20, 50, 20), Quaternion.identity);
        meteor.Fire(TutorialSpawnTransforms[2].position);
    }
    private void SpawnHouse(int index, Vector3 SpawnPosition)
    {

        if (currentHouses[index] != null)
        {
            Destroy(currentHouses[index]);
        }
        currentHouses[index] = Instantiate(TutorialHousePrefab, SpawnPosition, TutorialHousePrefab.transform.rotation);
        currentHouses[index].SetActive(true);
        currentHouses[index].transform.rotation = TutorialHousePrefab.transform.rotation;
    }
    private IEnumerator InfinityLightDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                SpawnHouse(0, TutorialSpawnTransforms[0].position);
                yield return new WaitForSeconds(1.5f);
                var lightning = Instantiate(LightningPrefab, TutorialSpawnTransforms[0].position, Quaternion.identity);
                lightning.Fire();
                yield return new WaitForSeconds(4f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
    private IEnumerator InfinityMeteorDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                SpawnHouse(1, TutorialSpawnTransforms[1].position);

                yield return new WaitForSeconds(1.5f);

                var meteor = Instantiate(MeteorPrefab, TutorialSpawnTransforms[1].position + new Vector3(20, 50, 20), Quaternion.identity);
                meteor.Fire(TutorialSpawnTransforms[1].position);
                yield return new WaitForSeconds(6f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
    private IEnumerator InfinityPistolDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                SpawnHouse(3, TutorialSpawnTransforms[3].position);
                yield return new WaitForSeconds(1.5f);
                Vector3[] PositionsOnShoot = new Vector3[] { new Vector3(0, -30, 0), new Vector3(0, 0, 0), new Vector3(0, 30, 0) };
                for (int i = 0; i < 3; i++)
                {
                    pistolModel.transform.DORotate(PositionsOnShoot[i], 0.2f);
                    yield return new WaitForSeconds(0.3f);
                    Instantiate(Bullet, ProjectileSpawnTransform.position, ProjectileSpawnTransform.rotation);
                    yield return new WaitForSeconds(0.03f);
                    Instantiate(Bullet, ProjectileSpawnTransform.position, ProjectileSpawnTransform.rotation);
                }
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
    private IEnumerator InfinityCreeperDestroysBlock()
    {
        while (true)
        {
            if (PlayerSee)
            {
                SpawnHouse(2, TutorialSpawnTransforms[2].position);

                yield return new WaitForSeconds(1.5f);
                var expCreeper = Instantiate(creeper, TutorialCreeperSpawnTransform.position, Quaternion.identity);
                expCreeper.transform.DOMove(TutorialSpawnTransforms[2].position+ new Vector3(0, 0.3f, 0), 2f);
                yield return new WaitForSeconds(2f);
                expCreeper.CreeperExplosion();
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }
}
