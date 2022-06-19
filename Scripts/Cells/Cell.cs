using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, ICloneable, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler {

    private const int DEFAULT_MAX_COUNT = 0;
    private const ItemType DEFAULT_TYPE_ITEM = ItemType.ITEM;
    private const int DEFAULT_ID_ITEM = -1;
    private const string DEFAULT_NAME_ITEM = "Item";
    private const string DEFAULT_DESC_ITEM = null;
    private const int DEFAUT_COUNT = 0;
    
    [Tooltip("Default icon")]
    [SerializeField] private Sprite DEFAULT_ICON;

    private Vector2 _imageSize;
    private Vector2 _deltaSize = new Vector2(0.15f, 0.15f);
    private bool _throw = false;
    public bool Throw {
        get {return _throw;}
        set {_throw = value;}
    }

    private Vector2 _startPosition;
    public Vector2 ImagePosition {
        get {return _startPosition;}
    }

    private bool _fullCell = false;
    public bool Full {
        get {return _fullCell;}
    }

    private Cell __self__;
    private Inventory _inv;

    public object Clone()
    {
        return MemberwiseClone();
    }

    //-------------------------- for item --------------------------

    private int _countItems = DEFAUT_COUNT;
    public int CountItems {
        get {return _countItems;}
    }

    private int _countMaxItems = DEFAULT_MAX_COUNT;
    public int MaxItems {
        get {return _countMaxItems;}
    }

    private int _idItem = DEFAULT_ID_ITEM;
    public int ID {
        get {return _idItem;}
    }

    private string[] _infoItem = new string[2] {DEFAULT_NAME_ITEM, DEFAULT_DESC_ITEM};
    public string[] InfoItem {
        get {return _infoItem;}
    }

    private ItemType _typeItem = DEFAULT_TYPE_ITEM;
    public ItemType TypeItem {
        get {return _typeItem;}
    }

    private Sprite _iconItem;
    public Sprite Icon {
        get {return _iconItem;}
    }

    private Sprite _spriteObject;
    public Sprite SpriteObject {
        get {return _spriteObject;}
    }

    private Vector2 _sizeObject;
    public Vector2 SizeItem {
        get {return _sizeObject;}
    }

    private Vector3 _transformObject;
    public Vector3 TransformObject {
        get {return _transformObject;}
    }
    //----------------------------------------------------


    [Tooltip("A text which show number of item in cell.")]
    [SerializeField] private TextMeshProUGUI  _textCountItems;
    public void SetText(string text = "") {
        _textCountItems.text = text;
    }    

    [Tooltip("A icon which show item icon.")]
    [SerializeField] private Image _imageIcon;

    private void Awake() {
        __self__ = this;
        _imageSize = new Vector2(_imageIcon.rectTransform.sizeDelta.x, _imageIcon.rectTransform.sizeDelta.y);
        _startPosition = transform.position;
    }

    public void Info() {
        Debug.Log("Name: " + gameObject.name + "\n CountItems: " + CountItems + "\n InfoItem: " + InfoItem[0] + " " + InfoItem[1] +"\n ID item: " + ID);
    }

    ///<summary>
    /// Sets a count items in cell. Called after add a couple of items.
    ///</summary>

    public void SetCount() {
        _textCountItems.text = _countItems + "";
    }

    public void SetInv(Inventory inv) {
        _inv = inv;
    }

    public void SetDefault() {
        SetItem(DEFAULT_MAX_COUNT, DEFAULT_TYPE_ITEM, DEFAULT_ID_ITEM, DEFAULT_NAME_ITEM, DEFAULT_DESC_ITEM, DEFAULT_ICON, DEFAUT_COUNT, null, new Vector2 (0, 0), new Vector3 (0, 0, 0));
    }
    public void SetItem(int max, ItemType type, int id, string name, string desc, Sprite icon, int count, Sprite sprite, Vector2 size, Vector3 TransformObject) {
        _idItem = id;
        _countMaxItems = max;
        _infoItem = new string[]{name, desc};
        _typeItem = type;
        _iconItem = icon;
        _countItems = count;
        _fullCell = _countItems == _countMaxItems;
        _spriteObject = sprite;
        _sizeObject = size;
        _transformObject = TransformObject;
    }

    public void NewItems(Item itemSet, int count) {
        _idItem = itemSet.ID;
        _countMaxItems = itemSet.MaxCount;
        _infoItem = new string[]{itemSet.Name, itemSet.Description};
        _typeItem = itemSet.Type;
        _iconItem = itemSet.Icon;
        _sizeObject = itemSet.gameObject.GetComponent<BoxCollider2D>().size;
        _spriteObject = itemSet.gameObject.GetComponent<SpriteRenderer>().sprite;
        _transformObject = itemSet.gameObject.transform.localScale;
        NewCountItem(itemSet, count);
    }

    public void NewCountItem(Item itemSet, int count) {
        _countItems += count;
        _fullCell = _countItems == _countMaxItems;
    }

    public void DeleteItem(int count) {
        _countItems -= count;
        _fullCell = false;
        if (_countItems == 0) {
            SetDefault();
        } 
        Visualise();
    }

    ///<summary>
    /// Sets a icon of item in cell. Called after add a new item to void cell.
    ///</summary>
    public void SetIcon() {
        _imageIcon.sprite  = (_idItem == -1) ? null : _iconItem;
    }
    
    ///<summary>
    /// Visualise text and icon of cell. (text = count items, icon = icon item)
    ///</summary>
    public void Visualise() {
        
        _imageIcon.rectTransform.sizeDelta = _imageSize;
        SetCount();
        SetIcon();
    }

    public void OnPointerEnter(PointerEventData eventData)
	{

        if (this.ID != -1) {
            _imageIcon.rectTransform.sizeDelta += _deltaSize;
        }
        if (_inv.OnDrag) {
            _inv.ChoosenCell = this; 
            _imageIcon.rectTransform.sizeDelta += _deltaSize;
        }  
	} 


    public void OnPointerExit(PointerEventData eventData)
	{
        
        _inv.ChoosenCell = null;
		_imageIcon.rectTransform.sizeDelta = _imageSize;

	} 

    // ------ FOR MOVE ITEMS --------------
    public void OnBeginDrag(PointerEventData eventData) {
        if (this.ID == -1) {return;}
        _inv.OnDrag = true;
        this.SetText();
	} 


    public void OnDrag(PointerEventData eventData)
    {
        _inv.GetCamera().DragObjectToMouse(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData) {
        
        transform.position = _startPosition;
        _inv.OnDrag = false;
        if (_throw) {
            
            _throw = false;
            if (MaxItems == 1 || _countItems == 1) {
                _inv.GetThrowMenu().SettingMenu(CountItems, _spriteObject, ref __self__);
                _inv.GetThrowMenu().ThrowItems();
            }
            else {
                _inv.GetThrowMenu().gameObject.SetActive(true);
                _inv.GetThrowMenu().SettingMenu(CountItems, _spriteObject, ref __self__);
            }
        }
        if (_inv.ChoosenCell != null) {
            Cell copy = (Cell)this.Clone();
            this.SetItem(_inv.ChoosenCell.MaxItems, _inv.ChoosenCell.TypeItem, _inv.ChoosenCell.ID, _inv.ChoosenCell.InfoItem[0], _inv.ChoosenCell.InfoItem[1], _inv.ChoosenCell.Icon, _inv.ChoosenCell.CountItems, _inv.ChoosenCell.SpriteObject, _inv.ChoosenCell.SizeItem, _inv.ChoosenCell.TransformObject);
            _inv.ChoosenCell.SetItem(copy.MaxItems, copy.TypeItem, copy.ID, copy.InfoItem[0], copy.InfoItem[1], copy.Icon, copy.CountItems, copy.SpriteObject, copy.SizeItem, copy.TransformObject);
            _inv.ChoosenCell.Visualise(); 
            
        }
        _inv.ChoosenCell = null;

        this.Visualise();
        _startPosition = transform.position;
	} 

    
}
