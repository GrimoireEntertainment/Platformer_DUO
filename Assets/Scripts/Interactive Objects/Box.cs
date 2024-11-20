using Common;
using Core;
using UnityEngine;

namespace Interactive_Objects
{
    public class Box : MonoBehaviour
    {
        [SerializeField] bool drop;
        [SerializeField] Sprite openBox;
        [SerializeField] PressChecker swordAttackButton;
        [SerializeField] GameObject[] drops;

        Vector3 _dropItemPosition;
        bool _playerXIsHere;
        bool _playerYIsHere;

        private void Update()
        {
            if ((_playerXIsHere && (Input.GetKey(KeyCode.K) || swordAttackButton.isPressed)) || _playerYIsHere)
            {
                _dropItemPosition = transform.position;

                if (drop)
                {
                    foreach (var item in drops)
                    {
                        Instantiate(item, _dropItemPosition, Quaternion.identity);
                    }
                }

                GetComponent<SpriteRenderer>().sprite = openBox;
                gameObject.GetComponent<Box>().enabled = false;
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerAttackArea)) _playerXIsHere = true;
            if (other.CompareTag(Tags.Missile)) _playerYIsHere = true;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerAttackArea)) _playerXIsHere = false;
            if (other.CompareTag(Tags.Missile)) _playerYIsHere = false;
        }

        private void DropFunction()
        {
        }
    }
}