using Common;
using UnityEngine;

namespace Player_Scripts
{
    public class ExtraScriptForJumping : MonoBehaviour
    {
        [SerializeField] GameObject tempGround;
        [SerializeField] Transform groundChecker;
    
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag(Tags.Wall)) {
                // playerPhysicsMaterial.friction = 1;
                tempGround.SetActive(true);
                Instantiate(tempGround, groundChecker.position, Quaternion.identity);
            }
        }
    }
}
