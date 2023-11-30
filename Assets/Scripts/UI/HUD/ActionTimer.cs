using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.HUD
{
    public class ActionTimer : MonoBehaviour
    {
        public static ActionTimer Instance = null;

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public IEnumerator SpawnQueueTimer(){
            if (ActionFrame.Instance != null && ActionFrame.Instance.spawnQueue.Count > 0){
                yield return new WaitForSeconds(ActionFrame.Instance.spawnQueue[0]);

                ActionFrame.Instance.SpawnObject();

                if (ActionFrame.Instance.spawnQueue.Count > 0){
                    StartCoroutine(SpawnQueueTimer());
                }
            }
        }
    }
}