using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIPerk : YoshiPerk
{
    /// <summary> �ǉ����Ă�����̃v���n�u </summary>
    [SerializeField] private Arrow arrow;

    public override void Apply()
    {
        base.Apply();

        Player player = GameManager.Instance.player;

        // �v���C���[�̃L�����N�^�[��Yoshi�ł��邩�ǂ������m�F����
        if (player.TryGetComponent<Yoshi>(out Yoshi yoshi))
        {
            // ���Ƀo�t���������Ă���ꍇ�i���̂悤�Ȃ��Ƃ͂Ȃ����A��ŏC���j�i�e�X�g�p�j
            if (yoshi.IsArrowI) return;

            // ���̃o�t���擾�����t���O�𗧂Ă�
            yoshi.IsArrowI = true;

            // ��̐��𑝂₷
            Arrow secondArrow = Instantiate(arrow, ProjectileManager.Instance.transform);

            // �v���C���[�̃N���X�ɖ���i�[����
            yoshi.arrows.Add(secondArrow);
        }
    }
}
