using System;
using System.Linq;
using Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    bool canPlayBarka = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager Instance { get; private set; }

    #region Enums
    public enum FractionEnum
    {
        Sebix = 0,
        Moher,
        Kler
    }

    public enum PlayerSideEnum
    {
        Left,
        Right
    }

    public enum GameStateEnum
    {
        Menu,
        GamePlay,
        Pause,
        End
    }

    public class PlayerAttribute
    {
        public FractionEnum Fraction = FractionEnum.Sebix;
        public int CoinsAmount = 160;
        public float BaseHP = 1000;
        public float BaseAtacckSpeed = 0;
        
        public float BaseDemagePerHit = 0;
        public PlayerSideEnum Side;
        public float spawnCooldown = 0;
        public int expAmount = 0;

        public PlayerAttribute(PlayerSideEnum side)
        {
            this.Side = side;
        }
    }
    #endregion

    #region Variables

    public PlayerAttribute PlayerLeft = new PlayerAttribute(PlayerSideEnum.Left);
    public PlayerAttribute PlayerRight = new PlayerAttribute(PlayerSideEnum.Right);
    public GameStateEnum GameState = GameStateEnum.Menu;
    public FactionChangeGameEvent factionUpgradeEvent;
    public IntChangeGameEvent goldChangeGameEvent;
    public IntChangeGameEvent expChangeEvent;
    public IntChangeGameEvent cooldownInitEvent;

    public Faction[] factions;
    

    #endregion

    #region Methods

    private void FixedUpdate()
    {
        if (PlayerLeft.spawnCooldown > 0)
        {
            PlayerLeft.spawnCooldown -= Time.fixedDeltaTime;
        }
        
        if (PlayerRight.spawnCooldown > 0)
        {
            PlayerRight.spawnCooldown -= Time.fixedDeltaTime;
        }
    }

    public Faction GetSideCurrentFaction(PlayerSideEnum side)
    {
        if (side == PlayerSideEnum.Left)
        {
            return factions.First(fac => PlayerLeft.Fraction == fac.faction);
        }

        return factions.First(fac => PlayerRight.Fraction == fac.faction);
    }
    
    public void ChangeCoinsAmount(IntValueChange intValueChange)
    {
        if (intValueChange.Side == PlayerSideEnum.Left)
        {
            PlayerLeft.CoinsAmount += intValueChange.Value;
            return;
        }
        else
        {
            PlayerRight.CoinsAmount += intValueChange.Value;
        }
    }    
    public void ChangeExp(IntValueChange intValueChange)
    {
        if (intValueChange.Side == PlayerSideEnum.Left)
        {
            PlayerLeft.expAmount += intValueChange.Value;
            UpgradePlayerFaction(PlayerLeft);
            return;
        }
        else
        {
            PlayerRight.expAmount += intValueChange.Value;
            UpgradePlayerFaction(PlayerRight);
        }
    }
    public void UpgradePlayerFaction(PlayerSideEnum side)
    {
        if (side == PlayerSideEnum.Left)
        {
            UpgradePlayerFaction(PlayerLeft);
        }
        else
        {
            UpgradePlayerFaction(PlayerRight);
        }
    }
    private void UpgradePlayerFaction(PlayerAttribute player)
    {
        var factionExpNeeded = factions.First(fac => player.Fraction == fac.faction).expNeeded;
        if (player.Fraction != FractionEnum.Kler && player.expAmount >= factionExpNeeded)
        {
            (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.LevelUp);
            player.Fraction += 1;

            if(player.Fraction == GameManager.FractionEnum.Kler && canPlayBarka)
            {
                canPlayBarka = false;
                (GameObject.FindGameObjectsWithTag("AudioManager")[0].GetComponent(typeof(AudioController)) as AudioController).playAudio(AudioType.Barka);
            }
            factionUpgradeEvent.Raise(new FactionChange() {faction = player.Fraction, side = player.Side});
            expChangeEvent.Raise(new IntValueChange() {Side = player.Side, Value = -factionExpNeeded});
        }
    }
    public void OnPawnSpawned(PawnSpawned pawnSpawned)
    {
        var unitData = pawnSpawned.pawn.GetComponent<PawnController>().UnitDataScriptableORGINAL;
        if (pawnSpawned.Side == PlayerSideEnum.Left)
        {
            PlayerLeft.spawnCooldown = unitData.TimeToSpawnUnit;
            goldChangeGameEvent.Raise(new IntValueChange() {Side = PlayerSideEnum.Left, Value = -(int)unitData.GoldToSpawn});
            cooldownInitEvent.Raise(new IntValueChange() {Side = PlayerSideEnum.Left, Value = (int)PlayerLeft.spawnCooldown});
        }
        else
        {
            PlayerRight.spawnCooldown = unitData.TimeToSpawnUnit;
            goldChangeGameEvent.Raise(new IntValueChange() {Side = PlayerSideEnum.Right, Value = -(int)unitData.GoldToSpawn});
            cooldownInitEvent.Raise(new IntValueChange() {Side = PlayerSideEnum.Right, Value = (int)PlayerRight.spawnCooldown});
        }
    }
    #endregion

    public bool CanSideSpawn(PlayerSideEnum side, float unitGoldToSpawn)
    {
        switch (side)
        {
            case PlayerSideEnum.Left:
                return PlayerLeft.spawnCooldown <= 0 && PlayerLeft.CoinsAmount >= unitGoldToSpawn;
            case PlayerSideEnum.Right:
                return PlayerRight.spawnCooldown <= 0 && PlayerRight.CoinsAmount >= unitGoldToSpawn;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
    }
}