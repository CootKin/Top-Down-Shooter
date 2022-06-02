using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellow : Enemy
{
    private new void Start()
    {
        base.Start(); // ��������� start ������������� ������
        timer_attack_rate = attack_rate; // �������� ������, �.�. ���� ���� �����-�� �����, ������ ��� ������� ������
    }

    public void OnTriggerStay2D(Collider2D hit_info)
    {
        if (hit_info.CompareTag("Player")) // ���� ��� �����
        {
            if (timer_attack_rate <= 0) // ���� ����� � �����
            {
                hit_info.GetComponent<Player>().�hangeHealth(-damage); // �������
                Destroy(gameObject); // ������������
            }
            else // ���� �� ����� � �����
            {
                rb.WakeUp(); // "�����" ������, ��������� ������� �� �������� � ������������ ���������
                timer_attack_rate -= Time.deltaTime; // ��������� ������ �� �����
            }
        }
    }
}
