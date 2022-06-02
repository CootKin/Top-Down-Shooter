using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    private Camera cam;
    public Vector3 camera_position_to_change;
    public Vector3 player_position_to_change;

    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += player_position_to_change;
            cam.transform.position += camera_position_to_change;
        }
    }
}
