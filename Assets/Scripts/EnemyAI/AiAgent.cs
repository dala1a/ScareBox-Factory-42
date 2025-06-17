using UnityEngine;
using UnityEngine.AI;

/** 
* Setup and hold variables that are used often for enemy states. 
* @author: Yunseo jeon
* @since 2025-05-29
*/
public class AiAgent : MonoBehaviour
{

    public AiStateMachine stateMachine; // Reference to the enemy states. 
    public AiStateID initialState; // The state the enemy starts in
    public NavMeshAgent navMeshAgent; // The enemy controller
    public AiAgentConfig config; // Reference to the config class. 
    public Transform playerTransform; // Reference to the players position. 
    public Transform waypoints; // The waypoints the enemy traverses in the roam state. 
    public Transform footstepsHolder; // The folder where all the footstep hitboxes are stored.  
    public bool footstepsTrigger; // True when footsteps are detected. 
    public Vector3 footstepPosition; // The center of each footstep hitbox. 


    /** 
    * Initializing variables and setting up enemy states. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (footstepsHolder == null)
        {
            footstepsHolder = GameObject.FindGameObjectWithTag("Footsteps").transform;
        }
        footstepsTrigger = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.registerState(new AiChasePlayerState());
        stateMachine.registerState(new AiIdlePlayerState());
        stateMachine.registerState(new AirRoamState());
        stateMachine.changeState(initialState);
    }

    /** 
    * Updates the mechanics for whatever state the enemy is in. 
    * @author: Yunseo Jeon
    * @since: 2025-05-29
    */
    void Update()
    {
        stateMachine.update();
    }

    
    // Called when the footstep collider hits an object with the enemy tag. 
    public void footstepsDetected()
    {
        footstepPosition = playerTransform.position;
        footstepsTrigger = true;
    }

}
