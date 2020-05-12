using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avtomat : MonoBehaviour
{
    [SerializeField] float timeRate = 1;
    [SerializeField] GameObject missle;
    [SerializeField] float destroyAfterSeconds = 1;


    private float rate;

    void Update()
    {
        if(Time.time >= rate)
        {
            missle.GetComponent<Spike>().destroyAfterSeconds = destroyAfterSeconds;
            Instantiate(missle, transform.position, transform.rotation);
            rate = Time.time + timeRate;
        }
    }
}
