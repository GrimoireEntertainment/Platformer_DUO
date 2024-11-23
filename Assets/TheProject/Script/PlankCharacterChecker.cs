using System.Collections;
using UnityEngine;

namespace TheProject.Script
{
    public class PlankCharacterChecker : MonoBehaviour
    {
        private WaitForSeconds _waitForSeconds;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (other.GetComponent<SwitchCharacterController>().IsManCharacter)
                {
                    transform.parent.gameObject.layer = LayerMask.NameToLayer("Rope");

                    StartCoroutine(RecoverLayer());
                }
            }
        }

        private IEnumerator RecoverLayer()
        {
            yield return _waitForSeconds;

            transform.parent.gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }
}
