using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDeleter : MonoBehaviour
{
    public bool isBossRoom = false; // Это комната босса?
    public bool player_in_this_room = false; // Игрок в данной комнате?
    public bool isRoomCleared = false; // Комната зачищена?
    public GameObject left_door; // Двери и roommovers
    public GameObject right_door;
    public GameObject up_door;
    public GameObject down_door;
    public GameObject left_mover;
    public GameObject right_mover;
    public GameObject up_mover;
    public GameObject down_mover;
    public GameObject wall;
    public bool have_left_door = true; // Какие двери есть
    public bool have_right_door = true;
    public bool have_down_door = true;
    public bool have_up_door = true;

    public void deleteDoors(List<string> directions) // Удаляет указанные двери и roommovers и заменяет их стенами
    {
        foreach (string direction in directions)
        {
            switch (direction)
            {
                case "left": // Лево
                    Instantiate(wall, left_door.transform.position, left_door.transform.rotation);
                    Destroy(left_door);
                    Destroy(left_mover);
                    have_left_door = false;
                    break;
                case "up": // Верх
                    Instantiate(wall, up_door.transform.position, up_door.transform.rotation);
                    Destroy(up_door);
                    Destroy(up_mover);
                    have_up_door = false;
                    break;
                case "right": // Право
                    Instantiate(wall, right_door.transform.position, right_door.transform.rotation);
                    Destroy(right_door);
                    Destroy(right_mover);
                    have_right_door = false;
                    break;
                case "down": // Низ
                    Instantiate(wall, down_door.transform.position, down_door.transform.rotation);
                    Destroy(down_door);
                    Destroy(down_mover);
                    have_down_door = false;
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision) // Отслеживание входа игрока
    {
        if (collision.CompareTag("Player"))
        {
            player_in_this_room = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // Зачистка комнаты
    {
        if (collision.CompareTag("Player"))
        {
            isRoomCleared = true;
            player_in_this_room = false;
        }
    }
}
