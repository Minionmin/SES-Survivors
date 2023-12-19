using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Create Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public float hp;
    public float stamina;
    public float power;
    public float movespeed;
    public float rotateSpeed;
}
