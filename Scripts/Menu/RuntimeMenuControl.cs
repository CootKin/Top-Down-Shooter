using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeMenuControl : MonoBehaviour
{
    public Texture2D cursor_game;
    public Texture2D cursor_interface;
    private Vector2 game_hot_spot = new Vector2(32, 32);
    public GameObject pause_menu;
    public GameObject upgrade_menu;
    public GameObject map_menu;
    public Map map;

    private void Update()
    {
        checkPause();
        checkUpgrade();
        checkMap();
    }
    private void checkPause()
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
    private void checkUpgrade()
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
    private void checkMap()
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
    public void continueInPausePressed()
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        pause_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void continueInUpgradePressed()
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        upgrade_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void continueInMapPressed()
    {
        Cursor.SetCursor(cursor_game, game_hot_spot, CursorMode.Auto);
        map_menu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    public void mainMenuPressed()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; 
    }
}
