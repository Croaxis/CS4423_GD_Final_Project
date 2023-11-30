using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.Menu.Audio
{
    public class SoundMenu : MonoBehaviour
    {

        [SerializeField] GameObject audioOptionsMenu;

        public void ToggleAudioOptionsMenu(){
            audioOptionsMenu.SetActive(false);
        }
    }
}