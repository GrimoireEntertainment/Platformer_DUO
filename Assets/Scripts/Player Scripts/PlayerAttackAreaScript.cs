using Common;
using Core;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerAttackAreaScript : MonoBehaviour
    {
        [SerializeField] private PressChecker swordAttackButton;
        [SerializeField] private GameObject projectile = null;
        [SerializeField] private GameObject missileStartPosition;
        [SerializeField] private float rangedAnimationRate;
        [SerializeField] private Animator playerXAnim;
        [SerializeField] private Animator playerYAnim;
        [SerializeField] private float playerDamage;
        [SerializeField] private float damageRate;
        [SerializeField] private float nextRangedLounchgeRate = 1;

        public float PlayerDamage => playerDamage;

        private float _nextDamage = 0.0f;
        private float _nextRangedLounch = 0;
        private float _rangedAnimationTime = 0f;
        private PlayerController _playerController;

        private static readonly int Attacked = Animator.StringToHash("playerAttacked");

        private void Start()
        {
            _playerController = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
            if (playerYAnim.gameObject.activeSelf)
            {
                if (projectile != null)
                {
                    if ((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed) && Time.time > _nextRangedLounch)
                    {
                        playerYAnim.SetBool(Attacked, true);
                        _rangedAnimationTime = Time.time + rangedAnimationRate;
                        RangedAttack();
                        _nextRangedLounch = Time.time + nextRangedLounchgeRate;
                    }
                    else
                    {
                        if (Time.time > _rangedAnimationTime)
                        {
                            playerYAnim.SetBool(Attacked, false);
                        }
                    }
                }
            }
            else
            {
                MeleeAttack();
            }
        }

        private void MeleeAttack()
        {
            if (Input.GetKey(KeyCode.K) || swordAttackButton.isPressed)
            {
                playerXAnim.SetBool(Attacked, true);
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                playerXAnim.SetBool(Attacked, false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        private void RangedAttack()
        {
            if (_playerController.FacingDirection == 1)
                Instantiate(projectile, missileStartPosition.transform.position,
                    Quaternion.AngleAxis(-180, Vector3.right));
            if (_playerController.FacingDirection == -1)
                Instantiate(projectile, missileStartPosition.transform.position, Quaternion.AngleAxis(180, Vector3.up));
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if ((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed) &&
                (other.CompareTag(Tags.Enemy) || other.CompareTag(Tags.FlyingDragon)) && Time.time > _nextDamage)
            {
                Health enemyHealth = other.gameObject.GetComponent<Health>();
                enemyHealth.AddDamage(playerDamage);
                _nextDamage = Time.time + damageRate;
            }
        }
    }
}