using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Structures
{
    public class StructureHandler : MonoBehaviour
    {
        public static StructureHandler Instance;
        
        [SerializeField] private StandardStructure starport, astroid;

        public LayerMask pUnitLayer, eUnitLayer;

        private void Awake(){
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public StructureStatTypes.Base GetBasicStructureStats(string type){
            StandardStructure structure;
            switch(type){
                case "starport":
                    structure = starport;
                    break;
                case "astroid":
                    structure = astroid;
                    break;
                default:
                    Debug.Log($"Structure Type: {type} couldn't be found or does not exist!");
                    return null;
            }
            return structure.baseStats;
        }
    }
}
