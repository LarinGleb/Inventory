using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private bool onJump = false;
    [SerializeField] private GameObject _Inventory;
    private Rigidbody2D _playerRigidbody;
    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    private void Update()
    {
        MovePlayer();
        if (Input.GetButton("Jump") && onJump)
            Jump();
        if (Input.GetKeyDown(KeyCode.E)) {
            _Inventory.SetActive((_Inventory.activeSelf) ? false : true);
        }
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }
    private void Jump() => _playerRigidbody.velocity = new Vector2( 0, jumpPower);

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject collisionObject = other.gameObject;
        if(collisionObject.tag == other.collider.gameObject.tag) {
            onJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        GameObject collisionObject = other.gameObject;
        if(collisionObject.tag == other.collider.gameObject.tag) {
            onJump = false;
        }
    }
}