using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject enemy;
    [SerializeField] bool dropCoin;
    [SerializeField] bool dropEnemy;
    Vector3 dropItemPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "PlayerAttackArea" && (Input.GetKey(KeyCode.K) || swordAttackButton.isPressed)) {
            dropItemPosition = transform.position;
            Destroy(gameObject);
            if(dropCoin) Instantiate(coin, dropItemPosition, Quaternion.identity);
            if(dropEnemy) Instantiate(enemy, dropItemPosition, Quaternion.identity);
        }
    }
}
