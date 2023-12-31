using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private Text expLabel;
    [SerializeField] private Image expGauge;
    [SerializeField] private Image expGaugeDiff;

    private Sequence expBarSeq;

    private void Start()
    {
        // ゲームマネージャーが経験値を計算する際に呼ぶ関数
        GameManager.OnExpGained += GameManager_OnExpGained;

        // EXP Barを初期化する
        InitExpUI();
    }

    /// <summary>EXPのUIをアップデートする</summary>
    private void GameManager_OnExpGained()
    {
        float timeToFill = 0.1f;
        UpdateExpBar(timeToFill);
        UpdateExpLabel();
    }

    /// <summary>EXPのUIを初期化する</summary>
    private void InitExpUI()
    {
        float fillAmount = (float)GameManager.Instance.player.exp / GameManager.Instance.player.maxExp;
        expGaugeDiff.fillAmount = fillAmount;
        expGauge.fillAmount = fillAmount;

        UpdateExpLabel();
    }

    /// <summary>アニメーションも再生しながらEXPのUIをアップデートする</summary>
    private void UpdateExpBar(float timeToFill = 0f)
    {
        float fillAmount = (float)GameManager.Instance.player.exp / GameManager.Instance.player.maxExp;

        expGaugeDiff.fillAmount = fillAmount;

        expBarSeq?.Kill();
        expBarSeq = DOTween.Sequence()
            .SetLink(gameObject)
            .SetDelay(0.5f)
            .Append(expGauge.DOFillAmount(fillAmount, 0.1f));
    }

    /// <summary>レベルテキストをアップデートする</summary>
    private void UpdateExpLabel()
    {
        expLabel.text = GameManager.Instance.player.lvl.ToString();
    }
}
