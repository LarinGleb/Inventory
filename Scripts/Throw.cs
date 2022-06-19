using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Throw : MonoBehaviour
{
    [Tooltip("Slider object")]
    [SerializeField] private Slider _sliderMenu;
    
    [Tooltip("Text with displayed count item to throw")]
    [SerializeField] private TextMeshProUGUI  _textCountItems;

    [Tooltip("Throw point")]
    [SerializeField] private GameObject _throwPoint;

    private Sprite itemSprite;
    private Cell cellThrow;

    private void Update() {
        _textCountItems.text = _sliderMenu.value.ToString();    
    }

    public void SettingMenu(int countMax, Sprite sprite, ref Cell cell) {
        _sliderMenu.maxValue = countMax;
        itemSprite = sprite;
        cellThrow = cell;
    }

    public int GetSliderValue() {
        return (int)_sliderMenu.value;
    }

    public void SetSliderValueDefault() {
        _textCountItems.text = "0";   
    }

    public void ThrowItems() {
        int countItems = GetSliderValue();

        for (int i = 0; i < countItems; i ++) {
            GameObject item = new GameObject(cellThrow.InfoItem[0]);
            item.AddComponent<Rigidbody2D>();
            item.tag = "Item";

            item.transform.localScale = cellThrow.TransformObject;
            SpriteRenderer itemSrend = item.AddComponent<SpriteRenderer>();
            itemSrend.sprite = itemSprite;
            itemSrend.size = cellThrow.SizeItem;

            // Set(int max, ItemType type, int id, string name, string desc, Sprite icon)
            item.AddComponent<Item>().Set(cellThrow.MaxItems, cellThrow.TypeItem, cellThrow.ID, cellThrow.InfoItem[0], cellThrow.InfoItem[1], cellThrow.Icon);
            item.AddComponent<BoxCollider2D>();

            item.transform.position = _throwPoint.transform.position;
        }
        cellThrow.DeleteItem(countItems);
        SetSliderValueDefault();
        itemSprite = null;
        cellThrow = null;

        gameObject.SetActive(false);
    }

}
