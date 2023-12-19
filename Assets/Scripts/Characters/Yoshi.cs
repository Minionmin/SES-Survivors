using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoshi : Player
{
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
        // ベースクラスのアップデート
        base.Update();


    }

    protected override void LateUpdate()
    {   
        // 物理演算の移動
        base.LateUpdate();
    }
}
