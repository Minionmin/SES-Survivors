using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    /// <summary> �v���C���[�ړ��X�s�[�h </summary>
    [SerializeField] private float movespeed = 2.0f;

    private void LateUpdate()
    {
        transform.position += playerControl.GetMoveDirection() * movespeed * Time.deltaTime;
    }
}
