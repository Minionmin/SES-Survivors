using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : Weapon
{
    /// <summary> 武器の威力 </summary>
    [SerializeField] protected int power = 15;

    /// <summary> 武器の移動スピード</summary>
    [SerializeField] protected float moveSpeed;

    /// <summary> 武器の移動方向 </summary>
    private Vector3 moveDir = Vector3.zero;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(power);
            Hide();
        }
    }

    protected override void LateUpdate()
    {
        // 直線に動く
        Travel();
    }

    /// <summary> 弾を発射する </summary>
    public void Launch()
    {
        var player = GameManager.Instance.player;

        if(player.TryGetComponent<Yoshi>(out Yoshi yoshi))
        {
            // 弾を非表示してから位置・方向をリセットする
            Hide();
            transform.position = player.bulletSpawnPoint.position;

            // 1つ目の矢の方向を設定
            if (yoshi.arrows.Count > 0)
            {
                yoshi.arrows[0].SetMoveDir(yoshi.transform.forward);
                yoshi.arrows[0].SetRotation(Quaternion.identity);
            }

            // 矢数のバフ1を取得した場合
            if (yoshi.IsArrowI)
            {
                // 2つ目の矢の方向を設定
                Quaternion dirQuaternion = Quaternion.AngleAxis(-45f, Vector3.up);
                Vector3 targetDir = dirQuaternion * player.transform.forward;
                yoshi.arrows[1].SetMoveDir(targetDir);

                // 2つ目の矢を任意の方向に回転
                Quaternion rotQuaternion = Quaternion.AngleAxis(-45f, Vector3.up);
                yoshi.arrows[1].SetRotation(rotQuaternion);
            }

            // リセットしたら表示にする
            Show();
        }
    }

    /// <summary> 弾をの挙動 </summary>
    public virtual void Travel()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary> 弾の移動方向をセットする </summary>
    public void SetMoveDir(Vector3 targetDir)
    {
        moveDir = targetDir;
    }

    /// <summary> 弾の回転をセットする </summary>
    public void SetRotation(Quaternion targetRotation)
    {
        transform.localRotation = GameManager.Instance.player.transform.localRotation * targetRotation;
    }

    /// <summary> 弾を非表示する </summary>
    protected void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary> 弾を表示する </summary>
    protected void Show()
    {
        gameObject.SetActive(true);
    }
}
