using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitData
{
    string GetUnitType();
    float GetHP();
    float GetMoveSpeed();
    float GetAttackSpeed();
    Sprite GetUnitSprite();
}
