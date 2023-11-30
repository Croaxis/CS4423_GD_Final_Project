using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.Menu
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] GameObject settingsMenuOverlayUI;
        [SerializeField] GameObject audioOptionsMenu;
        [SerializeField] GameObject resolutionOptionsMenu;

        public void ToggleOptionsMenu(){
            settingsMenuOverlayUI.SetActive(false);
        }

        public void LoadAudioOptions(){
            audioOptionsMenu.SetActive(true);
        }

        public void LoadResolutionOptions(){
            resolutionOptionsMenu.SetActive(true);
        }
    }   
}