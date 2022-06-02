using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private float left_panel_coord = 610; // ������� ���������� ������
    private float right_panel_coord = 1310;
    private float down_panel_coord = 200;
    private float up_panel_coord = 900;

    private float panel_size_horizontal; // ������� ������
    private float panel_size_vertical;
    public RoomGenerator room_generator;
    private DoorDeleter[,] field;
    private int field_size;
    private float image_size_vertical;
    private float image_size_horizontal;
    public GameObject room;
    public GameObject boss_room;
    public GameObject room_player;
    public GameObject boss_room_player;
    public Transform panel;
    private Vector3 position_to_spawn;


    private void Start()
    {
        panel_size_horizontal = left_panel_coord - right_panel_coord; // ����������� ���������� ������
        panel_size_vertical = up_panel_coord - down_panel_coord;
        field = room_generator.field; // �������� �����
        field_size = room_generator.size; // �������� ������ �����
        image_size_horizontal = panel_size_horizontal / (field_size - 4); // �������� ������� � �������� ����� �������� ��������
        image_size_vertical = panel_size_vertical / (field_size - 4);
        room.transform.localScale = new Vector3(image_size_horizontal/100, image_size_vertical/100);
        boss_room.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        room_player.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        boss_room_player.transform.localScale = new Vector3(image_size_horizontal / 100, image_size_vertical / 100);
        drawMap();
    }

    public void drawMap()
    {
        for (int col = 2; col < field_size-2; col++) // �������� �� �����
        {
            for (int line = 2; line < field_size-2; line++)
            {
                if (field[line, col] != null) // ���� ��������� �������
                {
                    if (field[line, col].isBossRoom && field[line, col].player_in_this_room)
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(boss_room_player, position_to_spawn, transform.rotation, panel);
                    }
                    else if (field[line, col].isBossRoom && !field[line, col].player_in_this_room)
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(boss_room, position_to_spawn, transform.rotation, panel);
                    }
                    else if (!field[line, col].isBossRoom && field[line, col].player_in_this_room)
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(room_player, position_to_spawn, transform.rotation, panel);
                    }
                    else if (!field[line, col].isBossRoom && !field[line, col].player_in_this_room)
                    {
                        position_to_spawn = new Vector3((-(col - 2) * image_size_horizontal + left_panel_coord - image_size_horizontal / 2), ((line - 2) * image_size_vertical + down_panel_coord + image_size_vertical / 2));
                        Instantiate(room, position_to_spawn, transform.rotation, panel);
                    }
                }
            }
        }
    }
}
