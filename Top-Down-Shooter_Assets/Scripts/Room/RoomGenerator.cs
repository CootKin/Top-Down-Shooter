using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class RoomGenerator : MonoBehaviour
{
    public DoorDeleter[] rooms_types_normal; // Типы обычных комнат
    public DoorDeleter boss_room; // Комната с боссом
    public DoorDeleter main_room; // Главная комната
    public DoorDeleter start_room_L; // Стартовые комнаты
    public DoorDeleter start_room_R;
    public DoorDeleter start_room_U;
    public DoorDeleter start_room_D;
    private float main_y; // y главной комнаты
    private float main_x; // x главной комнаты
    public DoorDeleter[,] field; // Поле
    public int size; // Размер поля
    private int count_rooms; // Число комнат
    private OpenDoor open_door; 
    List<string> directions_to_del = new List<string>(); // Направления для удаления

    private void Start()
    {
        count_rooms = UnityEngine.Random.Range(30, 70);
        size = calculateSize();
        main_y = main_room.transform.position.y; // Запоминаем координаты главной комнаты
        main_x = main_room.transform.position.x;
        field = new DoorDeleter[size, size];
        field[size / 2, size / 2] = main_room; // В центре поля ставим главную комнату
        field[(size / 2) - 1, (size / 2)] = start_room_D; // Вставляем остальные стартовые комнаты
        field[(size / 2) + 1, (size / 2)] = start_room_U;
        field[(size / 2), (size / 2) + 1] = start_room_R;
        field[(size / 2), (size / 2) - 1] = start_room_L;
        spawnRooms(count_rooms);
        deleteAllExcessDoors();
        open_door = GetComponent<OpenDoor>();
        Invoke("activateOpenDoor", 0.20f); // Позволяем начать работу классу OpenDoor
    }
    private int calculateSize() // Вычисление размера поля на основе числа комнат
    {
        int calculatedSize;
        calculatedSize = Convert.ToInt32(Math.Round(Mathf.Sqrt(count_rooms * 7), 0));
        if (calculatedSize < 9) calculatedSize = 9;
        if (calculatedSize % 2 == 0) calculatedSize += 1;
        return calculatedSize;
    }
    private void activateOpenDoor() // Заполнение списка дверей в классе OpenDoor
    {
        open_door.doors = open_door.getGeneratedDoors();
    }
    private void spawnRooms(int count_rooms) // Генерация комнат
    {
        int counter = 0;
        while (true)
        {
            for (int col = 0; col < size; col++) // Проходим по полю
            {
                for (int line = 0; line < size; line++)
                {
                    if (field[line, col] != null) // Если находим комнату
                    {
                        List<int> available_directions = new List<int>();
                        if (field[line, col].have_down_door) // Заполняем список доступных дверей
                            available_directions.Add(1);
                        if (field[line, col].have_up_door)
                            available_directions.Add(2);
                        if (field[line, col].have_left_door)
                            available_directions.Add(3);
                        if (field[line, col].have_right_door)
                            available_directions.Add(4);
                        int rand = available_directions[UnityEngine.Random.Range(0, available_directions.Count)]; // Случайно выбираем напрваление

                        // Спавн комнаты снизу
                        if (rand == 1 && field[line, col].have_down_door && field[line - 1, col] == null && haveOnlyOneNeighboor(line - 1, col)) // Если имеет нижнюю дверь и ячейка за ней пустая и она имеет только 1 соседа
                        {
                            if (line >= 3) // Если не выйдем за границы массива
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line - 1, col] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line - 1, col].transform.position = calculatePosition(line - 1, col); // Ставим на нужную позицию
                                    counter += 1;
                                    continue;
                                }
                                else // Если это последняя комната, спавним комнату босса
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line - 1, col] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line - 1, col].transform.position = calculatePosition(line - 1, col); // Ставим на нужную позицию
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // Спавн комнаты сверху
                        if (rand == 2 && field[line, col].have_up_door && field[line + 1, col] == null && haveOnlyOneNeighboor(line + 1, col)) // Если имеет нижнюю дверь и ячейка за ней пустая и она имеет только 1 соседа
                        {
                            if (line < size - 3) // Если не выйдем за границы массива
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line + 1, col] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line + 1, col].transform.position = calculatePosition(line + 1, col); // Ставим на нужную позицию
                                    counter += 1;
                                    continue;
                                }
                                else // Если это последняя комната, спавним комнату босса
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line + 1, col] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line + 1, col].transform.position = calculatePosition(line + 1, col); // Ставим на нужную позицию
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // Спавн комнаты слева
                        if (rand == 3 && field[line, col].have_left_door && field[line, col - 1] == null && haveOnlyOneNeighboor(line, col - 1)) // Если имеет нижнюю дверь и ячейка за ней пустая и она имеет только 1 соседа
                        {
                            if (col >= 3) // Если не выйдем за границы массива
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line, col - 1] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line, col - 1].transform.position = calculatePosition(line, col - 1); // Ставим на нужную позицию
                                    counter += 1;
                                    continue;
                                }
                                else // Если это последняя комната, спавним комнату босса
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line, col - 1] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line, col - 1].transform.position = calculatePosition(line, col - 1); // Ставим на нужную позицию
                                    counter += 1;
                                    return;
                                }
                            }
                        }
                        // Спавн комнаты справа
                        if (rand == 4 && field[line, col].have_right_door && field[line, col + 1] == null && haveOnlyOneNeighboor(line, col + 1)) // Если имеет нижнюю дверь и ячейка за ней пустая и она имеет только 1 соседа
                        {
                            if (col < size - 3) // Если не выйдем за границы массива
                            {
                                if (counter < count_rooms - 1)
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line, col + 1] = Instantiate(rooms_types_normal[UnityEngine.Random.Range(0, rooms_types_normal.Length)],
                                        transform.position,
                                        transform.rotation);
                                    field[line, col + 1].transform.position = calculatePosition(line, col + 1); // Ставим на нужную позицию
                                    counter += 1;
                                    continue;
                                }
                                else // Если это последняя комната, спавним комнату босса
                                {
                                    // Спавним комнату и отмечаем на карте
                                    field[line, col + 1] = Instantiate(boss_room,
                                        transform.position,
                                        transform.rotation);
                                    field[line, col + 1].transform.position = calculatePosition(line, col + 1); // Ставим на нужную позицию
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
    private Vector3 calculatePosition(int line, int col) // Вычисляет позицию новой комнаты относительно главной
    {
        Vector3 position;
        position.x = main_x + (col - size / 2) * 18;
        position.y = main_y + (line - size / 2) * 10;
        position.z = 0;
        return position;
    }
    private bool haveOnlyOneNeighboor(int line, int col) // Имеет ли указанная ячейка только одного соседа
    {
        int count_neighboor = 0;

        if (field[line, col + 1] != null) count_neighboor += 1;
        if (field[line, col - 1] != null) count_neighboor += 1;
        if (field[line + 1, col] != null) count_neighboor += 1;
        if (field[line - 1, col] != null) count_neighboor += 1;

        if (count_neighboor == 1) return true;
        else return false;
    }
    private void deleteAllExcessDoors() // Удаление всех дверей, которые не ведут в другие комнаты
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
