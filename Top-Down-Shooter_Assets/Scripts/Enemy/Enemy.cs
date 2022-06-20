using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb; // ��������� Rigidbody
    protected bool facing_right = false; // �������� �� ���� ������
    protected Vector2 move_vector; // ����������� ��������
    protected Vector2 move_velocity; // �������� � ����������� move_vector
    protected Player player; // �����
    protected float current_speed; // ������� �������� ������������ �����
    protected float timer_stun; // ������ �������, �� ������� ���������� ����
    protected float timer_attack_rate; // ������ ������� ����� �������
    protected HealthBar health_bar; // ����� ��������
    public float stun; // �����, �� ������� ��������������� ���� ��� ��������� � ����
    public float normal_speed; // ���������� �������� ������������ �����
    public float stop_dist; // ��������� �� ������, �� ������� ��������������� ���� (����� ��� ���������� ������)
    public float retr_dist; // ��������� �� ������, �� ������� ���� ��������� (����� ��� ���������� ������)
    public float health; // �������� �����
    public float damage; // ����
    public float attack_rate; // ����� ����� �������
    public float cost; // ���������� �����, ���������� �� ������� �����

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        health_bar = GetComponent<HealthBar>();
        health_bar.full_health = health; // ������ �������� ����� �������� � ������ ����
    }

    void Update()
    {
        checkStun(); 
        move(); 
        checkFlip();
    }

    protected void checkStun() // �������� � ���������� �����
    {
        if (timer_stun <= 0) current_speed = normal_speed; 
        else 
        {
            current_speed = 0; 
            timer_stun -= Time.deltaTime; 
        }
    }

    public void takeDamage (float damage) // ��������� �����
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

    protected void checkFlip() // �������� � ���������� ��������
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

    protected virtual void flip() // �������������� �����
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        health_bar.flip(); // ����� �������������� ��� ���, ��� ����������� �����������
    }

    protected void move() // ������������ �����
    {
        if (Vector2.Distance(transform.position, player.transform.position) > stop_dist) // ���� ���������� �� ������ ������, ��� ��������� ���������
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, current_speed * Time.deltaTime); // ���� �������� � ������
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= stop_dist && Vector2.Distance(transform.position, player.transform.position) > retr_dist) // ���� ���������� �� ������ ������, ��� ��������� ��������� � ������ ��������� �����������
        {
            return; // ���� ����� �� �����
        }
        else if (Vector2.Distance(transform.position, player.transform.position) <= retr_dist) // ���� ���������� �� ������ ����� ��� ������, ��� ��������� ����������
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -current_speed * Time.deltaTime); // ���� ��������
        }
    }
}
