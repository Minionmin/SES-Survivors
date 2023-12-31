using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIPerk : YoshiPerk
{
    /// <summary> 追加していく矢のプレハブ </summary>
    [SerializeField] private Arrow arrow;

    public override void Apply()
    {
        base.Apply();

        Player player = GameManager.Instance.player;

        // プレイヤーのキャラクターがYoshiであるかどうかを確認する
        if (player.TryGetComponent<Yoshi>(out Yoshi yoshi))
        {
            // 既にバフを所持している場合（このようなことはないが、後で修正）（テスト用）
            if (yoshi.IsArrowI) return;

            // このバフを取得したフラグを立てる
            yoshi.IsArrowI = true;

            // 矢の数を増やす
            Arrow secondArrow = Instantiate(arrow, ProjectileManager.Instance.transform);

            // プレイヤーのクラスに矢を格納する
            yoshi.arrows.Add(secondArrow);
        }
    }
}
