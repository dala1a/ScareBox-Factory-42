using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnTriggerEvent : MonoBehaviour
{
    private AiAgent aiAgent;

    void Start()
    {
        aiAgent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AiAgent>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            aiAgent.footstepsDetected(); 
        }
    }
}
