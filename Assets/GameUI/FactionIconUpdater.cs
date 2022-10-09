using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionIconUpdater : MonoBehaviour
{
    public GameManager.PlayerSideEnum side;
    public Image warriorIconImage;
    public Image rangedIconImage;
    public Image specialIconImage;
    
    public TextMeshProUGUI costWText;
    public TextMeshProUGUI costRText;
    public TextMeshProUGUI costSText;

    public Faction sebixFaction;
    public Faction moherFaction;
    public Faction klerFaction;

    public Slider[] sliders;
    public GameObject[] cooldownBorders;
    private float currentCooldown;

    private void Update()
    {
        if (currentCooldown <= 0)
        {
            foreach (var border in cooldownBorders)
            {
                border.SetActive(false);
            }
            return;
        }
        foreach (var border in cooldownBorders)
        {
            border.SetActive(true);
        }

        currentCooldown -= Time.deltaTime;
        foreach (var slider in sliders)
        {
            slider.value = currentCooldown;
        }
    }

    public void FactionChange(FactionChange factionChange)
    {
        if (side != factionChange.side)
        {
            return;
        }

        var newFaction = GetFaction(factionChange.faction);
        warriorIconImage.sprite = newFaction.warriorUnit.icon;
        costWText.text = newFaction.warriorUnit.GoldToSpawn + " PLN";
        rangedIconImage.sprite = newFaction.rangedUnit.icon;
        costRText.text = newFaction.rangedUnit.GoldToSpawn + " PLN";
        specialIconImage.sprite = newFaction.specialUnit.icon;
        costSText.text = newFaction.specialUnit.GoldToSpawn + " PLN";
    }

    private Faction GetFaction(GameManager.FractionEnum faction)
    {
        switch (faction)
        {
            case GameManager.FractionEnum.Sebix:
                return sebixFaction;
            case GameManager.FractionEnum.Moher:
                return moherFaction;
            case GameManager.FractionEnum.Kler:
                return klerFaction;
            default:
                throw new ArgumentOutOfRangeException(nameof(faction), faction, null);
        }
    }
    
    public void CooldownChange(IntValueChange cooldown)
    {
        if (side != cooldown.Side)
        {
            return;
        }

        currentCooldown = cooldown.Value;
        foreach (var slider in sliders)
        {
            slider.value = currentCooldown;
            slider.maxValue = currentCooldown;
        }
    }
}