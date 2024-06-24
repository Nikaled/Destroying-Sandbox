using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]

public class BuildCellGenerator : MonoBehaviour
{
    public int Lenght;
    public int Widht;
    public float Space;
    [SerializeField] GameObject BuildCell;
    GameObject[,] Cells;
    [SerializeField] GameObject GridParent;
    private float boundsSizeLenght;
    void Start()
    {
        Cells = new GameObject[Lenght, Widht];
        boundsSizeLenght = 2;
        GenerateStartCell();

    }
    [ContextMenu("Create Floor")]
    private void GenerateStartCell()
    {
        GameObject NewGridParent = Instantiate(GridParent);
        Cells = new GameObject[Lenght, Widht];
        //boundsSizeLenght = BuildCell.GetComponent<BoxCollider>().bounds.size.x;
        boundsSizeLenght = 2f;

        float StartPositionX = transform.position.x;
        float StartPositionZ = transform.position.z;
        for (int z = 0; z < Lenght; z++)
        {
            for (int x = 0; x < Widht; x++)
            {
                Cells[x, z] = Instantiate(BuildCell, new Vector3(x * boundsSizeLenght, 0, boundsSizeLenght * z), Quaternion.identity);
                Cells[x, z].transform.parent = NewGridParent.transform;
                Cells[x, z].tag = "Undestructable";
            }
        }
        var bc = NewGridParent.AddComponent<BoxCollider>();
        bc.size = new Vector3(100, 2, 100);
        bc.center = new Vector3(50, 0, 50);
        bc.isTrigger = true;
        NewGridParent.transform.position = new Vector3(-Widht, 0, -Lenght);
        CitizenNavMeshManager.instance.teleportArea = bc;
    }
}