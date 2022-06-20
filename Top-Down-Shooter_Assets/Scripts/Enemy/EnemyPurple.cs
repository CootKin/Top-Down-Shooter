using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPurple : Enemy
{
    protected override void flip() // Переопределяем метод, чтобы враг не вертелся
    {
        return;
    }
}
