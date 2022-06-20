using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    public Texture2D cursor_game; // Курсор для геймплея
    public Texture2D cursor_interface; // Курсор для интерфейса
    private Vector2 game_hot_spot = new Vector2(32, 32); // Точка клика

    private void Start()
    {
        Cursor.SetCursor(cursor_interface, Vector2.zero, CursorMode.Auto);
        if (SceneManager.sceneCount > 1) // Если запущены 2 сцены одновременно, выгружаем игровую сцену
        {
            SceneManager.UnloadSceneAsync("Game");
        }
    }
    public void playPressed() // Нажатие на "Начать игру"
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        SceneManager.LoadScene("Game");
    }
    public void exitPressed() // Нажатие на "Выйти из игры"
    {
        Application.Quit();
    }
}
