using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWind : MonoBehaviour
{

    Transform rectTransform;
    Vector3 initialScale;
    float initialOffset;

    void Start()
    {
        this.rectTransform = GetComponent<Transform>();
        this.initialScale = this.rectTransform.localScale;
        this.initialOffset = Random.Range(1, 15);
    }


    void Update()
    {
        float newScale = (Mathf.Abs(Mathf.Sin(Time.time + this.initialOffset)) / 20);
        //float rotationAmount = Mathf.Sin(cornerAngle) * radius;
        rectTransform.localScale = new Vector3(this.initialScale.x + newScale, this.initialScale.y + newScale, this.initialScale.z);
    }
}
