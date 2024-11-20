using Common;
using Core;
using UnityEngine;

namespace Enemy_Scripts
{
    public class RangedEnemyWeaponScript : MonoBehaviour
    {
        [SerializeField] float amountOfDamage;
        [SerializeField] float speed;
        [SerializeField] float destroyAfterSeconds;

        private Rigidbody2D _myRB;

        void Start()
        {
            _myRB = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _myRB.linearVelocity = new Vector2(-speed, _myRB.linearVelocity.y);
            Destroy(gameObject, destroyAfterSeconds);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                Health playerHealth = other.gameObject.GetComponent<Health>();
                playerHealth.AddDamage(amountOfDamage);
            }
        }
    }
}