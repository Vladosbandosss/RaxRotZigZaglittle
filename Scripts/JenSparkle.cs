using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JenSparkle : MonoBehaviour
{
    [SerializeField] GameObject sparkleFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("эффукт");
            Instantiate(sparkleFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
