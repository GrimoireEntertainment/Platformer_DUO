using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private float pointOfTime;
    [SerializeField] float secondsBeforeDestroy;
    void Start()
    {
        pointOfTime = Time.time + secondsBeforeDestroy;
    }

    void Update()
    {
        if(Time.time > pointOfTime) Destroy(gameObject); 
    }
}
