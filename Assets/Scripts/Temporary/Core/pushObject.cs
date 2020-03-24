using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushObject : MonoBehaviour
{
    [SerializeField] float playerSlowingRate;
    playerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag == "Player") {
            player.maxSpeed /= playerSlowingRate;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
        if(other.tag == "Player") {
            player.maxSpeed *= playerSlowingRate;
        }
    }

}
