using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject[] doors = null;
    private Enemy enemy;
    private Boss boss;
    private Minion minion;

    private void Update()
    {
        if (doors != null)
        {
            enemy = FindObjectOfType<Enemy>();
            boss = FindObjectOfType<Boss>();
            minion = FindObjectOfType<Minion>();
            if (enemy == null && boss == null && minion == null)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);
                }
            }
            if (enemy != null || boss != null || minion != null)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
        }
    }
    public GameObject[] getGeneratedDoors()
    {
        GameObject[] doors;
        doors = GameObject.FindGameObjectsWithTag("Door");
        return doors;
    }
}
