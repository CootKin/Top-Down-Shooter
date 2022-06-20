using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Player player; // Игрок
    public Text damage_cost; // Цены на улучшения
    public Text max_health_cost;
    public Text heal_cost;
    public Text firerate_cost;
    public Text count_bullets_cost;
    public HealthBar health_bar; // Шкала здоровья

    public void upDamage() // Увеличение урона на 10%
    {
        float cost;
        cost = Convert.ToSingle(damage_cost.text);
        if (player.money >= cost)
        {
            player.damage *= 1.1f; 
            player.changeMoney(-cost); 
            cost *= 1.2f; // Цена возрастает на 20%
            cost = Convert.ToSingle(Math.Round(cost, 2));
            damage_cost.text = Convert.ToString(cost); 
        }
    }
    public void upFirerate() // Увеличение скорострельности на 10%
    {
        float cost;
        cost = Convert.ToSingle(firerate_cost.text);
        if (player.money >= cost && player.firerate > 0.05)
        {
            player.firerate *= 0.9f; 
            player.changeMoney(-cost); 
            cost *= 1.2f; // Цена возрастает на 20%
            cost = Convert.ToSingle(Math.Round(cost, 2));
            firerate_cost.text = Convert.ToString(cost); 
        }
    }
    public void upCountBullets() // Увеличение числа пуль
    {
        float cost;
        cost = Convert.ToSingle(count_bullets_cost.text);
        if (player.money >= cost)
        {
            player.count_bullets += 1;
            player.changeMoney(-cost); 
            cost *= 2f; // Цена возрастает в 2 раза
            cost = Convert.ToSingle(Math.Round(cost, 2));
            count_bullets_cost.text = Convert.ToString(cost); 
        }
    }
    public void upMaxHealth() // Увеличение макс.запаса здоровья на 10%
    {
        float cost;
        cost = Convert.ToSingle(max_health_cost.text);
        if (player.money >= cost)
        {
            float health_difference = health_bar.full_health * 1.1f - health_bar.full_health;
            health_bar.full_health *= 1.1f; 
            player.сhangeHealth(health_difference); 
            player.changeMoney(-cost); 
            cost *= 1.2f; // Цена возрастает на 20%
            cost = Convert.ToSingle(Math.Round(cost, 2));
            max_health_cost.text = Convert.ToString(cost); 
        }
    }

    public void heal() // Восполнение здоровья
    {
        float cost;
        cost = Convert.ToSingle(heal_cost.text);
        if (player.money >= cost && player.health < health_bar.full_health) // Если у игрока хватает денег и есть что восполнять
        {
            player.сhangeHealth(health_bar.full_health * 0.05f); // Добавляем к текущему здоровью 5% от максимального
            if (player.health > health_bar.full_health) player.health = health_bar.full_health; // Если здоровье становится больше, чем максимальное, присваиваем ему значение максимального
            player.changeMoney(-cost); 
        }
    }
}
