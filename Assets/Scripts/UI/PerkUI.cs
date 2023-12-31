using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    /// <summary> バフ本体 </summary>
    public List<Button> perkButtons;

    /// <summary> バフの説明テキスト </summary>
    [SerializeField] private List<Text> perkTexts;

    private void Start()
    {
        GameManager.OnLevelUp += GameManager_OnLevelUp;
    }

    /// <summary> プレイヤーがレベルアップするときに表示 </summary>
    private void GameManager_OnLevelUp()
    {
        Show();
    }

    /// <summary> バフの説明を設定する </summary>
    public void SetPerkText(int index, string textToSet)
    {
        perkTexts[index].text = textToSet;
    }

    /// <summary> バフの機能を解除する </summary>
    public void UnsubscribePerks()
    {
        // こうすることで、バフの機能が次回と重複しない
        foreach (var perkButton in perkButtons)
        {
            perkButton.onClick.RemoveAllListeners();
        }
    }

    /// <summary> バフのUIを表示する </summary>
    public void Show()
    {
        // バフがなくなったら何もしない
        if (PerkManager.Instance.allPerks.Count == 0) return;

        foreach(var perkButton in perkButtons)
        {
            perkButton.gameObject.SetActive(true);
        }

        // バフをランダムする
        PerkManager.Instance.RandomizePerk();

        // バフを選んでいる時に時間を止める
        Time.timeScale = 0;
    }

    /// <summary> バフのUIを非表示する </summary>
    public void Hide()
    {
        foreach (var perkButton in perkButtons)
        {
            perkButton.gameObject.SetActive(false);
        }
    }
}
