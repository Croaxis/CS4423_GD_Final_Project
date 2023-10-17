using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuOverlayUI;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            TogglePause();
        }
    }

    public void TogglePause(){
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
        pauseMenuOverlayUI.SetActive(gameIsPaused);
    }

    public void LoadSettingsMenu(){
        Debug.Log("Loading Settings Menu!");
        
    }

    public void QuitGame(){
        Debug.Log("Quitting Game!");
        // Add a menu to ask if the user wants to return to the main menu or quit the application.
        //SceneManager.LoadScene( //Not ); or
        Application.Quit();
    }
}
