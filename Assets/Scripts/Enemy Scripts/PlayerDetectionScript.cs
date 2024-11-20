using Common;
using UnityEngine;

namespace Enemy_Scripts
{
    public class PlayerDetectionScript : MonoBehaviour
    {
        public bool PlayerDetected { get; private set; }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                PlayerDetected = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                PlayerDetected = false;
            }
        }
    }
}