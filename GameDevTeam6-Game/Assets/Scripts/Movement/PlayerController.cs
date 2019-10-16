using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Configuration Parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float borderOffset = 2f;
    private float xMin, xMax, yMin, yMax;

    //Reference Variables
    private Camera gameCamera;

    private void Awake() {
        FindGameCamera();
    }

    private void FindGameCamera() {
    }

    private void Start() {
        EstablishMovementBoundaries();
    }

    private void EstablishMovementBoundaries() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + borderOffset;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - borderOffset;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + borderOffset;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - borderOffset;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove() {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPosX = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newPosY = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newPosX, newPosY);
    }
}
