using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Create New Unit/Standard")]
    public class StandardUnit : ScriptableObject
    {
        public enum unitType
        {
            Harvester,
            Workship,
            Scoutship,
            Warship
        };

        [Header("Unit Settings")]
        [Space(15)]
        public unitType type;
        public string unitName;
        public GameObject unitPrefab;
        public GameObject icon;
        public float spawnTime;
        
        [Space(40)]
        [Header("Unit Base Stats")]
        [Space(15)]

        public UnitStatTypes.Base baseStats;
    }
}