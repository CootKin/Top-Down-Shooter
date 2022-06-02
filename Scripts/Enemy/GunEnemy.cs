using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEnemy : MonoBehaviour
{
    private float timer_firerate; // ������ ������� ����� ����������
    public GameObject bullet; // ����
    public Transform shot_point; // ����� ������ ����
    public float firerate; // ����� ����� ����������
    private Player player; // �����

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        rotate(); // ������������ ������� �����
        shot(); // ������������ ��������
    }

    private void shot() // ���������� ��������
    {
        if (timer_firerate <= 0) // ���� ������ ��������, ����� ��������
        {
            Instantiate(bullet, shot_point.position, transform.rotation); // ������� ���� � ������� shot_point � � ��������� �����
            timer_firerate = firerate; // �������� ����� ������
        }
        else timer_firerate -= Time.deltaTime; // ���� ������ �� �������� ���������� ���
    }

    private void rotate() // ������� �� �������
    {
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // ������� � ���� ����
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90); // ������������ ����� �� �������� ����� ���� + ���������������� ���� (�����������)
    }
}
