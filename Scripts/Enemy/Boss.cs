using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private float timer_attack_rate; // Отсчет времени между атаками
    private HealthBar health_bar; // Шкала здоровья
    private Player player; // Объект игрока
    public float attack_rate_melee; // Время между атаками в ближнем бою
    public float super_attack_rate_melee;
    public float minion_spawn_rate;
    public float extension_rate;
    public float extention_percent;
    private float timer_minion_spawn_rate;
    private float changes_attack_rate_melee;
    public float attack_rate_distant; // Время между атаками в дальнем бою
    public float time_in_condition; // Время в состоянии (типе атаки)
    private float timer_condition;
    public float health; // Здоровье
    public float damage; // Урон
    public float cost; // Число монет, получаемое при убийстве
    private int next_condition;
    private Vector3 start_position;
    public GameObject bullet; // Пуля
    public Transform[] shot_points; // Точки спавна пуль
    public Minion minion;
    private Vector3 room_position;
    private Vector3 start_scale;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        health_bar = GetComponent<HealthBar>();
        health_bar.full_health = health; // Полное здоровье равно здоровью в начале игры
        timer_condition = 0;
        changes_attack_rate_melee = attack_rate_melee;
        start_position = transform.position;
        timer_attack_rate = 0.5f;
        timer_minion_spawn_rate = 0.1f;
        start_scale = transform.localScale;
    }
    private void Update()
    {
        generateCondition();
        if (health / health_bar.full_health > 0.5) // Если здоровье больше 50% - 1 фаза
        {
            switch (next_condition)
            {
                case 1:
                    distant(false);
                    break;
                case 2:
                    melee(false);
                    break;
                case 3:
                    minionAndAoe(3, false);
                    break;
            }
        }
        else if (health / health_bar.full_health <= 0.5) // Если здоровье 50% и меньше, и больше 20% - 2 фаза
        {
            switch (next_condition)
            {
                case 1:
                    distant(true);
                    break;
                case 2:
                    melee(true);
                    break;
                case 3:
                    minionAndAoe(7, true);
                    break;
            }
        }
    }

    private void minionAndAoe(int count_minions, bool aoe)
    {
        if (timer_minion_spawn_rate <= 0) // Если отсчет закончен, можно стрелять
        {
            for (int i = 1; i <= count_minions; i++)
            {
                Instantiate(minion, generateMinionPosition(), transform.rotation); // Спавним пулю в позицию shot_point и с его поворотом
            }
            timer_minion_spawn_rate = minion_spawn_rate; // Начинаем новый отсчет
        }
        else timer_minion_spawn_rate -= Time.deltaTime; // Если отсчет не закончен продолжаем его

        if (aoe)
        {
            if (timer_attack_rate <= 0) // Если отсчет закончен, можно стрелять
            {
                if (transform.localScale.x < 2.5)
                {
                    transform.localScale = new Vector3(transform.localScale.x + extention_percent, transform.localScale.y + extention_percent, 1f);
                }
                timer_attack_rate = extension_rate; // Начинаем новый отсчет
            }
            else timer_attack_rate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
        }
    }
    private void distant(bool rotate)
    {
        if (!rotate)
            if (timer_attack_rate <= 0) // Если отсчет закончен, можно стрелять
            {
                foreach (Transform shot_point in shot_points) // В каждой точке спавна спавним пулю
                {
                    Instantiate(bullet, shot_point.position, shot_point.rotation); // Спавним пулю в позицию shot_point и с его поворотом
                }
                timer_attack_rate = attack_rate_distant; // Начинаем новый отсчет
            }
            else timer_attack_rate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
        else
        {
            if (timer_attack_rate <= 0) // Если отсчет закончен, можно стрелять
            {
                foreach (Transform shot_point in shot_points) // В каждой точке спавна спавним пулю
                {
                    Instantiate(bullet, shot_point.position, shot_point.rotation); // Спавним пулю в позицию shot_point и с его поворотом
                    shot_point.rotation *= Quaternion.Euler(0f, 0f, 10f);
                }
                timer_attack_rate = attack_rate_distant; // Начинаем новый отсчет
            }
            else timer_attack_rate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
        }
    }
    private void melee(bool extension)
    {
        if (extension) // Если частота атак возрастает со временем
        {
            if (timer_attack_rate <= 0) // Если готов к атаке
            {
                transform.position = player.transform.position + generateIncrement(); // Телепортируем в случайную точку вокруг игрока
                player.сhangeHealth(-damage); // Атакуем
                timer_attack_rate = changes_attack_rate_melee; // Обнуляем таймер
                if (changes_attack_rate_melee > super_attack_rate_melee) // Если частота не достигла пиковой
                {
                    changes_attack_rate_melee -= 0.1f; // Увеличиваем частоту атак
                }
            }
            else // Если не готов к атаке
            {
                timer_attack_rate -= Time.deltaTime; // Уменьшаем таймер до атаки
            }
        }
        else { // Если частота атак постоянная
            if (timer_attack_rate <= 0) // Если готов к атаке
            {
                transform.position = player.transform.position + generateIncrement(); // Телепортируем в случайную точку вокруг игрока
                player.сhangeHealth(-damage); // Атакуем
                timer_attack_rate = attack_rate_melee; // Обнуляем таймер
            }
            else // Если не готов к атаке
            {
                timer_attack_rate -= Time.deltaTime; // Уменьшаем таймер до атаки
            }
        }
    }
    private Vector3 generateIncrement() // Генерирует случайную точку на круге 1/1
    {
        Vector3 tp_position;
        double grad, x, y;

        grad = UnityEngine.Random.Range(0, 361); // Генерируем угол


        x = Math.Cos(grad); // Вычисляем косинус
        y = Math.Sin(grad); // Вычисляем синус
        tp_position.x = Convert.ToSingle(x) * 1.5f;
        tp_position.y = Convert.ToSingle(y) * 1.5f;
        tp_position.z = 0f;
        return tp_position;
    }
    private void generateCondition()
    {
        if (timer_condition <= 0) // Если пришла пора менять состояние
        {
            timer_condition = time_in_condition; // Обнуляем таймер
            next_condition = UnityEngine.Random.Range(1, 4); // Генерируем следующее состояние
            changes_attack_rate_melee = attack_rate_melee; // Сбрасываем изменяемое время атаки
            transform.position = start_position; // Сбрасываем положение
            transform.localScale = start_scale; // Сбрасываем размер
        }
        else
        {
            timer_condition -= Time.deltaTime;
        }
    }
    public void takeDamage(float damage) // Получение урона
    {
        health -= damage; // Вычитаем из здоровья полученный урон
        health_bar.fill = health / health_bar.full_health; // Отображаем измененное здоровье на шкале
        if (health <= 0) // Если здоровье опустилось до нуля
        {
            Destroy(gameObject); // Уничтожаем босса
            player.changeMoney(cost); // Даем игроку за него монеты
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            room_position = collision.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && next_condition == 3) // Если попал игрок и сейчас состояние 3
        {
            player.сhangeHealth(-damage * 0.1f); // Атакуем
            GetComponent<Rigidbody2D>().WakeUp();
        }
    }
    private Vector3 generateMinionPosition()
    {
        Vector3 minion_position;
        minion_position.x = UnityEngine.Random.Range(-8, 9);
        minion_position.y = UnityEngine.Random.Range(-4, 5);
        minion_position.z = 0;

        minion_position += room_position;

        return minion_position;
    }
}


