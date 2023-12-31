using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance;

    // �v���C���[�̃L�����N�^�[�ɕt�^�\�ȃo�t
    public List<Perk> allPerks;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary> �o�t�������_������ </summary>
    public void RandomizePerk()
    {
        int randomPerknum;

        // �o�t�̐���3�ȉ��̏ꍇ
        if(allPerks.Count < 3)
        {
            randomPerknum = allPerks.Count;
        }
        else
        {
            randomPerknum = 3;
        }

        // �����o�t���o�����Ȃ��悤�Ƀ��X�g�𐶐�����
        List<int> IgnoredIndex = new List<int>();

        for (int i = 0; i < randomPerknum; i++)
        {
            // �����C���f�N�X��h�����߂ɃC���f�N�X���o���Ă���
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, allPerks.Count);
            }
            while (IgnoredIndex.Contains(randomIndex));
            IgnoredIndex.Add(randomIndex);

            // �X�N���v�g�𒼐ڒǉ��ł��Ȃ����߁A���̃I�u�W�F�N�g���쐬���Ă���
            GameObject dummyPerkObject = Instantiate(allPerks[randomIndex]).gameObject;
            Perk dummyPerk = dummyPerkObject.GetComponent<Perk>();


            PerkUI perkUI = UIManager.Instance.perkUI;
            UIManager.Instance.perkUI.perkButtons[i].onClick.AddListener(() =>
            {
                // ���̃o�t�̌��ʂ�t�^���Ă���z�񂩂�폜����
                dummyPerk.Apply();

                allPerks.Remove(allPerks[randomIndex]);
                Destroy(dummyPerk.gameObject);

                // �o�t��I�ԂƃQ�[���ɖ߂�
                Time.timeScale = 1;

                // �C�x���g�̃T�u�X�N���������Ȃ��ƁA���x�o�t���o������Ƃ��Ƀo�t���d�����Ă��܂�
                perkUI.UnsubscribePerks();

                // �o�t��UI���\��
                perkUI.Hide();
            });
        }
    }
}
