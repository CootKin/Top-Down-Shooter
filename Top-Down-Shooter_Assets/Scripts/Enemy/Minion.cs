using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Player player; // Игрок
    public BulletEnemy bullet; // Снаряд
    public Transform shot_point; // Точка спавна
    public float health; // Здоровье врага
    public float firerate; // Время между атаками
    private float timer_firerate; // Отсчет времени между атаками

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90; // Вычисляем угол, на котором находится игрок
        angle = Random.Range(angle - 45, angle + 45); // Генерируем погрешность
        shot_point.rotation = Quaternion.Euler(0f, 0f, angle);
        shot();
    }

    private void shot() // Выполнение выстрела
    {
        if (timer_firerate <= 0) 
        {
            Instantiate(bullet, shot_point.position, shot_point.rotation); 
            timer_firerate = firerate; 
        }
        else timer_firerate -= Time.deltaTime; 
    }

    public void takeDamage(float damage) // Получение урона
    {
        health -= damage; 
        if (health <= 0) 
        {
            Destroy(gameObject); 
        }
    }
}
