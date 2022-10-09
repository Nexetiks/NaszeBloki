using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;

    public Vector3 coinTarget;

    public float explosionPower;
    public float explosionRadius;

    public void Explode(int coinCount)
    {

        for (int i = 0; i < coinCount; i++)
        {
            var coin = Instantiate(coinPrefab, transform.position, Random.rotation, transform);
            coin.GetComponent<Coin>().target = coinTarget;
        }
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "coins")
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, 10.0f, ForceMode.Impulse);
                }
            }
            
        }
    }
}