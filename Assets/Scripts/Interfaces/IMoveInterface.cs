using UnityEngine;

// 移動機能を持つオブジェクト用のインタフェース
public interface IMovement
{
    void Move();
    void Rotate(Vector3 targetDir);
}
