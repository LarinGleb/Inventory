
using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    private int _countItems = 0;
    public int CountItems {
        get {return _countItems;}
        set {_countItems = value;}
    }

    private Item _itemIn;
    public Item Item {
        get {return _itemIn;}
        set {_itemIn = value;}
    }

    [Tooltip("A text which show number of item in cell.")]
    [SerializeField] private TextMeshPro _textCountItems;
    
    private bool _fullCell = false;

    public bool Full {
        get {return _fullCell;}
        set {_fullCell = value;}
    }
    
    ///<summary>
    /// Sets a count items in cell. Called after add a couple of items.
    ///</summary>
    public void SetCount() {
        _textCountItems.text = _countItems + "";
    }


    private Sprite _imageIcon;

    ///<summary>
    /// Sets a icon of item in cell. Called after add a new item to void cell.
    ///</summary>

    public void AddItem(int count) {
        _countItems += count;
        _fullCell = count == Item.MaxCount;
        
    }
    public void SetIcon() {
        _imageIcon = _itemIn.Icon;
    }
    
    ///<summary>
    /// Visualise text and icon of cell. (text = count items, icon = icon item)
    ///</summary>
    public void Visualise() {
        SetCount();
        SetIcon();
    }

}
