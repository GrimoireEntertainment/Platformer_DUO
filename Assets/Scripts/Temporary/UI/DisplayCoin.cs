using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayCoin : MonoBehaviour
{
    static public int amountOfMoney = 0;

    private void Start()
    {
        amountOfMoney = 0;
    }

    private void Update()
    {
        GetComponent<Text>().text = amountOfMoney.ToString();    
    }
}
