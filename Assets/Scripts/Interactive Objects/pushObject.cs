using Player_Scripts;
using UnityEngine;

namespace Interactive_Objects
{
    public class pushObject : MonoBehaviour
    {
        [SerializeField] float massForPlayerX;
        [SerializeField] float massForPlayerY;

        bool isPlayerHere;
        char xOrY;
        Rigidbody2D myRB;

        // Start is called before the first frame update
        void Start()
        {
            myRB = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
                myRB.mass = other.GetComponentInChildren<Stats>().xOrY == 'x' ? massForPlayerX : massForPlayerY;

                isPlayerHere = true;
            }
        }
    }
}
