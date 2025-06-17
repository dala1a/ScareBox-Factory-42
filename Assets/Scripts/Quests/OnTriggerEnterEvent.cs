using UnityEngine;
using UnityEngine.Events;

/** 
* A trigger event for when the player collides with a quest trigger.
* @author: Yunseo Jeon
* @since: 2025-06-10
*/
public class OnTriggerEnterEvent : MonoBehaviour
{
    public UnityEvent<Collider> onTriggerEnter;

    /** 
    * If the user collides with quest trigger run the function attached to the script in unity.
    * @author: Yunseo Jeon
    * @since: 2025-06-10
    */
    void OnTriggerEnter(Collider col)
    {
        if (onTriggerEnter != null) onTriggerEnter.Invoke(col);
        Destroy(this);
    }
}