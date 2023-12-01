using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject settingsMenuOverlayUI;
    [SerializeField] GameObject loadMenuOverlayUI;
    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame(){
        loadMenuOverlayUI.SetActive(true);
    }

    public void Settings(){
        settingsMenuOverlayUI.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }
}