using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb; // ��������� Rigidbody2D 
    private bool isHit; // ���� �� ��������� �� �����
    private Enemy enemy; // �����
    private Boss boss;
    private Minion minion;
    private Player player; // �����


    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * player.bullet_speed; // ���������� ���������
        isHit = false; 
    }

    private void Update()
    {
        if (isHit){ // ���� ��������� ��������� (�������������� �����, ��������� ��� ����� ���, ��� ����� ��������� ������ �������� ����)
            Destroy(gameObject); 
            int rand = Random.Range(1, 101); 
            if (rand <= player.crit_chance) enemy.takeDamage(player.damage * player.crit_damage); 
            else enemy.takeDamage(player.damage); 
        }
    }

    private void OnTriggerEnter2D(Collider2D hit_info) // ��������� ��������� � ������ GameObject
    {
        if (hit_info != null) 
        {
            if (hit_info.CompareTag("Enemy") && !hit_info.isTrigger) 
            {
                isHit = true; // ������������ ���������
                enemy = hit_info.GetComponent<Enemy>(); 
            }
            else if (hit_info.CompareTag("Boss") && !hit_info.isTrigger)
            {
                boss = hit_info.GetComponent<Boss>();
                Destroy(gameObject); 
                int rand = Random.Range(1, 101); 
                if (rand <= player.crit_chance) boss.takeDamage(player.damage * player.crit_damage);
                else boss.takeDamage(player.damage);
            }
            else if (hit_info.CompareTag("Minion") && !hit_info.isTrigger)
            {
                minion = hit_info.GetComponent<Minion>(); 
                Destroy(gameObject);
                int rand = Random.Range(1, 101);
                if (rand <= player.crit_chance) minion.takeDamage(player.damage * player.crit_damage);
                else minion.takeDamage(player.damage);
            }
            else if (hit_info.isTrigger) 
            {
                return;
            }
            else if (!(hit_info.CompareTag("Player"))) 
            {
                Destroy(gameObject); 
            }
        }
    }
}
