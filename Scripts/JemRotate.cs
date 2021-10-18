using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JemRotate : MonoBehaviour
{
    float speed = 1f;
    float angle;
    

    
    void Update()
    {
        angle = (angle + speed) % 360f;
        transform.localRotation = Quaternion.Euler(new Vector3(45f, 45f, angle));
    }
}
