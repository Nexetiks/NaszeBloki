using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 target;
    public float freeFallCooldown = 0.75f;
    public float currentFreefallTime = 0f;

    private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (currentFreefallTime < freeFallCooldown)
        {
            currentFreefallTime += Time.fixedDeltaTime;    
        }
        else
        {
            if (_rigidbody.useGravity)
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.useGravity = false;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                Destroy(gameObject);
            }
        }
        
        
    }
}