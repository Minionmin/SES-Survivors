using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    /// <summary> �C���v�b�g�����N���X </summary>
    private InputControl inputControl;

    /// <summary> �v���C���[�̈ړ����� </summary>
    private Vector2 moveDir;

    private void Awake()
    {
        // �C���v�b�g�����N���X�𐶐�
        inputControl = new InputControl();

        // �C���v�b�g�����N���X��L����
        inputControl.Player.Enable();
    }

    private void Update()
    {
        // �L�[�{�[�h�E�X�e�B�b�N����C���v�b�g�l��ǂ�
        moveDir = inputControl.Player.Move.ReadValue<Vector2>();
    }

    /// <summary> �v���C���[�̈ړ�����Vector3���Q�b�g </summary>
    public Vector3 GetMoveDirection()
    {
        return new Vector3(moveDir.x, 0.0f, moveDir.y).normalized;
    }
}
