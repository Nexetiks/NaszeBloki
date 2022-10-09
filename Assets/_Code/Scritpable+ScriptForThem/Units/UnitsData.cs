using UnityEngine;

[CreateAssetMenu(fileName = "_units-name_Data", menuName = "ScriptableObjects/UnitData", order = 1)]
public class UnitsData : ScriptableObject
{
    public UnitsData(UnitsData _unit)
    {
        NameScriptable = _unit.NameScriptable;
        Fraction = _unit.Fraction;
        PawnType = _unit.PawnType;
        AttackDemage = _unit.AttackDemage;
        AttackSpeed = _unit.AttackSpeed;
        AttackRange = _unit.AttackRange;
        HP = _unit.HP;
        MaxHP = _unit.MaxHP;
        Speed = _unit.Speed;
        TimeToSpawnUnit = _unit.TimeToSpawnUnit;
        GoldForKilling = _unit.GoldForKilling;
        ExpForKilling = _unit.ExpForKilling;
        CriticChange = _unit.CriticChange;
        icon = _unit.icon;
    }

    public enum PawnTypeEnum
    {
        Warrior,
        Ranged,
        Special
    }

    public string NameScriptable = "";
    public GameManager.FractionEnum Fraction = GameManager.FractionEnum.Sebix;
    public PawnTypeEnum PawnType = PawnTypeEnum.Warrior;
    public float AttackDemage = 0;
    public float AttackSpeed = 0;
    public float AttackRange = 0;
    public float HP = 0;
    public float MaxHP;
    public float Speed = 0;
    public float TimeToSpawnUnit = 0;
    public float GoldForKilling = 0;
    public float GoldToSpawn = 0;
    public float ExpForKilling = 0;
    public float CriticChange = 0;
    public Sprite icon;

    public bool getHit(float _demage)
    {
        HP = HP - _demage;

        if (HP <= 0)
        {;
            return true;
        }
        else
        {
            return false;
        }
    }
}