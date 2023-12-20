using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] protected int power = 15;

    [SerializeField] protected float moveSpeed;

    private Vector3 moveDir = Vector3.zero;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(power);
            Hide();
        }
    }

    protected override void LateUpdate()
    {
        // �����ɓ���
        Travel();
    }

    /// <summary> �e�𔭎˂��� </summary>
    public void Launch()
    {
        var player = GameManager.Instance.player;

        // �e���\�����Ă���ʒu�E���������Z�b�g����
        Hide();

        moveDir = player.transform.forward;
        transform.position = player.bulletSpawnPoint.position;

        // ���Z�b�g������\���ɂ���
        Show();
    }

    /// <summary> �e���̋��� </summary>
    public virtual void Travel()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    /// <summary> �e���\������ </summary>
    protected void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary> �e��\������ </summary>
    protected void Show()
    {
        gameObject.SetActive(true);
    }
}
