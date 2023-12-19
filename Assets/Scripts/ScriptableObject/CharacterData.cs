using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Create object data/Character data", order = 0)]
public class CharacterData : ScriptableObject
{
    public float hp;
    public float power;
    public float stamina;
    public float movespeed;
    public float rotateSpeed;
}
