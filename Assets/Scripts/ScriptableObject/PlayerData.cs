using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Create object data/Player data", order = 0)]
public class PlayerData : ScriptableObject
{
    public int hp;
    public float stamina;
    public float movespeed;
    public float rotateSpeed;
    public float attackCooldownMax;
    public int maxEXP;
}
