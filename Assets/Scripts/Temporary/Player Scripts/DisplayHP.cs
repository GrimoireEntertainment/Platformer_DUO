using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    [SerializeField] restartGame theGameManager;
    float MaxHealth;


    private void Awake() {
        MaxHealth = GameObject.FindWithTag("Player").GetComponentInChildren<Health>().currentHealth;
    }
    // Update is called once per frame
    void Update()
    {
        float hp = GameObject.FindWithTag("Player").GetComponentInChildren<Health>().currentHealth;
        if(hp > MaxHealth * 0.7f && hp <= MaxHealth) transform.GetComponent<Text>().color = Color.green;
        if(hp > MaxHealth * 0.3f && hp <= MaxHealth * 0.7f) transform.GetComponent<Text>().color = Color.yellow;
        if(hp > 0 && hp <= MaxHealth * 0.3f) transform.GetComponent<Text>().color = new Color(1.0f, 0.55f, 0.1f);
        if(hp == 0) {
            transform.GetComponent<Text>().text = "0";
            transform.GetComponent<Text>().color = Color.red;
        }
        transform.GetComponent<Text>().text = hp.ToString();
        if(hp <= 0) {
            Destroy(GameObject.FindWithTag("Player"));
        }
    }
}
