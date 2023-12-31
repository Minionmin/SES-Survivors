using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    /// <summary> レベルのUI </summary>
    public ExpUI expUI;

    /// <summary> バフのUI </summary>
    public PerkUI perkUI;

    private void Awake()
    {
        Instance = this;
    }
}
