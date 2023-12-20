using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> �v���C���[�̎Q�� </summary>
    public Player player;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary> �v���C���[�̈ʒu���擾 </summary>
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
}
