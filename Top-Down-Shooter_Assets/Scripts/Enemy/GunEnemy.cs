using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private float timer_firerate; // Отсчет времени между выстрелами
    public GameObject bullet; // Снарял
    public Transform shot_point; // Точка спавна снарядов
    public float firerate; // Время между выстрелами
    private Player player; // Игрок

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        rotate(); 
        shot(); 
    }

    private void shot() // Выполнение выстрела
    {
        if (timer_firerate <= 0) 
        {
            Instantiate(bullet, shot_point.position, transform.rotation); 
            timer_firerate = firerate; 
        }
        else timer_firerate -= Time.deltaTime; 
    }

    private void rotate() // Поворот за игроком
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90); 
    }
}
