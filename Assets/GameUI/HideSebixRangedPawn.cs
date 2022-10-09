using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSebixRangedPawn : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        if (UnlockController.sebixRangedUnlocked)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
    }
}
