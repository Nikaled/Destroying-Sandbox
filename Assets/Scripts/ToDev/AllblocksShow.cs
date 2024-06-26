using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllblocksShow : MonoBehaviour
{
    public GameObject[] BlockPrefabs;
    public GameObject ReferenceBlock;
    [ContextMenu("Gen Blocks")]
    public void GenerateAllBlocks()
    {
        for (int i = 0; i < BlockPrefabs.Length; i++)
        {
            Instantiate(BlockPrefabs[i], ReferenceBlock.transform.position, ReferenceBlock.transform.rotation);
        }
    }
}
