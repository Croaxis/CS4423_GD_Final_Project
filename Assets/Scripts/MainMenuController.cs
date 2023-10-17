using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame(){
        Debug.Log("Loading Load Game Menu!");
    }

    public void Settings(){
        Debug.Log("Loading Settings Menu!");
    }

    public void QuitGame(){
        Debug.Log("Quitting Game!");
        Application.Quit();
    }
}
