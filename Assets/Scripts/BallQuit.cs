using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallQuit : MonoBehaviour
{
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
           StartCoroutine(nameof(StopShow));
        }
    }

    IEnumerator StopShow()
    {
       yield return new WaitForSeconds(3f);

       gameObject.SetActive(false);
    }
}
