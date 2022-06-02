using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Player player;
    public Text damage_cost;
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
            player.damage *= 1.1f; // Урон игрока увеличивается на 10%
            player.changeMoney(-cost); // Деньги уменьшаются на размер цены
            cost *= 1.2f; // Цена возрастает на 20%
            damage_cost.text = Convert.ToString(cost); // Отображается новая цена
        }
    }
    public void upFirerate() // Увеличение скорострельности на 10%
    {
        float cost;
        cost = Convert.ToSingle(firerate_cost.text);
        if (player.money >= cost && player.firerate > 0.05)
        {
            player.firerate *= 0.9f; // Скорострельность увеличивается на 10%
            player.changeMoney(-cost); // Деньги уменьшаются на размер цены
            cost *= 1.2f; // Цена возрастает на 20%
            firerate_cost.text = Convert.ToString(cost); // Отображается новая цена
        }
    }
    public void upCountBullets() // Увеличение числа пуль
    {
        float cost;
        cost = Convert.ToSingle(count_bullets_cost.text);
        if (player.money >= cost)
        {
            player.count_bullets += 1;
            player.changeMoney(-cost); // Деньги уменьшаются на размер цены
            cost *= 2f; // Цена возрастает в 2 раза
            count_bullets_cost.text = Convert.ToString(cost); // Отображается новая цена
        }
    }
    public void upMaxHealth() // Увеличение макс.запаса здоровья на 10%
    {
        float cost;
        cost = Convert.ToSingle(max_health_cost.text);
        if (player.money >= cost)
        {
            float health_difference = health_bar.full_health * 1.1f - health_bar.full_health;
            health_bar.full_health *= 1.1f; // Увеличиваем макс. запас здоровья
            player.сhangeHealth(health_difference); // Добавляем к текущему здоровью разницу между старым и новым макс. запасом здоровья
            player.changeMoney(-cost); // Деньги уменьшаются на размер цены
            cost *= 1.2f; // Цена возрастает на 20%
            max_health_cost.text = Convert.ToString(cost); // Отображается новая цена
        }
    }

    public void heal()
    {
        float cost;
        cost = Convert.ToSingle(heal_cost.text);
        if (player.money >= cost && player.health < health_bar.full_health)
        {
            player.сhangeHealth(health_bar.full_health * 0.05f); // Добавляем к текущему здоровью 5% от максимального
            if (player.health > health_bar.full_health) player.health = health_bar.full_health; // Если здоровье становится больше, чем максимальное, присваиваем ему значение максимального
            player.changeMoney(-cost); // Деньги уменьшаются на размер цены
        }
    }
}
