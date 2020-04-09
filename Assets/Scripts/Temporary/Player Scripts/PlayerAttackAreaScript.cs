using UnityEngine;

public class PlayerAttackAreaScript : MonoBehaviour
{
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject projectile = null;
    [SerializeField] GameObject missileStartPosition;

    public float playerDamage;
    public float damageRate;
    public float nextRangedLounchgeRate = 1;

    float nextDamage = 0.0f;
    float nextRangedLounch = 0;

    private void Update()
    {
        if(projectile != null)
        {
            if ((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed) && Time.time > nextRangedLounch)
            {
                RangedAttack();
                nextRangedLounch = Time.time + nextRangedLounchgeRate;
            }
        }
        else
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        if ((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void RangedAttack()
    {
        Instantiate(projectile, missileStartPosition.transform.position, projectile.transform.rotation);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if((Input.GetKey(KeyCode.K) || swordAttackButton.isPressed) && other.tag == "Enemy" && Time.time > nextDamage)
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            enemyHealth.AddDamage(playerDamage);
            nextDamage = Time.time + damageRate;
        }
    }
}