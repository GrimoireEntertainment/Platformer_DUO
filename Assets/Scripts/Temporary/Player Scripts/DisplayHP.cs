using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float hp = GameObject.FindWithTag("Player").GetComponentInChildren<Health>().currentHealth;
        transform.GetComponent<Text>().text = hp.ToString();
    }
}
