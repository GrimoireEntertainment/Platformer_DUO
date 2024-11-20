using UnityEngine;

namespace Core
{
    public class FallowingCamera : MonoBehaviour
    {
        [SerializeField] Transform Character;
        [SerializeField] Vector3 offset;
        [SerializeField] [Range(0, 1)] float smooth = 0.03f;

        void LateUpdate()
        {
            Fallowing();
        }

        private void Fallowing()
        {
            Vector3 characterPosition = Character.position + offset;
            transform.position = Vector3.Lerp(transform.position, characterPosition, smooth);
        }
    }
}