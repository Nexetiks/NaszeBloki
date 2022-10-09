using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSebixSpecialPawn : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        if (UnlockController.sebixSpecialUnlocked)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
}
