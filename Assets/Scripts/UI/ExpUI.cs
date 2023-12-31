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
        // �Q�[���}�l�[�W���[���o���l���v�Z����ۂɌĂԊ֐�
        GameManager.OnExpGained += GameManager_OnExpGained;

        // EXP Bar������������
        InitExpUI();
    }

    /// <summary>EXP��UI���A�b�v�f�[�g����</summary>
    private void GameManager_OnExpGained()
    {
        float timeToFill = 0.1f;
        UpdateExpBar(timeToFill);
        UpdateExpLabel();
    }

    /// <summary>EXP��UI������������</summary>
    private void InitExpUI()
    {
        float fillAmount = (float)GameManager.Instance.player.exp / GameManager.Instance.player.maxExp;
        expGaugeDiff.fillAmount = fillAmount;
        expGauge.fillAmount = fillAmount;

        UpdateExpLabel();
    }

    /// <summary>�A�j���[�V�������Đ����Ȃ���EXP��UI���A�b�v�f�[�g����</summary>
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

    /// <summary>���x���e�L�X�g���A�b�v�f�[�g����</summary>
    private void UpdateExpLabel()
    {
        expLabel.text = GameManager.Instance.player.lvl.ToString();
    }
}
