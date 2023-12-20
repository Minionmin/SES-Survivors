using UnityEngine;

/// <summary> 移動機能を持つオブジェクト用のインタフェース </summary>
public interface IMovement
{
    void Move();
    void Rotate(Vector3 targetDir);
}
