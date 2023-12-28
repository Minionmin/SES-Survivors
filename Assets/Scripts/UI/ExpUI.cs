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
        GameManager.OnExpGained += GameManager_OnExpGained;

        InitExpBar();
    }

    private void GameManager_OnExpGained()
    {
        float timeToFill = 0.1f;
        UpdateExpBar(timeToFill);
    }

    private void InitExpBar()
    {
        float fillAmount = (float)GameManager.Instance.player.exp / GameManager.Instance.player.maxExp;
        expGaugeDiff.fillAmount = fillAmount;
        expGauge.fillAmount = fillAmount;
    }

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
}
