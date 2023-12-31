using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Yoshi : Player
{
    /// <summary> 所持している武器 </summary>
    public List<Arrow> arrows;

    /// <summary> 矢数のバフ1を取得したかどうかのフラグ </summary>
    public bool IsArrowI = false;

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
        foreach (var arrow in arrows)
        {
            arrow.Launch();
        }

        // 攻撃可能フラグを下ろす
        canAttack = false;
    }

    protected override void LateUpdate()
    {   
        // 物理演算の移動
        base.LateUpdate();
    }


}
