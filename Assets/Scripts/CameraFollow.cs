using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform ballTarget;
    private Vector3 oldPos;

    private void Awake()
    {
        ballTarget = GameObject.FindGameObjectWithTag("Ball").transform;
        oldPos = ballTarget.position;
    }

    

    private void LateUpdate()
    {
        if (ballTarget)
        {
            Vector3 newPos = ballTarget.position;
            Vector3 delta = oldPos - newPos;
            delta.y = 0f;//по y не будет меняться
           
                         // Debug.Log(delta.x + " " + delta.z);
            transform.position = transform.position - delta;
            oldPos = newPos;


        }
    }
}
