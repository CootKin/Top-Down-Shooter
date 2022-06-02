using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // Компонент Rigidbody
    private Vector2 move_vector; // Направление движения
    private Vector2 move_velocity; // Скорость в направлении move_vector
    private HealthBar health_bar; // Шкала здоровья
    private MoneyCount money_count; // Отображение количества монет
    private bool facing_right = true; // Повернут ли игрок вправо
    public float money; // Количество денег
    private float timer_firerate; // Отсчет времени между выстрелами
    public GameObject bullet; // Пуля
    public Transform shot_point; // Точка спавна пуль
    public int count_bullets;
    
    // Прокачиваемые характеристики
    public float health; // Здоровье игрока
    public float speed; // Скорость
    public float damage; // Урон
    public float crit_chance; // Шанс крита в % 
    public float crit_damage; // Множитель урона при крите
    public float bullet_speed; // Скорость пули
    public float firerate; // Время между выстрелами
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health_bar = GetComponent<HealthBar>();
        money_count = GetComponent<MoneyCount>();
        health_bar.full_health = health; // Полное здоровье равно здоровью в начале игры
        money = 0; // Изначально монет 0
    }

    void Update()
    {
        move(); // Обрабатываем движение
        checkFlip(); // Обрабатываем повороты
        rotate(); // Обрабатываем поворот за курсором
        shot(count_bullets); // Обрабатываем стрельбу
    }

    private void shot(int count_bullets) // Выполнение выстрела
    {
        if (timer_firerate <= 0) // Если отсчет закончен, можно стрелять
        {
            if (Input.GetMouseButton(0)) // Если нажата левая кнопка мыши
            {
                if (count_bullets == 1)
                    Instantiate(bullet, shot_point.position, shot_point.rotation); // Спавним пулю в позицию shot_point и с поворотом пушки
                if (count_bullets > 1 && count_bullets % 2 == 1)
                {
                    Instantiate(bullet, shot_point.position, shot_point.rotation);
                    for (int i = 1; i <= count_bullets / 2; i++)
                    {
                        Vector3 increment = new Vector3(i * 0.4f, 0);
                        Instantiate(bullet, shot_point.position + increment, shot_point.rotation);
                    }
                    for (int i = 1; i <= count_bullets / 2; i++)
                    {
                        Vector3 increment = new Vector3(-i * 0.4f, 0);
                        Instantiate(bullet, shot_point.position + increment, shot_point.rotation);
                    }
                }
                if (count_bullets > 1 && count_bullets % 2 == 0)
                {
                    for (int i = 1; i <= count_bullets / 2; i++)
                    {
                        Vector3 increment = new Vector3(i * 0.4f, 0);
                        Instantiate(bullet, shot_point.position + increment, shot_point.rotation);
                    }
                    for (int i = 1; i <= count_bullets / 2; i++)
                    {
                        Vector3 increment = new Vector3(-i * 0.4f, 0);
                        Instantiate(bullet, shot_point.position + increment, shot_point.rotation);
                    }
                }
                timer_firerate = firerate; // Начинаем новый отсчет
            }
        }
        else timer_firerate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
    }

    private void rotate() // Поворот за курсором
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shot_point.position; // Разница между положением мыши и пушки в прострастве экрана
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Разница в виде угла
        shot_point.rotation = Quaternion.Euler(0f, 0f, rotZ - 90); // Поворачиваем пушку на величину этого угла + корректировочный угол (опционально)
    }

    private void checkFlip() // Проверка и исполнение поворота
    {
        if (!facing_right && move_vector.x > 0) flip(); // Если смотрим влево и движемся вправо, разворачиваемся
        else if (facing_right && move_vector.x < 0) flip(); // Если смотрим вправо и движемся влево, разворачиваемся
    }

    private void flip() // Разворачивание персонажа
    {
        facing_right = !facing_right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

private void move() 
{
    move_vector = new Vector2(Input.GetAxis("Horizontal"), 
        Input.GetAxis("Vertical")); 
    if (move_vector.x == 0 && move_vector.y == 0)
        anim.SetBool("isRunning", false);
    else
        anim.SetBool("isRunning", true);
    move_velocity = new Vector2(move_vector.x * speed, move_vector.y * speed); 
    rb.velocity = move_velocity;
}

    public void сhangeHealth(float health_value) // Изменение здоровья персонажа
    {
        health += health_value; // Увеличиваем здоровье на полученную величину
        health_bar.fill = health / health_bar.full_health; // Отображаем новое здоровье на шкале
        if (health <= 0) SceneManager.LoadScene("Menu"); // Если игрок умирает, загружаем меню
    }

    public void changeMoney(float money_value) // Изменение числа монет у персонажа
    {
        money += money_value; // Увеличиваем монеты на полученную величину
        money = Convert.ToSingle(Math.Round(money, 2));
        money_count.displayMoney(money); // Отображаем новое количество монет
    }
}
