using UnityEngine;

namespace Core
{
    public class DestroyScript : MonoBehaviour
    {
        private float pointOfTime;
        [SerializeField] float secondsBeforeDestroy;
        // Start is called before the first frame update
        void Start()
        {
            pointOfTime = Time.time + secondsBeforeDestroy;
        }

        // Update is called once per frame
        void Update()
        {
            if(Time.time > pointOfTime) Destroy(gameObject); 
        }
    }
}
