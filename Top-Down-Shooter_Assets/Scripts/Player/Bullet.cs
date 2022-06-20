using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb; // Компонент Rigidbody2D 
    private bool isHit; // Было ли попадание по врагу
    private Enemy enemy; // Враги
    private Boss boss;
    private Minion minion;
    private Player player; // Игрок


    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * player.bullet_speed; // Заставляем двигаться
        isHit = false; 
    }

    private void Update()
    {
        if (isHit){ // Если произошло попадание (обрабатывается здесь, поскольку был такой баг, что сразу несколько врагов получали урон)
            Destroy(gameObject); 
            int rand = Random.Range(1, 101); 
            if (rand <= player.crit_chance) enemy.takeDamage(player.damage * player.crit_damage); 
            else enemy.takeDamage(player.damage); 
        }
    }

    private void OnTriggerEnter2D(Collider2D hit_info) // Обработка попаданий в другие GameObject
    {
        if (hit_info != null) 
        {
            if (hit_info.CompareTag("Enemy") && !hit_info.isTrigger) 
            {
                isHit = true; // Регистрируем попадание
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
