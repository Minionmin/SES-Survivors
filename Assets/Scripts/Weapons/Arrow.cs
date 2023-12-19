using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] protected int power = 15;

    [SerializeField] protected float moveSpeed;

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

        // 弾を非表示してから位置・方向をリセットする
        Hide();

        moveDir = player.transform.forward;
        transform.position = player.bulletSpawnPoint.position;

        // リセットしたら表示にする
        Show();
    }

    /// <summary> 弾をの挙動 </summary>
    public virtual void Travel()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
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
