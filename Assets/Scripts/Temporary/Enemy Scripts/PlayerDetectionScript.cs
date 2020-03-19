using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionScript : MonoBehaviour
{

    public bool playerDetected;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player") {
            playerDetected = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player") {
            playerDetected = false;
        }
    }
}
