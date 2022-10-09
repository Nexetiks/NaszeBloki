using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSway : MonoBehaviour
{

    Transform rectTransform;

    void Start()
    {
        this.rectTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float newScale = (Mathf.Abs(Mathf.Sin(Time.time)) + 3) / 3 ;
        //float rotationAmount = Mathf.Sin(cornerAngle) * radius;
        rectTransform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
