using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerRunningSpeed;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity;
    
    private Vector2 playerMoveInputVector;
    private Vector3 playerMoveInputVector3D;
    private Vector2 playerLookInputVector;
    private Rigidbody playerRigidbody;
    private readonly float topClamp = 90f;
    private readonly float bottomClamp = -90f;
    private float xRotation = 0f;
    private readonly float threshold = 0.1f;

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
        if(playerMoveInputVector.sqrMagnitude < threshold) return;
        float speed = GameInputs.Instance.IsSprintPressed() ? playerRunningSpeed : playerMovementSpeed;
        playerMoveInputVector3D = transform.right*playerMoveInputVector.x + transform.forward*playerMoveInputVector.y;
        playerRigidbody.AddForce(playerMoveInputVector3D.normalized * (speed * Time.deltaTime));
    }

    private void HandleCameraMovement()
    {
        playerLookInputVector = GameInputs.Instance.GetLookInputVector();
        if (!(playerLookInputVector.sqrMagnitude > threshold)) return;
        float lookInputX = playerLookInputVector.x;
        float lookInputY = playerLookInputVector.y;
        transform.Rotate(Vector3.up * (lookInputX * sensitivity));
        xRotation -= lookInputY * sensitivity;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f,0f);
    }
}
