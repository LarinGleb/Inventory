
using UnityEngine;
using System;

// Need to copypast: --------------------

public class Item : MonoBehaviour
{
    
    private enum ItemType
    {
        WEAPON,
        COIN,
        ARMOR,
        AIRWEAPON,
        ITEM
    }

    private const int DEFAULT_MAX_COUNT = 99;
    private const ItemType DEFAULT_TYPE_ITEM = ItemType.ITEM;
    private const int DEFAULT_ID_ITEM = -1;
    private const string DEFAULT_NAME_ITEM = "Item";
    private const string DEFAULT_DESC_ITEM = "None";

    [Header("--------------------Required fields--------------------")]

    [Tooltip("The maximum amount of this item that can be contained in the inventory. \nDon't worry: the script itself will supply the maximum number of items for their various types.")]
    [Range(1, 99)]
    [SerializeField] private int _maxCount = DEFAULT_MAX_COUNT;

    [Tooltip("The type of this item.")]
    [SerializeField] private ItemType _typeItem = DEFAULT_TYPE_ITEM;

    [Tooltip("The ID of this item.")]
    [SerializeField] private int _idItem = DEFAULT_ID_ITEM;

    [Tooltip("The name of this item (it will be displayed in the inventory).")]
    [SerializeField] private string _nameItem = DEFAULT_NAME_ITEM;

    [Header("--------------------Nonrequired fields--------------------")]

    [TextArea]
    [Tooltip("Description of the item. You can leave it empty and nothing will be displayed in the inventory.")]
    [SerializeField] private string _descriptionItem = DEFAULT_DESC_ITEM;

    private void Start() {
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

    public void ClearItem() {
        _maxCount = DEFAULT_MAX_COUNT;
        _typeItem = DEFAULT_TYPE_ITEM;
        _idItem = DEFAULT_ID_ITEM;
        _nameItem = DEFAULT_NAME_ITEM;
        _descriptionItem = DEFAULT_DESC_ITEM;
    }
}
