using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnKnockback : MonoBehaviour
{
    public Transform knockbackTarget;
    public bool isDead;
    
    public void Knockback(float force, float duration, bool isBackwards)
    {
        var direction = new Vector3(0, -0.2f, 1);
        
        var targetPosition = knockbackTarget.localPosition - direction * force;
        StartCoroutine(LerpPosition(targetPosition, duration));
    }
    
    
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = knockbackTarget.localPosition;
        while (time < duration && !isDead)
        {
            if (time < duration * 0.5f)
            {
                knockbackTarget.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            }
            else
            {
                knockbackTarget.localPosition = Vector3.Lerp(targetPosition, startPosition, time / duration);
            }
            
            time += Time.deltaTime;
            yield return null;
        }
        knockbackTarget.localPosition = startPosition;
    }
}