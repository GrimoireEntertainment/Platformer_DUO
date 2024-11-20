using Player_Scripts;
using UnityEngine;

namespace Interactive_Objects
{
    public class PushObject : MonoBehaviour
    {
        [SerializeField] float massForPlayerX;
        [SerializeField] float massForPlayerY;

        private bool _isPlayerHere;
        private char _xOrY;
        private Rigidbody2D _myRB;

        void Start()
        {
            _myRB = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _myRB.mass = other.GetComponentInChildren<Stats>().xOrY == 'x' ? massForPlayerX : massForPlayerY;

                _isPlayerHere = true;
            }
        }
    }
}