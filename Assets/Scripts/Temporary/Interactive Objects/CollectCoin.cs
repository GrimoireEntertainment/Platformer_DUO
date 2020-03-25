using UnityEngine.UI;
using UnityEngine;


public class CollectCoin : MonoBehaviour
{
    public int valueOfCoin = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DisplayCoin.amountOfMoney += valueOfCoin;
            Destroy(gameObject);
        }
    }
}