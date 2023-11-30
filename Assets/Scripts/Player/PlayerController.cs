using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cs4423fp.InputController;

namespace cs4423fp.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;

        public Transform PlayerUnits;
        public Transform EnemyUnits;
        public Transform PlayerStructures;

        public Color PlayerColor = Color.blue;
        public Color EnemyColor = Color.red;

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
            SetBasicStats(PlayerUnits);
            SetBasicStats(EnemyUnits);
            SetBasicStats(PlayerStructures);
        }

        private void Update()
        {
            InputHandler.Instance.HandleUnitMovement();
        }

        public void SetBasicStats(Transform type){
            foreach (Transform child in type){
                foreach (Transform tf in child){
                    string name = child.name.Substring(0, child.name.Length - 1).ToLower();

                    if( type == PlayerUnits ){
                        Units.Player.PlayerUnit pUnit = tf.GetComponent<Units.Player.PlayerUnit>();
                        pUnit.baseStats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                        Transform colorAccentTransform = pUnit.transform.Find("ColorAccent");
                        if (colorAccentTransform != null){
                            SpriteRenderer colorAccentRenderer = colorAccentTransform.GetComponent<SpriteRenderer>();
                            if (colorAccentRenderer != null){
                                colorAccentRenderer.color = PlayerColor;
                            }
                            else{
                                Debug.LogWarning("SpriteRenderer not found on ColorAccent.");
                            }
                        }
                        else{
                            Debug.LogWarning("ColorAccent not found as a child of PlayerUnit.");
                        }
                    }
                    else if (type == EnemyUnits ){
                        Units.Enemy.EnemyUnit eUnit = tf.GetComponent<Units.Enemy.EnemyUnit>();
                        eUnit.baseStats = Units.UnitHandler.Instance.GetBasicUnitStats(name);
                        Transform colorAccentTransform = eUnit.transform.Find("ColorAccent");
                        if (colorAccentTransform != null){
                            SpriteRenderer colorAccentRenderer = colorAccentTransform.GetComponent<SpriteRenderer>();
                            if (colorAccentRenderer != null){
                                colorAccentRenderer.color = EnemyColor;
                            }
                            else{
                                Debug.LogWarning("SpriteRenderer not found on ColorAccent.");
                            }
                        }
                        else{
                            Debug.LogWarning("ColorAccent not found as a child of PlayerUnit.");
                        }
                    }
                    else if( type == PlayerStructures ){
                        Structures.Player.PlayerStructure pStructure = tf.GetComponent<Structures.Player.PlayerStructure>();
                        pStructure.baseStats = Structures.StructureHandler.Instance.GetBasicStructureStats(name);
                        Transform colorAccentTransform = pStructure.transform.Find("ColorAccent");
                        if (colorAccentTransform != null){
                            SpriteRenderer colorAccentRenderer = colorAccentTransform.GetComponent<SpriteRenderer>();
                            if (colorAccentRenderer != null){
                                colorAccentRenderer.color = PlayerColor;
                            }
                            else{
                                Debug.LogWarning("SpriteRenderer not found on ColorAccent.");
                            }
                        }
                        else{
                            Debug.LogWarning("ColorAccent not found as a child of PlayerUnit.");
                        }
                    }
                }
            }
        }
    }
}