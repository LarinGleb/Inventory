using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{

    [Tooltip("Cells in inventory.")]
    [ContextMenuItem("Set children as cells", "SetChildrenCell")]
    [SerializeField] private List<Cell> _cellsInventory;

    private delegate bool correctCell(Cell cell);

    private int ParsingInventory(correctCell func, int countToAdd) {
        foreach (Cell cell in _cellsInventory.Where(Cell => func(Cell)).ToList())
        {
            int space = cell.Item.MaxCount - cell.CountItems;
            cell.AddItem((space >= countToAdd) ? countToAdd : space);
            countToAdd -= (space >= countToAdd)? countToAdd: space;
            if (countToAdd == 0) return 0;
        }
        return countToAdd;
    }

    public void AddItem(Item itemAdd, int CountItems) {
        int countToAdd = CountItems;    
        countToAdd = ParsingInventory(Cell => (!Cell.Full && Cell.Item.ID == itemAdd.ID), countToAdd);
        countToAdd = ParsingInventory(Cell => (Cell.Item.ID == -1), countToAdd);

    } 

    private void SetChildrenCell() {
        for (int i = 0; i < gameObject.transform.childCount; i ++) {
            GameObject possibleCell = transform.GetChild(i).gameObject;
            if (possibleCell.tag == "Cell") {
                _cellsInventory.Add(possibleCell.GetComponent<Cell>());
            }
        }
    }


}
