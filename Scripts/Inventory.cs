using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

using cellFunc = System.Action<Cell, Item, int>;
using checkItemFunc =System.Predicate<Item>;
using chekcCellFunc = System.Func<Cell, Item, bool>;

public class Inventory : MonoBehaviour
{
    
    private static List<Cell> _cellsInventory = new List<Cell>();
    private static CoinCell _coinCell;
    private static Cell _throwCell;

    private Cell _choosenCell;
    public Cell ChoosenCell {
        get {return _choosenCell;}
        set {_choosenCell = value;}
    }
    
    private bool _onDrag = false;
    public bool OnDrag {
        get {return _onDrag;}
        set {_onDrag = value;}
    }

    [Tooltip("Throw menu script")]
    [SerializeField] private Throw _throwMenu;
    public Throw GetThrowMenu() {
        return _throwMenu;
    }
    
    [SerializeField] private CameraManager _camera;
    public CameraManager GetCamera() {
        return _camera;
    }

    private static bool _hasSpace = true;
    public static bool Full {
        get {return !_hasSpace;}
    }

    private static cellFunc NewCountItem = (cell, itemAdd, count) => cell.NewCountItem(itemAdd, count);
    private static cellFunc NewItems = (cell, itemAdd, count) => cell.NewItems(itemAdd, count);

    private static chekcCellFunc emptyCell = (cell, itemAdd) => (cell.ID == -1);
    private static chekcCellFunc correctCellForItem = (cell, itemAdd) => (!cell.Full && cell.ID == itemAdd.ID);

    private static checkItemFunc hasCellWithItem = (itemAdd) => _cellsInventory.Where(Cell => !Cell.Full && Cell.ID == itemAdd.ID).Count() != 0;
    private static checkItemFunc existSpace = (itemAdd) => _cellsInventory.Where(Cell => correctCellForItem(Cell, itemAdd) || emptyCell(Cell, null)).Count() != 0;

    private void Start() {
        SetChildrenCell();
    }

    private static int ParsingInventory(Item itemAdd, int countItem, chekcCellFunc findCell, cellFunc actionCell) {
        foreach (Cell cell in _cellsInventory.Where(Cell => findCell(Cell, itemAdd)))
        {
            if (countItem <= 0) return 0;
            int space = (emptyCell(cell, null)) ? itemAdd.MaxCount: cell.MaxItems - cell.CountItems;
            actionCell(cell, itemAdd, (space >= countItem) ? countItem : space);
            countItem -= (space >= countItem)? countItem: space;
            
        }
        return countItem;
    }

    public static void AddItemToInv(Item itemAdd, int countItem) {

        switch (itemAdd.Type)
        {
            case ItemType.COIN: {
                _coinCell.AddCoin(countItem);
                break;
            }

            default: {
                int countToAdd = countItem; 
                countToAdd = ParsingInventory(itemAdd, countItem, (hasCellWithItem(itemAdd)) ? correctCellForItem:emptyCell, (hasCellWithItem(itemAdd)) ? NewCountItem: NewItems);
                _hasSpace = existSpace(itemAdd);
                foreach(Cell cell in _cellsInventory) {
                    cell.Visualise();
                }

                if (countToAdd > 0) {AddItemToInv(itemAdd, countToAdd);}
                break;
            }
            
            
        }

    } 

    private void SetChildrenCell() {
        for (int i = 0; i < gameObject.transform.childCount; i ++) {
            GameObject possibleCell = transform.GetChild(i).gameObject;
            switch(possibleCell.tag) {
                case "Cell": {Cell cell = possibleCell.GetComponent<Cell>(); _cellsInventory.Add(cell); cell.SetInv(this);  break; }
                case "CellCoin": {_coinCell = possibleCell.GetComponent<CoinCell>(); break;}
            }
        }
    }


}
