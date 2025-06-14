using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent; // A refernce ti the enemy Controller. 
    Animator animator; // A reference to the enemy animation controller. 

    /** 
    * Setup the variables at the start when the script is initialized. 
    * @author: Yunseo Jeon. 
    * @since: 2025-05-29
    */
    void Start()
    {   
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    /** 
    * Updating the animation of the enemy based on their speed. 
    * @author: Yunseo Jeon
    * @since 2025-05-29
    */ 
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
