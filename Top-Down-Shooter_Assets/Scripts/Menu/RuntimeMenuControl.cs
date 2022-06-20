using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeMenuControl : MonoBehaviour
{
    public Texture2D cursor_game; // Курсоры
    public Texture2D cursor_interface;
    private Vector2 game_hot_spot = new Vector2(32, 32); // Точка клика
    public GameObject pause_menu; // Меню
    public GameObject upgrade_menu;
    public GameObject map_menu;
    public Map map; 

    private void Update()
    {
        checkPause();
        checkUpgrade();
        checkMap();
    }
    private void checkPause() // Открытие меню паузы
    {
        if (Input.GetKeyDown("escape")) 
        {
            Cursor.SetCursor(cursor_interface, Vector2.zero, CursorMode.Auto);
            pause_menu.SetActive(true);
            upgrade_menu.SetActive(false); 
            map_menu.SetActive(false); 
            Time.timeScale = 0f; 
        }
    }
    private void checkUpgrade() // Открытие меню улучшений
    {
        if (Input.GetKeyDown("p")) 
        {
            Cursor.SetCursor(cursor_interface, Vector2.zero, CursorMode.Auto);
            if (FindObjectOfType<Enemy>() == null 
                && FindObjectOfType<Boss>() == null 
                && FindObjectOfType<Minion>() == null) 
            {
                upgrade_menu.SetActive(true);
                pause_menu.SetActive(false); 
                map_menu.SetActive(false); 
                Time.timeScale = 0f; 
            }
        }
    }
    private void checkMap() // Открытие карты
    {
        if (Input.GetKeyDown("m")) 
        {
            Cursor.SetCursor(cursor_interface, Vector2.zero, CursorMode.Auto);
            map_menu.SetActive(true);
            upgrade_menu.SetActive(false); 
            pause_menu.SetActive(false); 
            map.drawMap();
            Time.timeScale = 0f; 
        }
    }
    public void continueInPausePressed() // Нажатие на "Продолжить" в меню паузы
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        pause_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void continueInUpgradePressed() // Нажатие на "Продолжить" в меню улучшений
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        upgrade_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void continueInMapPressed() // Нажатие на "Продолжить" на карте
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        map_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void mainMenuPressed() // Нажатие на "Главное меню" в меню паузы
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; 
    }
}
