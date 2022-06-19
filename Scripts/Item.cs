using UnityEngine;
using System;

// Need to copypast: --------------------

public class Item : MonoBehaviour, ICloneable
{
    private const int DEFAULT_MAX_COUNT = 99;
    private const ItemType DEFAULT_TYPE_ITEM = ItemType.ITEM;
    private const int DEFAULT_ID_ITEM = -1;
    private const string DEFAULT_NAME_ITEM = "Item";
    private const string DEFAULT_DESC_ITEM = null;

    private GameObject _itemObject;
    public GameObject ObjectItem {
        get {return _itemObject;}
        set {_itemObject = value;}
    }

    [Header("--------------------Required fields--------------------")]

    [Tooltip("The maximum amount of this item that can be contained in the inventory. \nDon't worry: the script itself will supply the maximum number of items for their various types.")]
    [Range(1, 99)]
    [SerializeField] private int _maxCount = DEFAULT_MAX_COUNT;

    public int MaxCount {
        get {return _maxCount;}
    } 

    [Tooltip("The type of this item.")]
    [SerializeField] private ItemType _typeItem = DEFAULT_TYPE_ITEM;

    public ItemType Type {
        get {return _typeItem;}
    }

    [Tooltip("The ID of this item.")]
    [SerializeField] private int _idItem = DEFAULT_ID_ITEM;

    public int ID {
        get {return _idItem;}
    }


    [Tooltip("The name of this item (it will be displayed in the inventory).")]
    [SerializeField] private string _nameItem = DEFAULT_NAME_ITEM;
    public string Name {
        get {return _nameItem;}
    }

    [Tooltip("The image of this item (it will be displayed in the inventory).")]
    [SerializeField] private Sprite _iconItem;

    public Sprite Icon {
        get {return _iconItem;}
    }

    [Header("--------------------Nonrequired fields--------------------")]

    [TextArea]
    [Tooltip("Description of the item. You can leave it empty and nothing will be displayed in the inventory.")]
    [SerializeField] private string _descriptionItem = DEFAULT_DESC_ITEM;

    public string Description {
        get {return _descriptionItem;}
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void Set(int max, ItemType type, int id, string name, string desc, Sprite icon) {
        this._maxCount = max;
        this._typeItem = type;
        this._idItem = id;
        this._nameItem = name;
        this._descriptionItem = desc;
        this._iconItem = icon;
    }

    private void Start() {
        _itemObject = this.gameObject;
        switch (_typeItem)
        {
            case ItemType.WEAPON:
            case ItemType.ARMOR:
            case ItemType.AIRWEAPON:
                this._maxCount = 1;
                break;

            default:
                break;
            
        }
    }

    /// <summary>
    /// Resets properties to default
    /// </summary>
    
    public void ClearItem() {
        _maxCount = DEFAULT_MAX_COUNT;
        _typeItem = DEFAULT_TYPE_ITEM;
        _idItem = DEFAULT_ID_ITEM;
        _nameItem = DEFAULT_NAME_ITEM;
        _descriptionItem = DEFAULT_DESC_ITEM;
    }
}
