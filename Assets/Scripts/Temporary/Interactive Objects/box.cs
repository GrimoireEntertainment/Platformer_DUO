using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    [SerializeField] bool drop;
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject[] drops;
    Vector3 dropItemPosition;
    bool playerIsHere;

    void Update()
    {
        if (playerIsHere && (Input.GetKey(KeyCode.K) || swordAttackButton.isPressed))
        {
            dropItemPosition = transform.position;
            
            if (drop)
            {
                foreach (var item in drops)
                {
                    Instantiate(item, dropItemPosition, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerAttackArea") playerIsHere = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "PlayerAttackArea") playerIsHere = false;
    }
}
