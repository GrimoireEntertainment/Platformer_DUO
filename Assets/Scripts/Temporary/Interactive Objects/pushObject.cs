using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    [SerializeField] float massForPlayerX;
    [SerializeField] float massForPlayerY;
    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            char xOrY = other.GetComponent<PlayerController>().xOrY;
            if(xOrY == 'x')
                myRB.mass = massForPlayerX;

            if (xOrY == 'y')
                myRB.mass = massForPlayerY;
        }
        
    }
}
