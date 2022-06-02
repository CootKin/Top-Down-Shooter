using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private float timer_firerate; // Отсчет времени между выстрелами
    public GameObject bullet; // Пуля
    public Transform shot_point; // Точка спавна пуль
    public float firerate; // Время между выстрелами
    private Player player; // Игрок

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        rotate(); // Обрабатываем поворот пушки
        shot(); // Обрабатываем стрельбу
    }

    private void shot() // Выполнение выстрела
    {
        if (timer_firerate <= 0) // Если отсчет закончен, можно стрелять
        {
            Instantiate(bullet, shot_point.position, transform.rotation); // Спавним пулю в позицию shot_point и с поворотом пушки
            timer_firerate = firerate; // Начинаем новый отсчет
        }
        else timer_firerate -= Time.deltaTime; // Если отсчет не закончен продолжаем его
    }

    private void rotate() // Поворот за игроком
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // Разница в виде угла
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90); // Поворачиваем пушку на величину этого угла + корректировочный угол (опционально)
    }
}
