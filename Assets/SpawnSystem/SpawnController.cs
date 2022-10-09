using Events;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject defaultPawn;

    public GameObject klerWarrior;
    public GameObject klerRanged;
    public GameObject klerSpecial;
    public GameObject moherWarrior;
    public GameObject moherRanged;
    public GameObject moherSpecial;
    public GameObject sebixWarrior;
    public GameObject sebixRanged;
    public GameObject sebixSpecial;

    public PawnSpawnedEvent pawnSpawnedEvent;

    Transform spawnPointLeft = null;
    Transform spawnPointRight = null;

    SpawnColliderController leftPointSpawnColliderController = null;
    SpawnColliderController rightPointSpawnColliderController = null;

    void Start()
    {
        this.spawnPointLeft = GameObject.FindGameObjectsWithTag("spawnPointLeft")[0].GetComponent(typeof(Transform)) as Transform;
        this.spawnPointRight = GameObject.FindGameObjectsWithTag("spawnPointRight")[0].GetComponent(typeof(Transform)) as Transform;
        this.leftPointSpawnColliderController = GameObject.FindGameObjectsWithTag("spawnColliderLeft")[0].GetComponent(typeof(SpawnColliderController)) as SpawnColliderController;
        this.rightPointSpawnColliderController = GameObject.FindGameObjectsWithTag("spawnColliderRight")[0].GetComponent(typeof(SpawnColliderController)) as SpawnColliderController;
    }

    public void SpawnUnits(SpawnPawn spawnPawnData)
    {
        Debug.Log(spawnPawnData);

        if (CanSpawn(spawnPawnData, leftPointSpawnColliderController.canSpawn, GameManager.PlayerSideEnum.Left))
        {
            Spawn(spawnPawnData, spawnPointLeft, GameManager.PlayerSideEnum.Left);
        }

        if (CanSpawn(spawnPawnData, rightPointSpawnColliderController.canSpawn, GameManager.PlayerSideEnum.Right))
        {
            Spawn(spawnPawnData, spawnPointRight, GameManager.PlayerSideEnum.Right);
        }
    }

    private void Spawn(SpawnPawn spawnPawnData, Transform spawnPoint, GameManager.PlayerSideEnum side)
    {
        GameObject spawnedPawn = null;

        switch(spawnPawnData.unit.Fraction)
        {
            case GameManager.FractionEnum.Sebix:
                {
                    switch(spawnPawnData.unit.PawnType)
                    {
                        case UnitsData.PawnTypeEnum.Warrior:
                            {
                                spawnedPawn = Instantiate(sebixWarrior, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Ranged:
                            {
                                spawnedPawn = Instantiate(sebixRanged, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Special:
                            {
                                spawnedPawn = Instantiate(sebixSpecial, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                    }
                    break;
                }
            case GameManager.FractionEnum.Moher:
                {
                    switch (spawnPawnData.unit.PawnType)
                    {
                        case UnitsData.PawnTypeEnum.Warrior:
                            {
                                spawnedPawn = Instantiate(moherWarrior, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Ranged:
                            {
                                spawnedPawn = Instantiate(moherRanged, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Special:
                            {
                                spawnedPawn = Instantiate(moherSpecial, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                    }
                    break;
                }
            case GameManager.FractionEnum.Kler:
                {
                    switch (spawnPawnData.unit.PawnType)
                    {
                        case UnitsData.PawnTypeEnum.Warrior:
                            {
                                spawnedPawn = Instantiate(klerWarrior, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Ranged:
                            {
                                spawnedPawn = Instantiate(klerRanged, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                        case UnitsData.PawnTypeEnum.Special:
                            {
                                (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.NiechZstapi);
                                spawnedPawn = Instantiate(klerSpecial, spawnPoint.position, spawnPoint.rotation);
                                break;
                            }
                    }
                    break;
                }
        }


        Debug.Log("dupa"); //MAGDA Log do not change that she is a coder now :P
        spawnedPawn.tag = side == GameManager.PlayerSideEnum.Left ? "Left" : "Right";
        PawnController controller = spawnedPawn.GetComponent<PawnController>();
        controller.UnitDataScriptableORGINAL = spawnPawnData.unit;
        controller.side = side;

        var pawnSpawned = new PawnSpawned
        {
            Side = spawnPawnData.Side,
            pawn = spawnedPawn
        };
        pawnSpawnedEvent.Raise(pawnSpawned);
    }

    private bool CanSpawn(SpawnPawn spawnPawnData, bool colliderCanSpawn, GameManager.PlayerSideEnum side)
    {
        return colliderCanSpawn && spawnPawnData.Side == side && GameManager.Instance.CanSideSpawn(side, spawnPawnData.unit.GoldToSpawn);
    }
}