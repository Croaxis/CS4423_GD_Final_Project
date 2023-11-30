using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Interactables
{
    public class Interactable : MonoBehaviour
    {
        public bool isInteracting = false;
        public GameObject selected = null;
        public virtual void Awake(){
            selected.SetActive(false);
        }
        public virtual void OnInteractEnter(){
            ShowSelected();
            isInteracting = true;
        }
        public virtual void OnInteractExit(){
            HideSelected();
            isInteracting = false;
        }
        public virtual void ShowSelected(){
            selected.SetActive(true);
        }
        public virtual void HideSelected(){
            selected.SetActive(false);
        }
    }   
}