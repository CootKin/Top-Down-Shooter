using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellow : Enemy
{
    private new void Start()
    {
        base.Start(); // Выполняем start родительского класса
        timer_attack_rate = attack_rate; // Имитация замаха, т.е. враг ждет какое-то время, прежде чем ударить игрока
    }

    public void OnTriggerStay2D(Collider2D hit_info)
    {
        if (hit_info.CompareTag("Player")) // Если это игрок
        {
            if (timer_attack_rate <= 0) // Если готов к атаке
            {
                hit_info.GetComponent<Player>().сhangeHealth(-damage); // Атакует
                Destroy(gameObject); // Уничтожается
            }
            else // Если не готов к атаке
            {
                rb.WakeUp(); // "Будим" объект, поскольку триггер не работает с неподвижными объектами
                timer_attack_rate -= Time.deltaTime; // Уменьшаем таймер до атаки
            }
        }
    }
}
