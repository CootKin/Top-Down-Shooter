using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb; // Физика пули
    private bool isHit; // Было ли попадание по врагу
    private Enemy enemy; // Враг
    private Boss boss;
    private Minion minion;
    private Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * player.bullet_speed; // Пуля движется прямо
        isHit = false; // При появлении пули урон попадания еще не было
    }

    private void Update()
    {
        if (isHit){ // Если произошло попадание (обрабатывается здесь, поскольку был баг из-за того, что сразу несколько врагов получали урон)
            Destroy(gameObject); // Уничтожаем пулю
            int rand = Random.Range(1, 101); // Генерируем случайное число от 1 до 100
            if (rand <= player.crit_chance) enemy.takeDamage(player.damage * player.crit_damage); // Если был крит, наносим критический урон
            else enemy.takeDamage(player.damage); // Если крита не было, наносим обычный урон
        }
    }

    private void OnTriggerEnter2D(Collider2D hit_info)
    {
        if (hit_info != null) // Если попали во что-то
        {
            if (hit_info.CompareTag("Enemy") && !hit_info.isTrigger) // Если в триггер пули попал враг и это не триггер
            {
                isHit = true; // Регистрируем попадание
                enemy = hit_info.GetComponent<Enemy>(); // Получаем этого врага
            }
            else if (hit_info.CompareTag("Boss") && !hit_info.isTrigger)
            {
                boss = hit_info.GetComponent<Boss>(); // Получаем этого врага
                Destroy(gameObject); // Уничтожаем пулю
                int rand = Random.Range(1, 101); // Генерируем случайное число от 1 до 100
                if (rand <= player.crit_chance) boss.takeDamage(player.damage * player.crit_damage);
                else boss.takeDamage(player.damage);
            }
            else if (hit_info.CompareTag("Minion") && !hit_info.isTrigger)
            {
                minion = hit_info.GetComponent<Minion>(); // Получаем этого врага
                Destroy(gameObject); // Уничтожаем пулю
                int rand = Random.Range(1, 101); // Генерируем случайное число от 1 до 100
                if (rand <= player.crit_chance) minion.takeDamage(player.damage * player.crit_damage);
                else minion.takeDamage(player.damage);
            }
            else if (hit_info.isTrigger) // Если в триггер пули попал триггер
            {
                return; // Выходим из функции
            }
            else if (!(hit_info.CompareTag("Player"))) // Если попали во что-то, но не во врага и не в игрока
            {
                Destroy(gameObject); // Уничтожаем пулю
            }
        }
    }
}
