using UnityEngine;
using System.Collections;

public class PlayerAttackAreaScript : MonoBehaviour
{
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject missileStartPosition;
    [SerializeField] float rangedAnimationRate;
    [SerializeField] Animator playerXAnim;
    [SerializeField] Animator playerYAnim;

    public float playerDamage;
    public float damageRate;
    public float nextRangedLounchgeRate = 1;

    float nextDamage = 0.0f;
    float nextRangedLounch = 0;
    private float rangedAnimationTime = 0f;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {

        if(playerYAnim.gameObject.activeSelf)
        {
            if(projectile != null)
            {
                if ((Input.GetKey(KeyCode.K) || swordAttackButton._isPressed) && Time.time > nextRangedLounch)
                {
                    playerYAnim.SetBool("playerAttacked", true);
                    rangedAnimationTime = Time.time + rangedAnimationRate;
                    RangedAttack();
                    nextRangedLounch = Time.time + nextRangedLounchgeRate;
                    
                }
                else
                {
                    if(Time.time > rangedAnimationTime)
                    {
                        playerYAnim.SetBool("playerAttacked", false);
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
        if (Input.GetKey(KeyCode.K) || swordAttackButton._isPressed)
        {
            playerXAnim.SetBool("playerAttacked", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            playerXAnim.SetBool("playerAttacked", false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void RangedAttack()
    {
        if(playerController.facingDirection == 1) Instantiate(projectile, missileStartPosition.transform.position, Quaternion.AngleAxis(-180, Vector3.right));
        if(playerController.facingDirection == -1) Instantiate(projectile, missileStartPosition.transform.position, Quaternion.AngleAxis(180, Vector3.up));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if((Input.GetKey(KeyCode.K) || swordAttackButton._isPressed) && (other.tag == "Enemy" || other.tag == "Flying Dragon") && Time.time > nextDamage)
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.AddDamage(playerDamage);
            nextDamage = Time.time + damageRate;
        }
    }
}