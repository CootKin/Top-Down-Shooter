using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellow : Enemy
{
    private new void Start()
    {
        base.Start(); 
        timer_attack_rate = attack_rate; // Имитация замаха, т.е. враг ждет какое-то время, прежде чем ударить игрока
    }

    public void OnTriggerStay2D(Collider2D hit_info) // Логика атаки
    {
        if (hit_info.CompareTag("Player"))
        {
            if (timer_attack_rate <= 0) 
            {
                hit_info.GetComponent<Player>().сhangeHealth(-damage); 
                Destroy(gameObject); 
            }
            else 
            {
                rb.WakeUp(); // "Будим" объект, поскольку метод не работает с неподвижными объектами
                timer_attack_rate -= Time.deltaTime; 
            }
        }
    }
}
