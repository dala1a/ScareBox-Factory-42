using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* Abstract class which contains the mechanics for a popup promp message. 
* @author: Oliver Thompson
* @since: 2025-05-17
*/ 
public abstract class Interactable : MonoBehaviour
{

    [SerializeField] public string promptMessage;
    public bool useEvents;

    /** 
    * Can be overwritten later to change whether you want the prompt or not. 
    * @author: Oliver Thompson
    * @since: 2025-05-17

    */ 
    public virtual string OnLook()
    {
        return promptMessage;
    }

    /** 
    * Runs the method attached to the interact event when use events is enabled. 
    * @author: Oliver Thomspon
    * @since 2025-05-17
    */
    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvents>().OnInteract.Invoke();
        Interact();
    }

    /** 
    * Can be overwritten to change what it should do when you Interact with an object with no event
    * @author: Oliver Thompson
    * @since: 2025-05-17
    */ 
    protected virtual void Interact() { }

}
