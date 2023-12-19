using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> プレイヤーの参照 </summary>
    public Player player;

    private void Awake()
    {
        Instance = this;
    }
}
