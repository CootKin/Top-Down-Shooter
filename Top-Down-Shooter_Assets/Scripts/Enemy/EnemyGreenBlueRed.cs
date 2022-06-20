using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGreenBlueRed : Enemy 
{
    private Animator anim;
    private new void Start()
    {
        base.Start(); 
        anim = GetComponent<Animator>();
        timer_attack_rate = attack_rate; // �������� ������, �.�. ���� ���� �����-�� �����, ������ ��� ������� ������
    }

    public void OnTriggerStay2D(Collider2D hit_info) // ������ �����
    {
        if (hit_info.CompareTag("Player")) 
        {
            if (timer_attack_rate <= 0)
            {

                hit_info.GetComponent<Player>().�hangeHealth(-damage);
                timer_attack_rate = attack_rate;
            }
            else 
            {
                anim.SetBool("isAttack", true);
                rb.WakeUp(); // "�����" ������, ��������� ����� �� �������� � ������������ ���������
                timer_attack_rate -= Time.deltaTime; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D hit_info) // �������� �������
    {
        if (hit_info.CompareTag("Player"))
        {
            anim.SetBool("isAttack", false);
            timer_attack_rate = attack_rate; 
        }
    }
}


