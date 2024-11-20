using UnityEngine;

namespace Player_Scripts
{
    public class ExtraScriptForJumping : MonoBehaviour
    {
        [SerializeField] GameObject tempGround;
        [SerializeField] Transform groundChecker;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Wall") {
                // playerPhysicsMaterial.friction = 1;
                tempGround.SetActive(true);
                Instantiate(tempGround, groundChecker.position, Quaternion.identity);
            }
        }
    }
}
