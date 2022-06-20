using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb; // Компонент Rigidbody
    protected bool facing_right = false; // Повернут ли враг вправо
    protected Vector2 move_vector; // Направление движения
    protected Vector2 move_velocity; // Скорость в направлении move_vector
    protected Player player; // Игрок
    protected float current_speed; // Текущая скорость передвижения врага
    protected float timer_stun; // Отсчет времени, на которое остановлен враг
    protected float timer_attack_rate; // Отсчет времени между атаками
    protected HealthBar health_bar; // Шкала здоровья
    public float stun; // Время, на которое останавливается враг при попадании в него
    public float normal_speed; // Нормальная скорость передвижения врага
    public float stop_dist; // Дистанция до игрока, на которой останавливается враг (нужно для стреляющих врагов)
    public float retr_dist; // Дистанция до игрока, на которой враг отступает (нужно для стреляющих врагов)
    public float health; // Здоровье врага
    public float damage; // Урон
    public float attack_rate; // Время между атаками
    public float cost; // Количество монет, получаемое за данного врага

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        health_bar = GetComponent<HealthBar>();
        health_bar.full_health = health; // Полное здоровье равно здоровью в начале игры
    }

    void Update()
    {
        checkStun(); 
        move(); 
        checkFlip();
    }

    protected void checkStun() // Проверка и исполнение стана
    {
        if (timer_stun <= 0) current_speed = normal_speed; 
        else 
        {
            current_speed = 0; 
            timer_stun -= Time.deltaTime; 
        }
    }

    public void takeDamage (float damage) // Получение урона
    {
        timer_stun = stun; 
        health -= damage; 
        health_bar.fill = health / health_bar.full_health; 
        if (health <= 0) 
        {
            Destroy(gameObject); 
            player.changeMoney(cost); 
        }
    }

    protected void checkFlip() // Проверка и исполнение поворота
    {
        if (player.transform.position.x > transform.position.x && !facing_right) 
        {
            flip(); 
            facing_right = true;

        }
        else if (player.transform.position.x < transform.position.x && facing_right) 
        {
            flip(); 
            facing_right = false;
        }
    }

    protected virtual void flip() // Разворачивание врага
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        health_bar.flip(); // Шкала поворачивается еще раз, для корректного отображения
    }

    protected void move() // Передвижение врага
    {
        if (Vector2.Distance(transform.position, player.transform.position) > stop_dist) // Если расстояние до игрока больше, чем дистанция остановки
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, current_speed * Time.deltaTime); // Враг движется к игроку
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= stop_dist && Vector2.Distance(transform.position, player.transform.position) > retr_dist) // Если расстояние до игрока меньше, чем дистанция остановки и больше дистанции отступления
        {
            return; // Враг стоит на месте
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= retr_dist) // Если расстояние до игрока равна или меньше, чем дистанция отсупления
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -current_speed * Time.deltaTime); // Враг оступает
        }
    }
}
