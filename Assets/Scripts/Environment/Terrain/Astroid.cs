using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Environment.Terrian
{
    public class Astroid : MonoBehaviour
    {
        public Structures.StandardStructure structureType;
        [HideInInspector]
        public Structures.StructureStatTypes.Base baseStats;
        public Units.UnitStatDisplay statDisplay;
        
        private void Start(){
            baseStats = structureType.baseStats;
            statDisplay = GetComponentInChildren<Units.UnitStatDisplay>();
            statDisplay.SetStatDisplayStandardStructure(baseStats, false);
        }
    }

}