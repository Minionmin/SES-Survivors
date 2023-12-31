using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance;

    // プレイヤーのキャラクターに付与可能なバフ
    public List<Perk> allPerks;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary> バフをランダムする </summary>
    public void RandomizePerk()
    {
        int randomPerknum;

        // バフの数が3以下の場合
        if(allPerks.Count < 3)
        {
            randomPerknum = allPerks.Count;
        }
        else
        {
            randomPerknum = 3;
        }

        // 同じバフが出現しないようにリストを生成する
        List<int> IgnoredIndex = new List<int>();

        for (int i = 0; i < randomPerknum; i++)
        {
            // 同じインデクスを防ぐためにインデクスを覚えておく
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, allPerks.Count);
            }
            while (IgnoredIndex.Contains(randomIndex));
            IgnoredIndex.Add(randomIndex);

            // スクリプトを直接追加できないため、仮のオブジェクトを作成しておく
            GameObject dummyPerkObject = Instantiate(allPerks[randomIndex]).gameObject;
            Perk dummyPerk = dummyPerkObject.GetComponent<Perk>();


            PerkUI perkUI = UIManager.Instance.perkUI;
            UIManager.Instance.perkUI.perkButtons[i].onClick.AddListener(() =>
            {
                // そのバフの効果を付与してから配列から削除する
                dummyPerk.Apply();

                allPerks.Remove(allPerks[randomIndex]);
                Destroy(dummyPerk.gameObject);

                // バフを選ぶとゲームに戻る
                Time.timeScale = 1;

                // イベントのサブスクを解除しないと、今度バフが出現するときにバフが重複してしまう
                perkUI.UnsubscribePerks();

                // バフのUIを非表示
                perkUI.Hide();
            });
        }
    }
}
