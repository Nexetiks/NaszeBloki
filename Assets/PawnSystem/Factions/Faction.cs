using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Faction : ScriptableObject
{
    public GameManager.FractionEnum faction = GameManager.FractionEnum.Sebix;
    public UnitsData warriorUnit;
    public UnitsData rangedUnit;
    public UnitsData specialUnit;
    public int expNeeded;

    public UnitsData GetUnitByType(UnitsData.PawnTypeEnum type)
    {
        switch (type)
        {
            case UnitsData.PawnTypeEnum.Warrior:
                return warriorUnit;
            case UnitsData.PawnTypeEnum.Ranged:
                return rangedUnit;
            case UnitsData.PawnTypeEnum.Special:
                return specialUnit;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}