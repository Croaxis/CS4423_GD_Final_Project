using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace cs4423fp.UI.Menu
{
    public class InGameMenuController : MonoBehaviour
    {
        public static bool GameIsPaused { get; private set; } = false;
        [SerializeField] GameObject pauseMenuOverlayUI;
        [SerializeField] GameObject settingsMenuOverlayUI;
        [SerializeField] GameObject loadMenuOverlayUI;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        void TogglePause()
        {
            GameIsPaused = !GameIsPaused;
            Time.timeScale = GameIsPaused ? 0f : 1f;
            pauseMenuOverlayUI.SetActive(GameIsPaused);
        }

        public void LoadSettingsMenu()
        {
            settingsMenuOverlayUI.SetActive(true);
        }

        public void Save(){
            Debug.Log("Save!");
        }

        public void LoadLoadMenu(){
            loadMenuOverlayUI.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}