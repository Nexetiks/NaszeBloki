using System.Collections;
using System.Collections.Generic;
using Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public SpawnPawnEvent spawnEvent;
    public UnitsData unit;

    public void SpawnZ(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }
        Debug.Log("z");
        SpawnPawnOnSide(UnitsData.PawnTypeEnum.Warrior,  GameManager.PlayerSideEnum.Left);
    }

    public void SpawnX(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !UnlockController.sebixRangedUnlocked)
        {
            return;
        }
        Debug.Log("x");
        SpawnPawnOnSide(UnitsData.PawnTypeEnum.Ranged,  GameManager.PlayerSideEnum.Left);
    }

    public void SpawnC(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !UnlockController.sebixSpecialUnlocked)
        {
            return;
        }
        Debug.Log("c");
        SpawnPawnOnSide(UnitsData.PawnTypeEnum.Special,  GameManager.PlayerSideEnum.Left);
    }

    public void SpawnNum1(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }
       Debug.Log("1");                                                                     
       SpawnPawnOnSide(UnitsData.PawnTypeEnum.Warrior,  GameManager.PlayerSideEnum.Right);  
    }

    public void SpawnNum2(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !UnlockController.sebixRangedUnlocked)
        {
            return;
        }
        Debug.Log("2");                                                                  
        SpawnPawnOnSide(UnitsData.PawnTypeEnum.Ranged,  GameManager.PlayerSideEnum.Right);
    }

    public void SpawnNum3(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || !UnlockController.sebixSpecialUnlocked)
        {
            return;
        }
         Debug.Log("3");                                                                    
         SpawnPawnOnSide(UnitsData.PawnTypeEnum.Special,  GameManager.PlayerSideEnum.Right); 
    }

    public void UpgradeLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }
        GameManager.Instance.UpgradePlayerFaction(GameManager.PlayerSideEnum.Left);
    }

    public void UpgradeRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed)
        {
            return;
        }
        GameManager.Instance.UpgradePlayerFaction(GameManager.PlayerSideEnum.Right);
    }

    private void SpawnPawnOnSide(UnitsData.PawnTypeEnum unitType, GameManager.PlayerSideEnum side)
    {
        var faction = GameManager.Instance.GetSideCurrentFaction(side);
        var spawnPawn = new SpawnPawn();
        spawnPawn.Side = side;
        spawnPawn.unit = faction.GetUnitByType(unitType);
        Debug.Log("spawn: " + unitType + " side:" + side);
        spawnEvent.Raise(spawnPawn);
    }
}