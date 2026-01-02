using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance{ get; private set; }
    
    private InputActions inputActions;
    private Vector2 moveInputVector;
    private Vector2 lookInputVector;
    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
    }

    private void Update()
    {
        moveInputVector = inputActions.Player.Move.ReadValue<Vector2>();
        lookInputVector = inputActions.Player.Look.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    public Vector2 GetMoveInputVector()
    {
        return moveInputVector;
    }

    public Vector2 GetLookInputVector()
    {
        return lookInputVector;
    }

    public bool IsSprintPressed()
    {
        return inputActions.Player.Sprint.IsPressed();
    }
    public bool IsInteractPressed()
    {
        return inputActions.Player.Interact.IsPressed();
    }
}
