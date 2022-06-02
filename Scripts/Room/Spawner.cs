using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemy_types;
    public List<GameObject> spawnpoints;
    private GameObject spawnpoint;
    private bool spawned = false;
    public int max_count;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!spawned) // Спавн происходит только при первом вхождении в комнату
            {
                for (int count = 1; count <= Random.Range(1, max_count); count++) // Рандомизируем число врагов
                {
                    spawnOne();
                }
            }
            spawned = true;
        }
    }

    private void spawnOne() // Спавн одного врага
    {
        int index_spawnpoint = Random.Range(0, spawnpoints.Count);
        Instantiate(enemy_types[Random.Range(0, enemy_types.Count)], spawnpoints[index_spawnpoint].transform.position, transform.rotation);
        spawnpoints.RemoveAt(index_spawnpoint);
    }
}
