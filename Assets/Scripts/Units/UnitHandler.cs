using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cs4423fp.Player;

namespace cs4423fp.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler Instance;
        
        [SerializeField] private StandardUnit harvester, workship, scoutship, warship;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public UnitStatTypes.Base GetBasicUnitStats(string type){
            StandardUnit unit;
            switch(type){
                case "harvester":
                    unit = harvester;
                    break;
                case "workship":
                    unit = workship;
                    break;
                case "scoutship":
                    unit = scoutship;
                    break;
                case "warship":
                    unit = warship;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} couldn't be found or does not exist!");
                    return null;
            }
            return unit.baseStats;
        }

        public StandardUnit GetUnitType(string type){
            StandardUnit unit;
            switch(type){
                case "harvester":
                    unit = harvester;
                    break;
                case "workship":
                    unit = workship;
                    break;
                case "scoutship":
                    unit = scoutship;
                    break;
                case "warship":
                    unit = warship;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} couldn't be found or does not exist!");
                    return null;
            }
            return unit;
        }
    }
}

