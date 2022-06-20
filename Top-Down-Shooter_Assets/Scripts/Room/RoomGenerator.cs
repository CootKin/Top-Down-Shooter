using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class RoomGenerator : MonoBehaviour
{
    public DoorDeleter[] rooms_types_normal; // ���� ������� ������
    public DoorDeleter boss_room; // ������� � ������
    public DoorDeleter main_room; // ������� �������
    public DoorDeleter start_room_L; // ��������� �������
    public DoorDeleter start_room_R;
    public DoorDeleter start_room_U;
    public DoorDeleter start_room_D;
    private float main_y; // y ������� �������
    private float main_x; // x ������� �������
    public DoorDeleter[,] field; // ����
    public int size; // ������ ����
    private int count_rooms; // ����� ������
    private OpenDoor open_door; 
    List<string> directions_to_del = new List<string>(); // ����������� ��� ��������

    private void Start()
    {
        count_rooms = UnityEngine.Random.Range(30, 70);
        size = calculateSize();
        main_y = main_room.transform.position.y; // ���������� ���������� ������� �������
        main_x = main_room.transform.position.x;
        field = new DoorDeleter[size, size];
        field[size / 2, size / 2] = main_room; // � ������ ���� ������ ������� �������
        field[(size / 2) - 1, (size / 2)] = start_room_D; // ��������� ��������� ��������� �������
        field[(size / 2) + 1, (size / 2)] = start_room_U;
        field[(size / 2), (size / 2) + 1] = start_room_R;
        field[(size / 2), (size / 2) - 1] = start_room_L;
        spawnRooms(count_rooms);
        deleteAllExcessDoors();
        open_door = GetComponent<OpenDoor>();
        Invoke("activateOpenDoor", 0.20f); // ��������� ������ ������ ������ OpenDoor
    }
    private int calculateSize() // ���������� ������� ���� �� ������ ����� ������
    {
        int calculatedSize;
        calculatedSize = Convert.ToInt32(Math.Round(Mathf.Sqrt(count_rooms * 7), 0));
        if (calculatedSize < 9) calculatedSize = 9;
        if (calculatedSize % 2 == 0) calculatedSize += 1;
        return calculatedSize;
    }
    private void activateOpenDoor() // ���������� ������ ������ � ������ OpenDoor
    {
        open_door.doors = open_door.getGeneratedDoors();
    }
    private void spawnRooms(int count_rooms) // ��������� ������
    {
        int counter = 0;
        while (true)
        {
            for (int col = 0; col < size; col++) // �������� �� ����
            {
                for (int line = 0; line < size; line++)
                {
                    if (field[line, col] != null) // ���� ������� �������
                    {
                        List<int> available_directions = new List<int>();
                        if (field[line, col].have_down_door) // ��������� ������ ��������� ������
                            available_directions.Add(1);
                        if (field[line, col].have_up_door)
                            available_directions.Add(2);
                        if (field[line, col].have_left_door)
                            available_directions.Add(3);
                        if (field[line, col].have_right_door)
                            available_directions.Add(4);
                        int rand = available_directions[UnityEngine.Random.Range(0, available_directions.Count)]; // �������� �������� �����������

                        // ����� ������� �����
                        if (rand == 1 && field[line, col].have_down_door && field[line - 1, col] == null && haveOnlyOneNeighboor(line - 1, col)) // ���� ����� ������ ����� � ������ �� ��� ������ � ��� ����� ������ 1 ������
                        {
                            if (line >= 3) // ���� �� ������ �� ������� �������
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line - 1, col] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line - 1, col].transform.position = calculatePosition(line - 1, col); // ������ �� ������ �������
                                    counter += 1;
                                    continue;
                                }
                                else // ���� ��� ��������� �������, ������� ������� �����
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line - 1, col] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line - 1, col].transform.position = calculatePosition(line - 1, col); // ������ �� ������ �������
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // ����� ������� ������
                        if (rand == 2 && field[line, col].have_up_door && field[line + 1, col] == null && haveOnlyOneNeighboor(line + 1, col)) // ���� ����� ������ ����� � ������ �� ��� ������ � ��� ����� ������ 1 ������
                        {
                            if (line < size - 3) // ���� �� ������ �� ������� �������
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line + 1, col] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line + 1, col].transform.position = calculatePosition(line + 1, col); // ������ �� ������ �������
                                    counter += 1;
                                    continue;
                                }
                                else // ���� ��� ��������� �������, ������� ������� �����
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line + 1, col] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line + 1, col].transform.position = calculatePosition(line + 1, col); // ������ �� ������ �������
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // ����� ������� �����
                        if (rand == 3 && field[line, col].have_left_door && field[line, col - 1] == null && haveOnlyOneNeighboor(line, col - 1)) // ���� ����� ������ ����� � ������ �� ��� ������ � ��� ����� ������ 1 ������
                        {
                            if (col >= 3) // ���� �� ������ �� ������� �������
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line, col - 1] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line, col - 1].transform.position = calculatePosition(line, col - 1); // ������ �� ������ �������
                                    counter += 1;
                                    continue;
                                }
                                else // ���� ��� ��������� �������, ������� ������� �����
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line, col - 1] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line, col - 1].transform.position = calculatePosition(line, col - 1); // ������ �� ������ �������
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // ����� ������� ������
                        if (rand == 4 && field[line, col].have_right_door && field[line, col + 1] == null && haveOnlyOneNeighboor(line, col + 1)) // ���� ����� ������ ����� � ������ �� ��� ������ � ��� ����� ������ 1 ������
                        {
                            if (col < size - 3) // ���� �� ������ �� ������� �������
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line, col + 1] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line, col + 1].transform.position = calculatePosition(line, col + 1); // ������ �� ������ �������
                                    counter += 1;
                                    continue;
                                }
                                else // ���� ��� ��������� �������, ������� ������� �����
                                {
                                    // ������� ������� � �������� �� �����
                                    field[line, col + 1] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line, col + 1].transform.position = calculatePosition(line, col + 1); // ������ �� ������ �������
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private Vector3 calculatePosition(int line, int col) // ��������� ������� ����� ������� ������������ �������
    {
        Vector3 position;
        position.x = main_x + (col - size / 2) * 18;
        position.y = main_y + (line - size / 2) * 10;
        position.z = 0;
        return position;
    }
    private bool haveOnlyOneNeighboor(int line, int col) // ����� �� ��������� ������ ������ ������ ������
    {
        int count_neighboor = 0;

        if (field[line, col + 1] != null) count_neighboor += 1;
        if (field[line, col - 1] != null) count_neighboor += 1;
        if (field[line + 1, col] != null) count_neighboor += 1;
        if (field[line - 1, col] != null) count_neighboor += 1;

        if (count_neighboor == 1) return true;
        else return false;
    }
    private void deleteAllExcessDoors() // �������� ���� ������, ������� �� ����� � ������ �������
    {
        for (int col = 0; col < size; col++) 
        {
            for (int line = 0; line < size; line++)
            {
                if (field[line, col] != null)
                {
                    if (field[line, col].have_down_door && field[line - 1, col] == null)
                        directions_to_del.Add("down");
                    if (field[line, col].have_up_door && field[line + 1, col] == null)
                        directions_to_del.Add("up");
                    if (field[line, col].have_right_door && field[line, col + 1] == null)
                        directions_to_del.Add("right");
                    if (field[line, col].have_left_door && field[line, col - 1] == null)
                        directions_to_del.Add("left");
                    field[line, col].deleteDoors(directions_to_del);
                    directions_to_del = new List<string>();
                }
            }
        }
    }
}
