using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb; // ��������� Rigidbody2D
    public float speed; // �������� �������
    public float damage; // ���� ������� ������ �������
    private Player player; // �����
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // ���������� ���������
    }

    private void OnTriggerEnter2D(Collider2D hit_info) // ��������� ��������� � ������ GameObject
    {
        if (hit_info != null) 
        {
            if (hit_info.CompareTag("Player")) 
            {
                Destroy(gameObject); 
                hit_info.GetComponent<Player>().�hangeHealth(-damage); 
            }
            else if (hit_info.isTrigger) 
            {
                return; 
            }
            else if (!(hit_info.CompareTag("Enemy")) && !(hit_info.CompareTag("Bullet")) && !(hit_info.CompareTag("Boss")) && !(hit_info.CompareTag("Minion"))) 
            {
                Destroy(gameObject); 
            }
        }
    }
}
