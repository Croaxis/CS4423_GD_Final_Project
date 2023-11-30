using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Structures
{
    public class StructureStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base{
            public float structureCost, structureHP, structureArmor, structureAttack, structureAttackRange, structureAttackSpeed, structureAggroRange;

        }
    }
}