using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private float timer_attack_rate; // ������ ������� ����� �������
    private HealthBar health_bar; // ����� ��������
    private Player player; // �����
    public float attack_rate_melee; // ����� ����� ������� � ������� ���
    public float super_attack_rate_melee; // ������� ������� ���� � ������� ��� (�� 2 ����)
    public float minion_spawn_rate; // ����� ����� ��������� ��������
    public float extension_rate; // ����� ����� ����������� �����
    public float extention_percent; // ������� ���������� �����
    private float timer_minion_spawn_rate; // ������ ������� ����� ��������� ��������
    private float changes_attack_rate_melee; // ���������� ����� ����� ������� � ������� ���
    public float attack_rate_distant; // ����� ����� ������� � ������� ���
    public float time_in_condition; // ����� � ��������� (���� �����)
    private float timer_condition; // ������ ������� � ���������
    public float health; // ��������
    public float damage; // ����
    public float cost; // ����� �����, ���������� ��� ��������
    private int next_condition; // ��������� ���������
    private Vector3 start_position; // ��������� ������� �����
    public GameObject bullet; // ����
    public Transform[] shot_points; // ����� ������ ����
    public Minion minion; // ������
    private Vector3 room_position; // ������� �������
    private Vector3 start_scale; // ��������� ������ �����

    private void Start()
    {
        player = FindObjectOfType<Player>();
        health_bar = GetComponent<HealthBar>();
        health_bar.full_health = health;
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
        if (health / health_bar.full_health > 0.5) // ���� �������� ������ 50% - 1 ����
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
        else if (health / health_bar.full_health <= 0.5) // 2 ����
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

    private void minionAndAoe(int count_minions, bool aoe) // ������ ���������, ���� �������� true ������ ���������� ���������� �� ������ ����
    {
        if (timer_minion_spawn_rate <= 0) 
        {
            for (int i = 1; i <= count_minions; i++)
            {
                Instantiate(minion, generateMinionPosition(), transform.rotation); 
            }
            timer_minion_spawn_rate = minion_spawn_rate; 
        }
        else timer_minion_spawn_rate -= Time.deltaTime; 

        if (aoe)
        {
            if (timer_attack_rate <= 0) 
            {
                if (transform.localScale.x < 2.5)
                {
                    transform.localScale = new Vector3(transform.localScale.x + extention_percent, transform.localScale.y + extention_percent, 1f);
                }
                timer_attack_rate = extension_rate; 
            }
            else timer_attack_rate -= Time.deltaTime; 
        }
    }
    private void distant(bool rotate) // ������ ���������, ���� �������� true ���������� �� ������ ����
    {
        if (!rotate)
            if (timer_attack_rate <= 0) 
            {
                foreach (Transform shot_point in shot_points) 
                {
                    Instantiate(bullet, shot_point.position, shot_point.rotation);
                }
                timer_attack_rate = attack_rate_distant; 
            }
            else timer_attack_rate -= Time.deltaTime;
        else
        {
            if (timer_attack_rate <= 0) 
            {
                foreach (Transform shot_point in shot_points) 
                {
                    Instantiate(bullet, shot_point.position, shot_point.rotation); 
                    shot_point.rotation *= Quaternion.Euler(0f, 0f, 10f);
                }
                timer_attack_rate = attack_rate_distant; 
            }
            else timer_attack_rate -= Time.deltaTime; 
        }
    }
    private void melee(bool extension) // ������ ���������, ���� �������� true ���������� �� ������ ����
    {
        if (extension) 
        {
            if (timer_attack_rate <= 0) 
            {
                transform.position = player.transform.position + generateIncrement(); 
                player.�hangeHealth(-damage); 
                timer_attack_rate = changes_attack_rate_melee; 
                if (changes_attack_rate_melee > super_attack_rate_melee)
                {
                    changes_attack_rate_melee -= 0.1f; 
                }
            }
            else 
            {
                timer_attack_rate -= Time.deltaTime; 
            }
        }
        else { 
            if (timer_attack_rate <= 0) 
            {
                transform.position = player.transform.position + generateIncrement(); 
                player.�hangeHealth(-damage); 
                timer_attack_rate = attack_rate_melee; 
            }
            else 
            {
                timer_attack_rate -= Time.deltaTime; 
            }
        }
    }
    private Vector3 generateIncrement() // ��������� ��������� ����� �� ����������, ����� ������� ��������� � ����� (0,0)
    {
        Vector3 tp_position;
        double grad, x, y;
        int rand;
        bool can_left, can_right, can_down, can_up;
        Vector3 difference_player_position_and_center;

        can_left = true;
        can_right = true;
        can_up = true;
        can_down = true;

        difference_player_position_and_center = 
            player.transform.position - room_position;
        if (difference_player_position_and_center.x < -6) 
            can_left = false;
        if (difference_player_position_and_center.x > 6) 
            can_right = false;
        if (difference_player_position_and_center.y > 2) 
            can_up = false;
        if (difference_player_position_and_center.y < -2) 
            can_down = false;

        if (!can_left && !can_up)
            grad = UnityEngine.Random.Range(300, 330);
        else if (!can_left && !can_down)
            grad = UnityEngine.Random.Range(30, 60);
        else if (!can_right && !can_up)
            grad = UnityEngine.Random.Range(210, 240);
        else if (!can_right && !can_down)
            grad = UnityEngine.Random.Range(120, 150);
        else if (!can_right)
            grad = UnityEngine.Random.Range(120, 240);
        else if (!can_left)
        {
            rand = UnityEngine.Random.Range(0, 2);
            if (rand == 1) 
                grad = UnityEngine.Random.Range(0, 60);
            else 
                grad = UnityEngine.Random.Range(300, 360);
        }
        else if (!can_up)
            grad = UnityEngine.Random.Range(210, 330);
        else if (!can_down)
            grad = UnityEngine.Random.Range(30, 150);
        else
            grad = UnityEngine.Random.Range(0, 360);

        grad /= 57.2958; 
        x = Math.Cos(grad);
        y = Math.Sin(grad); 
        tp_position.x = Convert.ToSingle(x) * 1.7f;
        tp_position.y = Convert.ToSingle(y) * 1.7f;
        tp_position.z = 0f;
        return tp_position;
    }
    private void generateCondition() // ��������� ���������
    {
        if (timer_condition <= 0) 
        {
            timer_condition = time_in_condition; 
            next_condition = UnityEngine.Random.Range(1, 4);
            changes_attack_rate_melee = attack_rate_melee; 
            transform.position = start_position; 
            transform.localScale = start_scale;
        }
        else
        {
            timer_condition -= Time.deltaTime;
        }
    }
    public void takeDamage(float damage) // ��������� �����
    {
        health -= damage; 
        health_bar.fill = health / health_bar.full_health; 
        if (health <= 0) 
        {
            Destroy(gameObject); 
            player.changeMoney(cost); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // ��������� ��������� �������
    {
        if (collision.CompareTag("Room"))
        {
            room_position = collision.transform.position;
        }
    }
    private void OnTriggerStay2D(Collider2D collision) // ��������� ����� ������ ��� ����������� � 3 ���������
    {
        if (collision.CompareTag("Player") && next_condition == 3) 
        {
            player.�hangeHealth(-damage * 0.1f); 
            GetComponent<Rigidbody2D>().WakeUp();
        }
    }
    private Vector3 generateMinionPosition() // ��������� ������� �������
    {
        Vector3 minion_position;
        minion_position.x = UnityEngine.Random.Range(-8, 9);
        minion_position.y = UnityEngine.Random.Range(-4, 5);
        minion_position.z = 0;

        minion_position += room_position;

        return minion_position;
    }
}


