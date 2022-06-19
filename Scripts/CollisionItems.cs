
using UnityEngine;

public class CollisionItems : MonoBehaviour
{
    [Tooltip("Inventory script")]
    [SerializeField] private Inventory _inv;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Item" && !Inventory.Full) {
            Item item = other.gameObject.GetComponent<Item>();
            Destroy(other.gameObject);
            Inventory.AddItemToInv(item, 1);
            
        }

    }

    
}
