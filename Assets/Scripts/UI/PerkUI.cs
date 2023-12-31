using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUI : MonoBehaviour
{
    /// <summary> �o�t�{�� </summary>
    public List<Button> perkButtons;

    /// <summary> �o�t�̐����e�L�X�g </summary>
    [SerializeField] private List<Text> perkTexts;

    private void Start()
    {
        GameManager.OnLevelUp += GameManager_OnLevelUp;
    }

    /// <summary> �v���C���[�����x���A�b�v����Ƃ��ɕ\�� </summary>
    private void GameManager_OnLevelUp()
    {
        Show();
    }

    /// <summary> �o�t�̐�����ݒ肷�� </summary>
    public void SetPerkText(int index, string textToSet)
    {
        perkTexts[index].text = textToSet;
    }

    /// <summary> �o�t�̋@�\���������� </summary>
    public void UnsubscribePerks()
    {
        // �������邱�ƂŁA�o�t�̋@�\������Əd�����Ȃ�
        foreach (var perkButton in perkButtons)
        {
            perkButton.onClick.RemoveAllListeners();
        }
    }

    /// <summary> �o�t��UI��\������ </summary>
    public void Show()
    {
        // �o�t���Ȃ��Ȃ����牽�����Ȃ�
        if (PerkManager.Instance.allPerks.Count == 0) return;

        foreach(var perkButton in perkButtons)
        {
            perkButton.gameObject.SetActive(true);
        }

        // �o�t�������_������
        PerkManager.Instance.RandomizePerk();

        // �o�t��I��ł��鎞�Ɏ��Ԃ��~�߂�
        Time.timeScale = 0;
    }

    /// <summary> �o�t��UI���\������ </summary>
    public void Hide()
    {
        foreach (var perkButton in perkButtons)
        {
            perkButton.gameObject.SetActive(false);
        }
    }
}
