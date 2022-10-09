using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColliderController : MonoBehaviour
{
    public bool canSpawn = true;
    public HealthBarController healthBarController;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("pawn"))
        {
            canSpawn = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("pawn"))
        {
            canSpawn = true;
        }
    }
}
