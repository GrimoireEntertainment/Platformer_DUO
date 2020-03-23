using UnityEngine;

public class SpikeDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GetComponentInParent<Spike>().Triggered = true;
        }
    }
}
