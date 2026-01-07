using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player  Instance{get; private set;}
    public event EventHandler OnPlayerDeath ;
    
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerRunningSpeed;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private LayerMask weaponLayer;
    [SerializeField] private Transform weaponHolderTransform;
    [SerializeField] private Transform cameraPivotTransform;
    
    private Vector2 playerMoveInputVector;
    private Vector3 playerMoveInputDirection;
    private Vector2 playerLookInputVector;
    private Rigidbody playerRigidbody;
    private readonly float topClamp = 90f;
    private readonly float bottomClamp = -90f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private readonly float weaponInteractionDistance = 3f;

    public enum PlayerState
    {
        Alive,
        Dead
    }
    
    public PlayerState playerState = PlayerState.Alive;

    private void Awake()
    {
        Instance = this;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerLookInputVector = Vector2.zero;
    }
    private void Update()
    {
        ReadInput();
        WeaponPickup();
    }

    private void LateUpdate()
    {
        HandleCameraMovement();
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        Quaternion delta = Quaternion.Euler(0,yRotation,0);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * delta);
    }

    
    private void HandleMovement()
    {
        playerMoveInputVector = GameInputs.Instance.GetMoveInputVector();
        
        float acceleration = GameInputs.Instance.IsSprintPressed() ? playerRunningSpeed : playerMovementSpeed;
        playerMoveInputDirection = transform.right*playerMoveInputVector.x + transform.forward*playerMoveInputVector.y;
        playerRigidbody.AddForce(playerMoveInputDirection.normalized * acceleration,ForceMode.Force );
    }

    private void HandleCameraMovement()
    {
        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f,0f);
    }

    private void WeaponPickup()
    {
        if (Physics.Raycast(cameraPivotTransform.position, cameraPivotTransform.forward, out RaycastHit hit,
                weaponInteractionDistance, weaponLayer))
        {
            if (GameInputs.Instance.IsInteractPressed())
            {
                hit.transform.SetParent(weaponHolderTransform);
                hit.transform.localPosition = Vector3.zero;
                hit.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }

    private void ReadInput()
    {
        playerLookInputVector = GameInputs.Instance.GetLookInputVector();
        float lookInputY = playerLookInputVector.y* ySensitivity;
        float lookInputX = playerLookInputVector.x* xSensitivity;
        xRotation -= lookInputY ;
        xRotation = Mathf.Clamp(xRotation, bottomClamp, topClamp);
        yRotation = lookInputX ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BasicEnemy enemy))
        {
            playerState = PlayerState.Dead;
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}
