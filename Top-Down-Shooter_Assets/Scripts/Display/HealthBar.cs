using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float fill;
    public float full_health;

    void Start()
    {
        fill = 1f; // ���������� ����� ������
    }

    void Update()
    {
        bar.fillAmount = fill; // ������ ���� ������ ������������� ����� � ������������ � ���������� fill
    }

    public void flip() // ������ ����� (� ������ ����� �������������� ������ � ����, ������� ����� ��� ��� ��������� ������ �����)
    {
        if (bar.fillOrigin == 0) bar.fillOrigin = 1;
        else bar.fillOrigin = 0;
    }
}
