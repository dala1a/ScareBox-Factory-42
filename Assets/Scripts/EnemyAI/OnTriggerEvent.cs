using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnTriggerEvent : MonoBehaviour
{

    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<AiAgent>().footstepsDetected(); 
        }
    }
}
