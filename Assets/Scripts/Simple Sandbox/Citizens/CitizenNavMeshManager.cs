using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using System.Runtime.CompilerServices;

public class CitizenNavMeshManager : MonoBehaviour
{
    public static CitizenNavMeshManager instance;
    [SerializeField] public Collider teleportArea;
    [SerializeField] public NavMeshChecker Checker;
    [SerializeField] private NavMeshSurface navMeshSurface;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuildNavMesh();
        }
    }
    public void BuildNavMesh()
    {
        buildMesh();
        async void buildMesh()
        {
        navMeshSurface.BuildNavMesh();
        }
    }
    public Vector3? MoveCheckerToNewPoint(GameObject sphere, Transform UnitTransform)
    {
       int RPath =  Random.Range(0, 2);
        if(RPath == 0)
        {
            sphere.SetActive(false);
            int RandomX = Random.Range(-1, 1);
            int RandomZ = Random.Range(-1, 1);
            int RandomY = Random.Range(-1, 1);
            sphere.transform.position = UnitTransform.position + new Vector3(RandomX, RandomY, RandomZ);
            //Debug.Log("sphere.transform.position"+sphere.transform.position);
            sphere.SetActive(true);
            return sphere.transform.position;
        }
        else
        {
            sphere.SetActive(false);
            sphere.transform.position = RandomPointInBounds(teleportArea.bounds);
            //Debug.Log("sphere.transform.position"+sphere.transform.position);
            sphere.SetActive(true);
            return sphere.transform.position;
        }
      
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x*0.9f, bounds.max.x * 0.9f),
            UnityEngine.Random.Range(bounds.max.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z * 0.9f, bounds.max.z * 0.9f)
        );
    }
}
