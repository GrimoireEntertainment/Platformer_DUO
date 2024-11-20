using UnityEngine;

namespace Player_Scripts
{
    public class PlayerContainer : MonoBehaviour
    {
        [SerializeField] GameObject player_x;
        [SerializeField] GameObject player_y;

        private bool _isActive = true;

        private void Start()
        {
            Swap();
        }

        private void Update()
        {
            if (player_y.activeSelf)
            {
                transform.position = player_y.transform.position;
            }
            else
            {
                transform.position = player_x.transform.position;
            }

            if (Input.GetKeyDown(KeyCode.L)) Swap();
        }

        public void Swap()
        {
            player_x.SetActive(_isActive);
            player_y.SetActive(!_isActive);
            _isActive = !_isActive;
            GetComponent<PlayerController>().UpdateStats();
        }
    }
}