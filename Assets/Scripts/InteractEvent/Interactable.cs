using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{

    [SerializeField] public string promptMessage; 
    public bool useEvents; 

    public virtual string OnLook()
    { 
        return promptMessage;
    }

    public void BaseInteract()
    {   
        if(useEvents)
            GetComponent<InteractionEvents>().OnInteract.Invoke(); 
        Interact();
    }

    protected virtual void Interact(){}

}
