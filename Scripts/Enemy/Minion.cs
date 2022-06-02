using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Player player; // Объект игрока
    public BulletEnemy bullet;
    public Transform shot_point;
    public float health; // Здоровье врага
    public float firerate; // Время между атаками
    private float timer_firerate;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90; // Вычисляем угол, на котором находится игрок
        angle = Random.Range(angle - 45, angle + 45);
        shot_point.rotation = Quaternion.Euler(0f, 0f, angle);
        shot();
    }

    private void shot() // Выполнение выстрела
    {
        if (timer_firerate <= 0) // Если отсчет закончен, можно стрелять
        {
            Instantiate(bullet, shot_point.position, shot_point.rotation); // Спавним пулю в позицию shot_point и с поворотом пушки
            timer_firerate = firerate; // Начинаем новый отсчет
        }
        else timer_firerate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
    }

    public void takeDamage(float damage) // Получение урона
    {
        health -= damage; // Вычитаем из здоровья полученный урон
        if (health <= 0) // Если здоровье опустилось до нуля
        {
            Destroy(gameObject); // Уничтожаем врага
        }
    }
}
