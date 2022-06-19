using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ThrowCell : MonoBehaviour,  IPointerEnterHandler,  IPointerExitHandler {

    [Tooltip("A icon which show.")]
    [SerializeField] private Image _imageIcon;
    private Vector2 _deltaSize = new Vector2(0.15f, 0.15f);
    private Vector2 _imageSize;

    private void Awake() {
        _imageSize = new Vector2(_imageIcon.rectTransform.sizeDelta.x, _imageIcon.rectTransform.sizeDelta.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
        _imageIcon.rectTransform.sizeDelta += _deltaSize;
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<Cell>().Throw = true;
	} 


    public void OnPointerExit(PointerEventData eventData)
	{
		_imageIcon.rectTransform.sizeDelta = _imageSize;
        if (eventData.pointerDrag != null)
            eventData.pointerDrag.GetComponent<Cell>().Throw = false;

	} 

}