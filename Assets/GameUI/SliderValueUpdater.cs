using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdater : MonoBehaviour
{
    public GameManager.PlayerSideEnum side;
    public Slider slider;
    public TextMeshProUGUI textMesh;
    public int currentGoldValue = 0;
    public int currentExpValue = 0;
    
    public void UpdateExpSlider(IntValueChange val)
    {
        if (val.Side == side)
        {
            currentExpValue += val.Value;
            slider.value = currentExpValue;
        }
    }
    
    public void UpdateGoldText(IntValueChange val)
    {
        if (val.Side == side)
        {
            currentGoldValue += val.Value;
            textMesh.text = currentGoldValue.ToString();    
        }
    }

    public void OnUpgradeFaction(FactionChange val)
    {
        if (val.side == side)
        {
            slider.maxValue = GameManager.Instance.GetSideCurrentFaction(side).expNeeded;
        }
    }
}
