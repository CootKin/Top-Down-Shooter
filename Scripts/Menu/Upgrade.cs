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
    public HealthBar health_bar; // ����� ��������
    public void upDamage() // ���������� ����� �� 10%
    {
        float cost;
        cost = Convert.ToSingle(damage_cost.text);
        if (player.money >= cost)
        {
            player.damage *= 1.1f; // ���� ������ ������������� �� 10%
            player.changeMoney(-cost); // ������ ����������� �� ������ ����
            cost *= 1.2f; // ���� ���������� �� 20%
            damage_cost.text = Convert.ToString(cost); // ������������ ����� ����
        }
    }
    public void upFirerate() // ���������� ���������������� �� 10%
    {
        float cost;
        cost = Convert.ToSingle(firerate_cost.text);
        if (player.money >= cost && player.firerate > 0.05)
        {
            player.firerate *= 0.9f; // ���������������� ������������� �� 10%
            player.changeMoney(-cost); // ������ ����������� �� ������ ����
            cost *= 1.2f; // ���� ���������� �� 20%
            firerate_cost.text = Convert.ToString(cost); // ������������ ����� ����
        }
    }
    public void upCountBullets() // ���������� ����� ����
    {
        float cost;
        cost = Convert.ToSingle(count_bullets_cost.text);
        if (player.money >= cost)
        {
            player.count_bullets += 1;
            player.changeMoney(-cost); // ������ ����������� �� ������ ����
            cost *= 2f; // ���� ���������� � 2 ����
            count_bullets_cost.text = Convert.ToString(cost); // ������������ ����� ����
        }
    }
    public void upMaxHealth() // ���������� ����.������ �������� �� 10%
    {
        float cost;
        cost = Convert.ToSingle(max_health_cost.text);
        if (player.money >= cost)
        {
            float health_difference = health_bar.full_health * 1.1f - health_bar.full_health;
            health_bar.full_health *= 1.1f; // ����������� ����. ����� ��������
            player.�hangeHealth(health_difference); // ��������� � �������� �������� ������� ����� ������ � ����� ����. ������� ��������
            player.changeMoney(-cost); // ������ ����������� �� ������ ����
            cost *= 1.2f; // ���� ���������� �� 20%
            max_health_cost.text = Convert.ToString(cost); // ������������ ����� ����
        }
    }

    public void heal()
    {
        float cost;
        cost = Convert.ToSingle(heal_cost.text);
        if (player.money >= cost && player.health < health_bar.full_health)
        {
            player.�hangeHealth(health_bar.full_health * 0.05f); // ��������� � �������� �������� 5% �� �������������
            if (player.health > health_bar.full_health) player.health = health_bar.full_health; // ���� �������� ���������� ������, ��� ������������, ����������� ��� �������� �������������
            player.changeMoney(-cost); // ������ ����������� �� ������ ����
        }
    }
}
