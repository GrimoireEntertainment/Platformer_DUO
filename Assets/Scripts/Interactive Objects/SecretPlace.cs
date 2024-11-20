using Common;
using UnityEngine;

namespace Interactive_Objects
{
    public class SecretPlace : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                Destroy(gameObject);
            }
        }
    }
}