using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAreaScript : MonoBehaviour
{
    [SerializeField] PressChecker swordAttackButton;
    [SerializeField] GameObject player;
    Rigidbody2D playerRB;

    private void OnTriggerStay2D(Collider2D other) {
        if((Input.GetKey(KeyCode.Mouse0) || swordAttackButton.isPressed) && other.tag == "Enemy") {
            playerRB.AddForce(new Vector2(0, 500));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
