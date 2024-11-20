using Common;
using UI;
using UnityEngine;

namespace Interactive_Objects
{
    public class CollectCoin : MonoBehaviour
    {
        [SerializeField] private int valueOfCoin = 10;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                DisplayCoin.amountOfMoney += valueOfCoin;
                Destroy(gameObject);
            }
        }
    }
}