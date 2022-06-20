using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private float left_panel_coord = 700; // Крайние координаты панели
    private float right_panel_coord = 1200;
    private float down_panel_coord = 300;
    private float up_panel_coord = 800;

    private float panel_size_horizontal; // Размеры панели
    private float panel_size_vertical;
    public RoomGenerator room_generator;
    private DoorDeleter[,] field; // Поле
    private int field_size; // Размер поля
    private float image_size_vertical; // Размеры изображений
    private float image_size_horizontal;
    public GameObject room; // Изображения
    public GameObject room_cleared;
    public GameObject boss_room;
    public GameObject boss_room_cleared;
    public GameObject room_player;
    public GameObject boss_room_player;
    public Transform panel; 
    private Vector3 position_to_spawn; // Позиция спавна изображения


    private void Start()
    {
        panel_size_horizontal = left_panel_coord - right_panel_coord; // Расчитываем размеры панели
        panel_size_vertical = up_panel_coord - down_panel_coord;
        field = room_generator.field; // Получаем карту
        field_size = room_generator.size; // Получаем размер карты
        image_size_horizontal = panel_size_horizontal / (field_size - 4); // Получаем сколько в пикселях будет занимать картинка
        image_size_vertical = panel_size_vertical / (field_size - 4);
        room.transform.localScale = new Vector3(image_size_horizontal/100, image_size_vertical/100); // Меняем размеры изображений
        boss_room.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        room_player.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        boss_room_player.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        boss_room_cleared.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        room_cleared.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        drawMap();
    }

    public void drawMap() // Отрисовка карты
    {
        for (int col = 2; col < field_size-2; col++) // Проходим по карте
        {
            for (int line = 2; line < field_size-2; line++)
            {
                if (field[line, col] != null) // Если встречаем комнату
                {
                    if (field[line, col].isBossRoom && field[line, col].player_in_this_room) // Если комната с боссом и в ней игрок
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(boss_room_player, position_to_spawn, transform.rotation, panel);
                    }
                    else if (field[line, col].isBossRoom && !field[line, col].player_in_this_room) // Если комната с боссом и в ней нет игрока
                    {
                        if (!field[line, col].isRoomCleared) // Если комната не зачищена
                        {
                            position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                            Instantiate(boss_room, position_to_spawn, transform.rotation, panel);
                        }
                        else // Если комната зачищена
                        {
                            position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                            Instantiate(boss_room_cleared, position_to_spawn, transform.rotation, panel);
                        }
                    }
                    else if (!field[line, col].isBossRoom && field[line, col].player_in_this_room) // Если не комната с боссом и в ней игрок
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(room_player, position_to_spawn, transform.rotation, panel);
                    }
                    else if (!field[line, col].isBossRoom && !field[line, col].player_in_this_room) // Если не комната с боссом и в ней нет игрока
                    {
                        if (!field[line, col].isRoomCleared) // Если комната не зачищена
                        {
                            position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                            Instantiate(room, position_to_spawn, transform.rotation, panel);
                        }
                        else // Если комната зачищена
                        {
                            position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                            Instantiate(room_cleared, position_to_spawn, transform.rotation, panel);
                        }
                    }
                }
            }
        }
    }
}
