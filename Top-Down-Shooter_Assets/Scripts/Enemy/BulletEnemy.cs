using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb; // Компонент Rigidbody2D
    public float speed; // Скорость снаряда
    public float damage; // Урон который снаряд наносит
    private Player player; // Игрок
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // Заставляем двигаться
    }

    private void OnTriggerEnter2D(Collider2D hit_info) // Обработка попаданий в другие GameObject
    {
        if (hit_info != null) 
        {
            if (hit_info.CompareTag("Player")) 
            {
                Destroy(gameObject); 
                hit_info.GetComponent<Player>().сhangeHealth(-damage); 
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
