using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.UI.HUD
{
    [CreateAssetMenu(fileName = "NewPlayerActions", menuName = "PlayerActions")]
    public class PlayerActions : ScriptableObject
    {
        [Space(15)]
        [Header("Units")]
        public List<Units.StandardUnit> standardUnits = new List<Units.StandardUnit>();
        [Space(15)]
        [Header("Structures")]
        public List<Structures.StandardStructure> standardStructures = new List<Structures.StandardStructure>();
    }   
}