using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerRunningSpeed;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float sensitivity;
    [SerializeField] private LayerMask weaponLayer;
    [SerializeField] private Transform weaponHolderTransform;
    [SerializeField] private Transform cameraPivotTransform;
    
    private Vector2 playerMoveInputVector;
    private Vector3 playerMoveInputVector3D;
    private Vector2 playerLookInputVector;
    private Rigidbody playerRigidbody;
    private readonly float topClamp = 90f;
    private readonly float bottomClamp = -90f;
    private float xRotation = 0f;
    private readonly float threshold = 0.1f;
    private float weaponInteractionDistance = 3f;

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
        WeaponPickup();
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
        float lookInputX = playerLookInputVector.x* sensitivity;
        float lookInputY = playerLookInputVector.y* sensitivity;
        transform.Rotate(Vector3.up * (lookInputX ));
        xRotation -= lookInputY ;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f,0f);
    }

    private void WeaponPickup()
    {
        if (Physics.Raycast(cameraPivotTransform.position, cameraPivotTransform.forward, out RaycastHit hit,
                weaponInteractionDistance,weaponLayer))
        {
            if (GameInputs.Instance.IsInteractPressed())
            {
                hit.transform.SetParent(weaponHolderTransform);
                hit.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
            }
        }
    }
}
