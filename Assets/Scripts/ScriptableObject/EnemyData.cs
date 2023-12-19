using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Create object data/Enemy data", order = 1)]
public class EnemyData : ScriptableObject
{
    public int hp;
    public float power;
    public float movespeed;
    public float rotateSpeed;
    public float maxDistanceOffset;
    public int dropEXP;
}