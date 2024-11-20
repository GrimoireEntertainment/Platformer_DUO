using UnityEngine;

namespace Traps
{
    public class Avtomat : MonoBehaviour
    {
        [SerializeField] float timeRate = 1;
        [SerializeField] GameObject missile;
        [SerializeField] float destroyAfterSeconds = 1;

        private float _rate;

        void Update()
        {
            if (Time.time >= _rate)
            {
                missile.GetComponent<Spike>().DestroyAfterSeconds = destroyAfterSeconds;
                Instantiate(missile, transform.position, transform.rotation);
                _rate = Time.time + timeRate;
            }
        }
    }
}