using UnityEngine;

namespace Enemy_Scripts
{
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
}
