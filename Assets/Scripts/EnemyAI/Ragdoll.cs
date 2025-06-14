using UnityEngine;

/** 
* Ragdoll mechanics for the enemy. 
* @author: Yunseo Jeon
* @since: 2025-05-29
*/
public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidbodies; // Reference to all the bone rigidbodies. 
    Animator animator; // Reference to the enemy animator. 

    /** 
    * Setup the variables at the start when the script is initialized. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-29
    */
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    /** 
    * Disable ragdoll and by looping through all the rigidbodies. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    public void DeactivateRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        animator.enabled = true;
    }

    /** 
    * Enables ragdoll by looping through all the rigidbodies. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    public void activateRagdoll()
    {
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        animator.enabled = false;
    }
}
