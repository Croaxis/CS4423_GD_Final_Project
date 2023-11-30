using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace cs4423fp.UI.HUD
{
    public class ActionFrame : MonoBehaviour
    {
        public static ActionFrame Instance = null;

        [SerializeField] private Button actionButton = null;
        [SerializeField] private Transform layoutGroup = null;

        private List<Button> buttons = new List<Button>();
        private PlayerActions actionsList = null;
        public List<float> spawnQueue = new List<float>();
        public List<GameObject> spawnOrder = new List<GameObject>();
        public List<string> spawnOrderName = new List<string>();
        public GameObject spawnPoint = null; 

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public void SetActionButtons(PlayerActions actions, GameObject spawnLocation){
            actionsList = actions;
            spawnPoint = spawnLocation;

            if (actions.standardUnits.Count > 0){
                foreach(Units.StandardUnit unit in actions.standardUnits){
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = unit.name;
                    GameObject icon = Instantiate(unit.icon, btn.transform);
                    icon.GetComponentInChildren<Image>().sprite = unit.unitPrefab.GetComponent<SpriteRenderer>().sprite;
                    buttons.Add(btn);
                }
            }

            if (actions.standardStructures.Count > 0){
                foreach(Structures.StandardStructure structure in actions.standardStructures){
                    Button btn = Instantiate(actionButton, layoutGroup);
                    btn.name = structure.name;
                    GameObject icon = Instantiate(structure.icon, btn.transform);
                    icon.GetComponentInChildren<Image>().sprite = structure.structurePrefab.GetComponent<SpriteRenderer>().sprite;
                    buttons.Add(btn);
                }
            }
        }

        public void clearActions(){
            foreach (Button btn in buttons){
                Destroy(btn.gameObject);
            }
            buttons.Clear();
        }

        public void StartSpawn(string toSpawn){
            if(IsUnit(toSpawn)){
                Units.StandardUnit unit = IsUnit(toSpawn);
                spawnQueue.Add(unit.spawnTime);
                spawnOrder.Add(unit.unitPrefab);
                spawnOrderName.Add(unit.unitName);
            }
            else if(IsStructure(toSpawn)){
                Structures.StandardStructure structure = IsStructure(toSpawn);
                spawnQueue.Add(structure.spawnTime);
                spawnOrder.Add(structure.structurePrefab);
                spawnOrderName.Add(structure.structureName);
            }
            else{
                Debug.Log($"ERROR: {toSpawn} Is An Invalid Spawnable Object");
            }

            if(spawnQueue.Count == 1){
                ActionTimer.Instance.StartCoroutine(ActionTimer.Instance.SpawnQueueTimer());
            }
            else if (spawnQueue.Count == 0){
                ActionTimer.Instance.StopAllCoroutines();
            }
        }

        private Units.StandardUnit IsUnit(string name){
            if (actionsList.standardUnits.Count > 0){
                foreach (Units.StandardUnit unit in actionsList.standardUnits)
                {
                    if(unit.name == name){
                        return unit;
                    }
                }
            }
            return null;
        }

        private Structures.StandardStructure IsStructure(string name){
            if (actionsList.standardStructures.Count > 0){
                foreach (Structures.StandardStructure structure in actionsList.standardStructures)
                {
                    if(structure.name == name){
                        return structure;
                    }
                }
            }
            return null;
        }

        public void SpawnObject(){
            GameObject spawnObject = Instantiate(spawnOrder[0], new Vector2(spawnPoint.transform.parent.position.x, spawnPoint.transform.parent.position.y), Quaternion.identity);

            spawnObject.AddComponent<Units.Player.PlayerUnit>();
            spawnObject.GetComponent<Units.Player.PlayerUnit>().unitType = Units.UnitHandler.Instance.GetUnitType(spawnOrderName[0]);
            string type = char.ToUpper(spawnOrderName[0][0]) + spawnOrderName[0].Substring(1);

            spawnObject.transform.SetParent(GameObject.FindWithTag( "Player" + type + "s").transform);

            Transform colorAccentTransform = spawnObject.GetComponent<Units.Player.PlayerUnit>().transform.Find("ColorAccent");
            if (colorAccentTransform != null){
                SpriteRenderer colorAccentRenderer = colorAccentTransform.GetComponent<SpriteRenderer>();
                if (colorAccentRenderer != null){
                    colorAccentRenderer.color = Player.PlayerController.Instance.PlayerColor;
                }
                else{
                    Debug.LogWarning("SpriteRenderer not found on ColorAccent.");
                }
            }
            else{
                Debug.LogWarning("ColorAccent not found as a child of PlayerUnit.");
            }

            spawnObject.GetComponent<Units.Player.PlayerUnit>().MoveUnit(spawnPoint.transform.position);

            spawnQueue.Remove(spawnQueue[0]);
            spawnOrder.Remove(spawnOrder[0]);
        }
    }
}