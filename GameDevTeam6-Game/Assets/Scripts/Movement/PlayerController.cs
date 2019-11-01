using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] float moveSpeed = 10f;

    //Reference Variables
    private Rigidbody2D playerRB;

    private void Awake() {
        FindPlayerRB();
    }

    private void FindPlayerRB() {
        playerRB = GetComponent<Rigidbody2D>();
        if (!playerRB) {
            Debug.LogError("No Rigidbody2D Component Found on Player!");
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        playerRB.velocity = new Vector2(deltaX, deltaY);
    }
}
