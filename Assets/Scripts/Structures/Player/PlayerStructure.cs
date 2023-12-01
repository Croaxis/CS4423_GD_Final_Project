using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Structures.Player
{
    public class PlayerStructure : MonoBehaviour
    {
        public StandardStructure structureType;
        [HideInInspector]
        public StructureStatTypes.Base baseStats;

        public Units.UnitStatDisplay statDisplay;

        private void Start(){
            baseStats = structureType.baseStats;
            statDisplay = GetComponentInChildren<Units.UnitStatDisplay>();
            statDisplay.SetStatDisplayStandardStructure(structureType.structureName, baseStats, true);
        }
    }
}