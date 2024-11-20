using UnityEngine;

namespace Core
{
    public class DestroyScript : MonoBehaviour
    {
        [SerializeField] float _secondsBeforeDestroy = 0.5f;

        private float _pointOfTime;

        void Start()
        {
            _pointOfTime = Time.time + _secondsBeforeDestroy;
        }

        void Update()
        {
            if(Time.time > _pointOfTime) Destroy(gameObject);
        }
    }
}
