using Common;
using UnityEngine;

namespace Traps
{
    public class SpikeDetection : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                GetComponentInParent<Spike>().Triggered = true;
            }
        }
    }
}