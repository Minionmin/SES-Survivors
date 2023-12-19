using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    protected override void Awake()
    {
        // 初期化
        base.Awake();
    }

    protected override void LateUpdate()
    {
        // プレイヤーを追いかける
        base.LateUpdate();
    }

    /// <summary> 死亡前の処理 </summary>
    public override void Die()
    {
        // アニメーション + フラグ
        base.Die();

        // 死亡アニメーションを再生
        animator.SetTrigger(DIE);

        // マテリアルのフェードプロパティを取得
        var startDissolve = material.GetFloat("_Dissolve");

        // 死亡エフェクトを再生して、モデルを隠す
        DOTween.To(() => startDissolve, Dissolve, 0.0f, 0.5f)
            .onComplete = () => { Hide(); };
    }

    /// <summary> 倒された時にフェードアウトする </summary>
    private void Dissolve(float val)
    {
        material.SetFloat("_Dissolve", val);
    }
}
