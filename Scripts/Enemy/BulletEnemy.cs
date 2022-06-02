using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb; // Физика пули
    public float speed; // Скорость пули
    public float damage; // Урон который пуля наносит
    private Player player; // Игрок
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed; // Пуля движется в направлении игрока
    }

    private void OnTriggerEnter2D(Collider2D hit_info)
    {
        if (hit_info != null) // Если попали во что-то
        {
            if (hit_info.CompareTag("Player")) // Если в триггер пули попал игрок
            {
                Destroy(gameObject); // Уничтожаем пулю
                hit_info.GetComponent<Player>().сhangeHealth(-damage); // Наносим урон
            }
            else if (hit_info.isTrigger) // Если в триггер пули попал триггер
            {
                return; // Выходим из функции
            }
            else if (!(hit_info.CompareTag("Enemy")) && !(hit_info.CompareTag("Bullet")) && !(hit_info.CompareTag("Boss")) && !(hit_info.CompareTag("Minion"))) // Если попали во что-то, но не во врага и не в игрока
            {
                Destroy(gameObject); // Уничтожаем пулю
            }
        }
    }
}
