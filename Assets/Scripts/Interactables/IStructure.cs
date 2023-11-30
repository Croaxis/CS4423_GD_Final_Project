using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Interactables
{
    public class IStructure : Interactable
    {
        public UI.HUD.PlayerActions actions;
        public GameObject spawnMarker = null;
        public GameObject spawnMarkerIcon = null;
        public float MaxMarkerDistance = 10f;
        public override void OnInteractEnter(){
            UI.HUD.ActionFrame.Instance.SetActionButtons(actions, spawnMarker);
            spawnMarkerIcon.SetActive(true);
            base.OnInteractEnter();
        }

        public override void OnInteractExit(){
            UI.HUD.ActionFrame.Instance.clearActions();
            spawnMarkerIcon.SetActive(false);
            base.OnInteractExit();
        }

        public void SetSpawnMarkerLocation(){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);

            if (hit.collider != null){
                if (spawnMarker != null){
                    spawnMarker.transform.position = hit.point;
                }
            }
        }

    }
}
