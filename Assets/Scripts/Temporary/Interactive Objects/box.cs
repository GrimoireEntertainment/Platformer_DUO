using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    [SerializeField] bool drop;
    [SerializeField] Sprite openBox;
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject[] drops;
    Vector3 dropItemPosition;
    bool playerXIsHere;
    bool playerYIsHere;

    private void Update()
    {
        if ((playerXIsHere && (Input.GetKey(KeyCode.K) || swordAttackButton._isPressed)) || playerYIsHere)
        {
            dropItemPosition = transform.position;
            
            if (drop)
            {
                foreach (var item in drops)
                {
                    Instantiate(item, dropItemPosition, Quaternion.identity);
                }
            }
            GetComponent<SpriteRenderer>().sprite = openBox;
            gameObject.GetComponent<box>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerAttackArea") playerXIsHere = true;
        if(other.tag == "Missile") playerYIsHere = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "PlayerAttackArea") playerXIsHere = false;
        if(other.tag == "Missile") playerYIsHere = false;
    }

    private void DropFunction() {
        
    }
}
