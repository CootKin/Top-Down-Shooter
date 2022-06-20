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
        fill = 1f; // »значально шкала полна€
    }

    void Update()
    {
        bar.fillAmount = fill; //  аждый кадр мен€ем заполненность шкалы в соответствии с переменной fill
    }

    public void flip() // –еверс шкалы (у врагов шкала поворачиваетс€ вместе с ними, поэтому нужно еще раз повернуть только шкалу)
    {
        if (bar.fillOrigin == 0) bar.fillOrigin = 1;
        else bar.fillOrigin = 0;
    }
}
