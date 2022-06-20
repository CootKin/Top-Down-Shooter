using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeleter : MonoBehaviour
{
    public bool isBossRoom = false; // ��� ������� �����?
    public bool player_in_this_room = false; // ����� � ������ �������?
    public bool isRoomCleared = false; // ������� ��������?
    public GameObject left_door; // ����� � roommovers
    public GameObject right_door;
    public GameObject up_door;
    public GameObject down_door;
    public GameObject left_mover;
    public GameObject right_mover;
    public GameObject up_mover;
    public GameObject down_mover;
    public GameObject wall;
    public bool have_left_door = true; // ����� ����� ����
    public bool have_right_door = true;
    public bool have_down_door = true;
    public bool have_up_door = true;

    public void deleteDoors(List<string> directions) // ������� ��������� ����� � roommovers � �������� �� �������
    {
        foreach (string direction in directions)
        {
            switch (direction)
            {
                case "left": // ����
                    Instantiate(wall, left_door.transform.position, left_door.transform.rotation);
                    Destroy(left_door);
                    Destroy(left_mover);
                    have_left_door = false;
                    break;
                case "up": // ����
                    Instantiate(wall, up_door.transform.position, up_door.transform.rotation);
                    Destroy(up_door);
                    Destroy(up_mover);
                    have_up_door = false;
                    break;
                case "right": // �����
                    Instantiate(wall, right_door.transform.position, right_door.transform.rotation);
                    Destroy(right_door);
                    Destroy(right_mover);
                    have_right_door = false;
                    break;
                case "down": // ���
                    Instantiate(wall, down_door.transform.position, down_door.transform.rotation);
                    Destroy(down_door);
                    Destroy(down_mover);
                    have_down_door = false;
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) // ������������ ����� ������
    {
        if (collision.CompareTag("Player"))
        {
            player_in_this_room = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // �������� �������
    {
        if (collision.CompareTag("Player"))
        {
            isRoomCleared = true;
            player_in_this_room = false;
        }
    }
}
