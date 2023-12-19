using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputControl inputControl;

    private Vector2 moveDir;

    private void Awake()
    {
        inputControl = new InputControl();
        inputControl.Player.Enable();
    }

    private void Update()
    {
        moveDir = inputControl.Player.Move.ReadValue<Vector2>();
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(moveDir.x, 0.0f, moveDir.y).normalized;
    }
}
