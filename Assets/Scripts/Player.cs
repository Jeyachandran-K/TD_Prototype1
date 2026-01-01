using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity;
    
    private Vector2 playerMoveInputVector;
    private Vector3 playerMoveInputVector3D;
    private Vector2 playerLookInputVector;
    private Rigidbody playerRigidbody;
    private float topClamp = 90f;
    private float bottomClamp = -90f;
    private float xRotation = 0f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        HandleCameraMovement();
    }

    private void HandleMovement()
    {
        playerMoveInputVector = GameInputs.Instance.GetMoveInputVector();
        if(playerMoveInputVector.sqrMagnitude < 0.01f) return;
        playerMoveInputVector3D = transform.right*playerMoveInputVector.x + transform.forward*playerMoveInputVector.y;
        playerRigidbody.AddForce(playerMoveInputVector3D.normalized * (playerMovementSpeed * Time.deltaTime));
    }

    private void HandleCameraMovement()
    {
        playerLookInputVector = GameInputs.Instance.GetLookInputVector();
        if (!(playerLookInputVector.sqrMagnitude > 0.01f)) return;
        float lookInputX = playerLookInputVector.x;
        float lookInputY = playerLookInputVector.y;
        transform.Rotate(Vector3.up * (lookInputX * sensitivity));
        xRotation -= lookInputY * sensitivity;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f,0f);
    }
}
