using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    /// <summary> xÌUI </summary>
    public ExpUI expUI;

    private void Awake()
    {
        Instance = this;
    }
}
