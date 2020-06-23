using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]  bool dynamic = false;
    [SerializeField] bool waitForTrigger = false;
    public float destroyAfterSeconds = 2;
    [SerializeField] float timeToStart = 0;
    [SerializeField] float speed = 20;
    [SerializeField] float damage = 10;
    [SerializeField] float damageRate = 1;
    [SerializeField] float gizmosLongness = 20;
    [SerializeField] float timeToStartAfterTrigger = 0;

    private bool triggered = false;
    private float damageTime;

    public bool Triggered
    {
        get{return triggered;}
        set{triggered = value;}
    }


    void Update()
    {
        if(dynamic && Time.time >= timeToStart && !waitForTrigger)
        {
            Lounch();
        }
        if(waitForTrigger && triggered)
        {
            StartCoroutine(TriggerLounch());
        }
    }

//____________________________Damaging_____________________________
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Time.time > damageTime)
        {
            other.transform.GetComponent<Health>().AddDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }
    
//_____________________________Moving_______________________________
    private void Lounch()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        Destroy(gameObject, destroyAfterSeconds);
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
