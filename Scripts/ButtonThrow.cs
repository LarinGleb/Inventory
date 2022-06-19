using UnityEngine;

public class ButtonThrow : MonoBehaviour
{
    [Tooltip("Object-parent main with throw script")]
    [SerializeField] private Throw _throwMenu;

    public void Throw() {
        _throwMenu.ThrowItems();
    }

    public void Cancel() {
        _throwMenu.SetSliderValueDefault();
        _throwMenu.gameObject.SetActive(false);
    }
}
