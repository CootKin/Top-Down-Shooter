using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private Player player; // ������ ������
    public BulletEnemy bullet;
    public Transform shot_point;
    public float health; // �������� �����
    public float firerate; // ����� ����� �������
    private float timer_firerate;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Vector3 difference = player.transform.position - transform.position;
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90; // ��������� ����, �� ������� ��������� �����
        angle = Random.Range(angle - 45, angle + 45);
        shot_point.rotation = Quaternion.Euler(0f, 0f, angle);
        shot();
    }

    private void shot() // ���������� ��������
    {
        if (timer_firerate <= 0) // ���� ������ ��������, ����� ��������
        {
            Instantiate(bullet, shot_point.position, shot_point.rotation); // ������� ���� � ������� shot_point � � ��������� �����
            timer_firerate = firerate; // �������� ����� ������
        }
        else timer_firerate -= Time.deltaTime; // ���� ������ �� �������� ���������� ���
    }

    public void takeDamage(float damage) // ��������� �����
    {
        health -= damage; // �������� �� �������� ���������� ����
        if (health <= 0) // ���� �������� ���������� �� ����
        {
            Destroy(gameObject); // ���������� �����
        }
    }
}
