using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // ��������� Rigidbody
    private Vector2 move_vector; // ����������� ��������
    private Vector2 move_velocity; // �������� � ����������� move_vector
    private HealthBar health_bar; // ����� ��������
    private MoneyCount money_count; // ����������� ���������� �����
    private bool facing_right = true; // �������� �� ����� ������
    public float money; // ���������� �����
    private float timer_firerate; // ������ ������� ����� ����������
    public GameObject bullet; // ����
    public Transform shot_point; // ����� ������ ����
    public int count_bullets;
    
    // ������������� ��������������
    public float health; // �������� ������
    public float speed; // ��������
    public float damage; // ����
    public float crit_chance; // ���� ����� � % 
    public float crit_damage; // ��������� ����� ��� �����
    public float bullet_speed; // �������� ����
    public float firerate; // ����� ����� ����������
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        health_bar = GetComponent<HealthBar>();
        money_count = GetComponent<MoneyCount>();
        health_bar.full_health = health; // ������ �������� ����� �������� � ������ ����
        money = 0; // ���������� ����� 0
    }

    void Update()
    {
        move(); // ������������ ��������
        checkFlip(); // ������������ ��������
        rotate(); // ������������ ������� �� ��������
        shot(count_bullets); // ������������ ��������
    }

    private void shot(int count_bullets) // ���������� ��������
    {
        if (timer_firerate <= 0) // ���� ������ ��������, ����� ��������
        {
            if (Input.GetMouseButton(0)) // ���� ������ ����� ������ ����
            {
                if (count_bullets == 1)
                    Instantiate(bullet, shot_point.position, shot_point.rotation); // ������� ���� � ������� shot_point � � ��������� �����
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
                timer_firerate = firerate; // �������� ����� ������
            }
        }
        else timer_firerate -= Time.deltaTime; // ���� ������ �� �������� ���������� ���
    }

    private void rotate() // ������� �� ��������
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shot_point.position; // ������� ����� ���������� ���� � ����� � ����������� ������
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // ������� � ���� ����
        shot_point.rotation = Quaternion.Euler(0f, 0f, rotZ - 90); // ������������ ����� �� �������� ����� ���� + ���������������� ���� (�����������)
    }

    private void checkFlip() // �������� � ���������� ��������
    {
        if (!facing_right && move_vector.x > 0) flip(); // ���� ������� ����� � �������� ������, ���������������
        else if (facing_right && move_vector.x < 0) flip(); // ���� ������� ������ � �������� �����, ���������������
    }

    private void flip() // �������������� ���������
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

    public void �hangeHealth(float health_value) // ��������� �������� ���������
    {
        health += health_value; // ����������� �������� �� ���������� ��������
        health_bar.fill = health / health_bar.full_health; // ���������� ����� �������� �� �����
        if (health <= 0) SceneManager.LoadScene("Menu"); // ���� ����� �������, ��������� ����
    }

    public void changeMoney(float money_value) // ��������� ����� ����� � ���������
    {
        money += money_value; // ����������� ������ �� ���������� ��������
        money = Convert.ToSingle(Math.Round(money, 2));
        money_count.displayMoney(money); // ���������� ����� ���������� �����
    }
}
