using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public  bool dynamic = false;
    public bool waitForTrigger = false;
    public bool isDestroyable = false;
    public float destroyAfterSeconds;
    public float timeToStart = 0;
    public float speed = 20;
    public float damage = 10;
    public float damageRate = 1;
    public float gizmosLongness = 20;
    public float timeToStartAfterTrigger = 0;

    private bool triggered = false;

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
        if(other.tag == "Player" && Time.time > damageRate)
        {
            other.transform.GetComponent<Health>().AddDamage(damage);
            damageRate += Time.time;
        }
    }
    
//_____________________________Moving_______________________________
    private void Lounch()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

//______________________Coroutine Wait For Seconds After Trigger The Area_________________________
IEnumerator TriggerLounch()
{
    yield return new WaitForSeconds(timeToStartAfterTrigger);
    Lounch();
    yield return new WaitForSeconds(destroyAfterSeconds);
    Destroy(gameObject);
}

//_____________________________Gizmos Debuging____________________________________
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
