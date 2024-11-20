using System.Collections;
using Common;
using Core;
using UnityEngine;

namespace Traps
{
    public class Spike : MonoBehaviour
    {
        public float DestroyAfterSeconds = 2;

        [SerializeField] private bool dynamic = true;
        [SerializeField] private bool waitForTrigger = false;
        [SerializeField] private float timeToStart = 0;
        [SerializeField] private float speed = 20;
        [SerializeField] private float damage = 10;
        [SerializeField] private float damageRate = 1;
        [SerializeField] private float gizmosLongness = 20;
        [SerializeField] private float timeToStartAfterTrigger = 0;

        private bool _triggered = false;
        private float _damageTime;

        public bool Triggered
        {
            get { return _triggered; }
            set { _triggered = value; }
        }


        void Update()
        {
            if (dynamic && Time.time >= timeToStart && !waitForTrigger)
            {
                Lounch();
            }

            if (waitForTrigger && _triggered)
            {
                StartCoroutine(TriggerLounch());
            }
        }

//____________________________Damaging_____________________________
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(Tags.PlayerTag) && Time.time > _damageTime)
            {
                other.transform.GetComponent<Health>().AddDamage(damage);
                _damageTime = Time.time + damageRate;
            }
        }

//_____________________________Moving_______________________________
        private void Lounch()
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            Destroy(gameObject, DestroyAfterSeconds);
        }

//______________________Coroutine Wait For Seconds After Trigger The Area_________________________
        IEnumerator TriggerLounch()
        {
            yield return new WaitForSeconds(timeToStartAfterTrigger);
            Lounch();
        }

//_____________________________Gizmos Debugging____________________________________
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 direction = transform.TransformDirection(Vector3.right) * gizmosLongness;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}


// namespace UnityEditor
// {
//     //____________________________________Inspector Customing_____________________________________
//     [CustomEditor(typeof(Spike))]
//     public class SpikeEditor : Editor
//     {
//         public override void OnInspectorGUI()
//         {

//             var spike = target as Spike;

//             spike.dynamic = GUILayout.Toggle(spike.dynamic, "Dynamic");
//             spike.waitForTrigger = GUILayout.Toggle(spike.waitForTrigger, "Wait For Trigger");
//             spike.isDestroyable = GUILayout.Toggle(spike.isDestroyable, "Is Destroyable");


//             if (spike.dynamic)
//             {
//                 if (spike.isDestroyable) spike.destroyAfterSeconds = EditorGUILayout.FloatField("Destroy After Seconds After Lounch", spike.destroyAfterSeconds);

//                 if (spike.waitForTrigger)
//                 {
//                     spike.timeToStartAfterTrigger = EditorGUILayout.FloatField("Time To Start After Trigger", spike.timeToStartAfterTrigger);
//                 }
//                 if (!spike.waitForTrigger) spike.timeToStart = EditorGUILayout.FloatField("Time To Start", spike.timeToStart);

//                 spike.speed = EditorGUILayout.FloatField("Speed", spike.speed);
//                 spike.damage = EditorGUILayout.FloatField("Damage", spike.damage);
//                 spike.damageRate = EditorGUILayout.FloatField("Damage Rate", spike.damageRate);
//                 spike.gizmosLongness = EditorGUILayout.FloatField("Gizmos Longness", spike.gizmosLongness);
//             }

//         }
//     }

// }