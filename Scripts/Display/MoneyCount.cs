using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyCount : MonoBehaviour
{
    public Text[] money_displays; // Отображение

    public void displayMoney(float money) // Отображение указанного числа монет
    {
        foreach (Text display in money_displays)
        {
            display.text = System.Convert.ToString(money); // Отображаем указанное число монет
        }
    }
}
