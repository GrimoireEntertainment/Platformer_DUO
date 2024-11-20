using UnityEngine;

namespace Traps
{
    public class Avtomat : MonoBehaviour
    {
        [SerializeField] float timeRate = 1;
        [SerializeField] GameObject missile;
        [SerializeField] float destroyAfterSeconds = 1;


        private float rate;

        void Update()
        {
            if(Time.time >= rate)
            {
                missile.GetComponent<Spike>().destroyAfterSeconds = destroyAfterSeconds;
                Instantiate(missile, transform.position, transform.rotation);
                rate = Time.time + timeRate;
            }
        }
    }
}
