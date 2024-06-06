using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingContentManager : MonoBehaviour
{
    [SerializeField] public ChooseBlockCell[] CellsInGrid;

    //public void SaveCellsUnlockState()
    //{
    //    List<bool> CellsState = new();
    //    for (int i = 0; i < CellsInGrid.Length; i++)
    //    {
    //        CellsState.Add(CellsInGrid[i].IsOpened);
    //    }
    //    SaverBuildingMenu.instance.SaveItemsState(CellsState, this);
    //}
}
