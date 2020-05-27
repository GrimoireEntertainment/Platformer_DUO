using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        if(isPlayerHere) 
        {
            xOrY = GameObject.FindWithTag("Player").GetComponentInChildren<Stats>().xOrY;
            if(xOrY == 'x')
            {
                myRB.mass = massForPlayerX;
            }

            if (xOrY == 'y')
            {
                myRB.mass = massForPlayerY;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isPlayerHere = false;
        }
    }
}
