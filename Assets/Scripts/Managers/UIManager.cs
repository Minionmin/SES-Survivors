using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    /// <summary> ƒŒƒxƒ‹‚ÌUI </summary>
    public ExpUI expUI;

    /// <summary> ƒoƒt‚ÌUI </summary>
    public PerkUI perkUI;

    private void Awake()
    {
        Instance = this;
    }
}
