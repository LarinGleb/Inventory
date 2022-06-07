using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [Tooltip("Cells in inventory.")]
    [SerializeField] private Cell[] _cellsInventory;

    private Cell[] _busyCells;


    public void AddItem(Item itemAdd, int countItem) {
        foreach (Cell busyCell in _busyCells)
        {
            if (busyCell.Full) continue;

            if (busyCell.Item.ID == itemAdd.ID) {
                
            }
        }
    }
}
