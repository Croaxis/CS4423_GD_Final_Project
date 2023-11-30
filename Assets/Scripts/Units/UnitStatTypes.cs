using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cs4423fp.Units
{
    public class UnitStatTypes : ScriptableObject
    {
        [System.Serializable]
        public class Base{
            public float unitCost, unitHP, unitArmor, unitAttack, unitAttackRange, unitAttackSpeed, unitAggroRange;
        }
    }
}

