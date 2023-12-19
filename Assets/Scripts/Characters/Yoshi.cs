using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoshi : Player
{
    /// <summary> 所持している武器 </summary>
    [SerializeField] private List<Arrow> arrows;

    protected override void Awake()
    {
        // 初期化
        base.Awake();
    }

    protected override void Start()
    {
        // 他オブジェクトとの初期化
        base.Start();
    }

    protected override void Update()
    {
        // 攻撃フラグの処理
        base.Update();

        // 攻撃できないなら何もしない
        if (!canAttack) return;

        // 弾の方向・位置をリセット
        Attack();
    }

    /// <summary> 攻撃を始める </summary>
    private void Attack()
    {
        arrows[0].Launch();

        // 攻撃可能フラグを下ろす
        canAttack = false;
    }

    protected override void LateUpdate()
    {   
        // 物理演算の移動
        base.LateUpdate();
    }
}
