using Common;
using UnityEngine;

namespace Enemy_Scripts
{
    public class FreezeFlyEnemyOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                print("freeze");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}