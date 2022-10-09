using System.Collections.Generic;
using UnityEngine;
using Events;

public class BattleController : MonoBehaviour
{
    List<GameObject> spawnedPawns = new List<GameObject>();

    public void registerSpawnedPawn(PawnSpawned pawnSpawnedData)
    {
        this.spawnedPawns.Add(pawnSpawnedData.pawn);
    }

}
