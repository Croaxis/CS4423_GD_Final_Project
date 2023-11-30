using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Structures
{
    [CreateAssetMenu(fileName = "Structure", menuName = "Create New Structure/Standard")]
    public class StandardStructure : ScriptableObject
    {
        public enum structureType{
            Starport
        }

        [Space(15)]
        [Header("Structure Settings")]
        public structureType type;
        public string structureName;
        public GameObject structurePrefab;
        public GameObject icon;
        public float spawnTime;

        [Space(15)]
        [Header("Structure Base Stats")]
        public StructureStatTypes.Base baseStats;
    }
}

