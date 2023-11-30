using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.Menu.ResolutionController
{
    public class ResolutionMenu : MonoBehaviour
    {
        [SerializeField] GameObject resolutionOptionsMenu;

        public void ToggleResolutionOptionsMenu(){
            resolutionOptionsMenu.SetActive(false);
        }
    }   
}